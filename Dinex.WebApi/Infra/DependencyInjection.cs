namespace Dinex.WebApi.Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterAllDependecies(this IServiceCollection services)
        {
            services.RegisterSwagger();

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
            //services.AddAutoMapper(typeof(CategoryMapper));
            #endregion

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
            #endregion

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
            #endregion

            return services;
        }

        private static IServiceCollection RegisterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "",
                    Version = "v1",
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header usign the Bearer scheme. Example: Bearer {token}"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] { }
                        }
                });
            });

            return services;
        }
    }
}
