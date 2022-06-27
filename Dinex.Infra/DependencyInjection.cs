﻿namespace Dinex.Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterInfraDependencies(this IServiceCollection services)
        {
            // database context registration
            services.AddEntityFrameworkSqlite().AddDbContext<DinexBackendContext>();

            services.AddMvc(
                options =>
                    {
                        options.Filters.Add(new ValidationMiddleware());
                    }
                ).AddFluentValidation();

            #region infra services
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<ISendMailService, SendMailService>();
            services.AddScoped<ICryptographyService, CryptographyService>();
            #endregion

            #region repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IActivationRepository, ActivationRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryToUserRepository, CategoryToUserRepository>();
            services.AddScoped<ILaunchRepository, LaunchRepository>();
            services.AddScoped<IPayMethodFromLaunchRepository, PayMethodFromLaunchRepository>();
            #endregion

            return services;
        }
    }
}