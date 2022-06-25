
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//connection string
//builder.Services.AddDbContext<DinexBackendContext>(
//    options => options.UseNpgsql(builder.Configuration.GetConnectionString("DinExDB"))
//    );

var appSettings = new AppSettings();
new ConfigureFromConfigurationOptions<AppSettings>(builder.Configuration.GetSection("AppSettings")).Configure(appSettings);
builder.Services.AddSingleton(appSettings);

// fix to work with this section on classes and methods
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

//configuration add to working
builder.Services.AddCors( options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins, 
        builder =>
        {
            builder.WithOrigins( appSettings.AllowedOrigin)
                .WithMethods("POST", "PUT", "GET", "DELETE", "OPTIONS")
                .WithHeaders("accept", "content-type", "origin", "authorization");
        });
});

builder.Services.RegisterBusinessDependecies();

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

// Execute Migrations on start app
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<DinexBackendContext>();
    dataContext.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
