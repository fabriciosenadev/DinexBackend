﻿namespace Dinex.Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterInfraDependencies(this IServiceCollection services)
        {
            // database context registration
            services.AddEntityFrameworkSqlite().AddDbContext<DinexBackendContext>();

            //services.AddMvc(
            //    options =>
            //        {
            //            options.Filters.Add(new ValidationMiddleware());
            //        }
            //    ).AddFluentValidation();

            #region infra services
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ICryptographyService, CryptographyService>();
            services.AddScoped<INotificationService, NotificationService>();
            #endregion

            #region generic repositories
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            #endregion

            #region repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ILaunchRepository, LaunchRepository>();
            services.AddScoped<ICodeManagerRepository, CodeManagerRepository>();

            services.AddScoped<IQueueInRepository, QueueInRepository>();
            services.AddScoped<IHistoryFileRepository, HistoryFileRepository>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ILaunchInvestingRepository, LaunchInvestingRepository>();
            services.AddScoped<IBrokerageRepository, BrokerageRepository>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryToUserRepository, CategoryToUserRepository>();
            services.AddScoped<IPayMethodFromLaunchRepository, PayMethodFromLaunchRepository>();
            services.AddScoped<IUserAmountAvailableRepository, UserAmountAvailableRepository>();
            #endregion

            return services;
        }
    }
}
