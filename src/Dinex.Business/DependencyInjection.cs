namespace Dinex.Business
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterBusinessDependecies(this IServiceCollection services)
        {

            services.RegisterInfraDependencies();

            #region register ContextAccessor
            services.AddHttpContextAccessor();
            //services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            #endregion

            #region AutoMapper
            services.AddAutoMapper(typeof(UserMapper));
            services.AddAutoMapper(typeof(LoginMapper));
            services.AddAutoMapper(typeof(CategoryMapper));
            services.AddAutoMapper(typeof(LaunchMapper));
            services.AddAutoMapper(typeof(PayMethodFromLaunchMapper));
            #endregion

            #region business validations
            //services.AddScoped<IValidator<UserRequestDto>, UserRequestModelValidation>();
            //services.AddScoped<IValidator<AccountActivationRequestDto>, ActivationRequestModelValidation>();
            //services.AddScoped<IValidator<CategoryRequestDto>, CategoryRequestModelValidation>();
            //services.AddScoped<IValidator<LaunchAndPayMethodRequestDto>, LaunchAndPayMethodRequestModelValidation>();
            #endregion

            #region business managers
            services.AddScoped<IActivationAccountManager, AccountManagerService>();
            services.AddScoped<ICategoryManager, CategoryManager>();
            services.AddScoped<ILaunchManager, LaunchManager>();
            services.AddScoped<IUserAmountManager, UserAmountManager>();           

            services.AddScoped<IHistoryFileManager, HistoryFileManagerService>();
            #endregion

            #region business services
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ICodeManagerService, CodeManagerService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICategoryToUserService, CategoryToUserService>();
            services.AddScoped<ILaunchService, LaunchService>();
            services.AddScoped<IPayMethodFromLaunchService, PayMethodFromLaunchService>();
            services.AddScoped<IUserAmountAvailableService, UserAmountAvailableService>();

            services.AddScoped<IQueueInService, QueueInService>();
            services.AddScoped<IHistoryFileService, HistoryFileService>();
            #endregion

            return services;
        }
    }
}
