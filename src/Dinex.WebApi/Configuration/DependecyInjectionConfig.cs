using Swashbuckle.AspNetCore.SwaggerGen;

namespace Dinex.Backend.WebApi.Configuration
{
    public static class DependecyInjectionConfig
    {

        public static IServiceCollection RegisterAllDepdencies(this IServiceCollection services)
        {
            #region swagger
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            #endregion

            services.RegisterBusinessDependecies();

            return services;
        }
    }
}
