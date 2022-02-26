
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var appSettings = new AppSettings();
new ConfigureFromConfigurationOptions<AppSettings>(builder.Configuration.GetSection("AppSettings")).Configure(appSettings);
builder.Services.AddSingleton(appSettings);
//builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

//configuration add to working
builder.Services.AddCors( options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins, 
        builder =>
        {
            builder.WithOrigins( appSettings.AllowedOrigin)
                .WithMethods("POST", "PUT", "GET")
                .WithHeaders("accept", "content-type", "origin");
        });
});

builder.Services.RegisterAllDependecies();

var app = builder.Build();

// Configure the HTTP request pipeline.
{ 
    //app.UseCors(x => x
    //.AllowAnyOrigin()
    //.AllowAnyMethod()
    //.AllowAnyHeader());
    app.UseCors(MyAllowSpecificOrigins);

    app.UseMiddleware<JwtMiddleware>();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
