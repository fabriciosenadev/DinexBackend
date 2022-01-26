
namespace Dinex.WebApi.Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterAllDependecies(this IServiceCollection services)
        {
            // database context registration
            services.AddEntityFrameworkSqlite().AddDbContext<DinexBackendContext>();



            #region AutoMapper
            services.AddAutoMapper(typeof(UserMapper));
            #endregion

            #region business services
            services.AddScoped<IUserService, UserService>();
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
