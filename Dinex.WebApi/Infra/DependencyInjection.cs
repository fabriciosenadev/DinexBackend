﻿namespace Dinex.WebApi.Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterAllDependecies(this IServiceCollection services)
        {
            // database context registration
            services.AddEntityFrameworkSqlite().AddDbContext<DinexBackendContext>();

            services.AddMvc(
                options =>
                    {
                        options.Filters.Add(new ValidationMiddleware());
                    }
                ).AddFluentValidation();


            #region AutoMapper
            services.AddAutoMapper(typeof(UserMapper));
            services.AddAutoMapper(typeof(LoginMapper));
            #endregion

            #region business validations
            services.AddScoped<IValidator<UserInputModel>, UserModelValidation>();
            services.AddScoped<IValidator<ActivationInputModel>, ActivationValidation>();
            #endregion

            #region business services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IActivationService, ActivationService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICategoryToUserService, CategoryToUserService>();
            #endregion

            #region infra services
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<ISendMailService, SendMailService>();
            #endregion

            #region repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IActivationRepository, ActivationRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryToUserRepository, CategoryToUserRepository>();
            #endregion

            return services;
        }
    }
}
