namespace Dinex.Business
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterBusinessDependecies(IServiceCollection services)
        {

            services.RegisterInfraDependencies();

            #region business validations
            services.AddScoped<IValidator<UserRequestModel>, UserRequestModelValidation>();
            services.AddScoped<IValidator<ActivationRequestModel>, ActivationRequestModelValidation>();
            services.AddScoped<IValidator<CategoryRequestModel>, CategoryRequestModelValidation>();
            #endregion

            #region business services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IActivationService, ActivationService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICategoryToUserService, CategoryToUserService>();
            services.AddScoped<ILaunchService, LaunchService>();
            services.AddScoped<IPayMethodFromLaunchService, PayMethodFromLaunchService>();
            #endregion

            return services;
        }
    }
}
