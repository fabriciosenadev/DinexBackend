
using Dinex.Backend.WebApi.Configuration;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddApiConfig(builder.Configuration);

builder.Services.AddSwaggerConfiguration();

builder.Services.RegisterAllDependencies();

var app = builder.Build();

var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

app.UseApiConfig(app.Environment, app.Services);

app.UseSwaggerConfiguration(apiVersionDescriptionProvider);

app.Run();
