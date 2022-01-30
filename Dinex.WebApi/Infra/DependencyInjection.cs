namespace Dinex.WebApi.Infra
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

            #region bisness validations
            services.AddScoped<IValidator<UserInputModel>, UserModelValidation>();
            #endregion

            #region business services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            #endregion

            #region infra services
            services.AddScoped<IJwtService, JwtService>();
            #endregion

            #region repositories
            services.AddScoped<IUserRepository, UserRepository>();
            #endregion

            return services;
        }
    }
}
