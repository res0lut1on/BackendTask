using BackendTestTask.Services;
using System.Security.Principal;
using BackendTestTask.AspNetExtensions.Filters;
using BackendTestTask.Database.Entities;
using BackendTestTask.UserContext;
using BackendTestTask.Database.Models;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.SignalR;
using System.Configuration;
using BackendTestTask.Database;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

const string CorsPolicyKey = "CorsPolicy";

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

var sentrySection = configuration.GetSection("Sentry");
//var swaggerSection = configuration.GetSection(nameof(SwaggerSettings));
//
//var sentrySettings = sentrySection.Get<SentrySettings>();
//var swaggerSettings = swaggerSection.Get<SwaggerSettings>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "BackendTestTask API",
        Description = "An ASP.NET Core Web API for managing Node items",  //                                            !!!!
        Contact = new OpenApiContact
        {
            Name = "Tasks",
            Url = new Uri("https://test.vmarmysh.com/user/description/backend")
        }
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});


services.AddServices();

services.AddControllers(options =>
{
    options.Filters.Add<ExceptionHandlerAttribute>();
    options.Filters.Add<ModelValidationAttribute>();
    options.Filters.Add<ResponseModelFilter>();
    
    // example auth

    options.AddUserContext();
    options.AddUserContext<Account, AppUserModel>();

    options.Filters.Add<UserContextFilter>(); // user filter for 401, 403 statuses
        //options.ReturnHttpNotAcceptable = true;
});
builder.Services.AddEndpointsApiExplorer();

services
.AddLogging(logger => logger.AddLog4Net())
    .AddOptions()
    .AddDbContext<BackendTestTaskContext>(options =>
    {
        options.UseSqlServer(configuration.GetConnectionString("BackendTestTaskDatabase"));
    })
    .AddUserContext<Account, AppUserModel>();

var res = IsConnectionStringValid(configuration.GetConnectionString("BackendTestTaskDatabase"));

Console.ForegroundColor = res ? ConsoleColor.Green : ConsoleColor.Red;
Console.WriteLine("IsConnectionStringValid " + res);
Console.ForegroundColor = ConsoleColor.Black;

services.AddCors(options => options.AddPolicy(CorsPolicyKey, builder =>
{
    builder
        //.WithOrigins(List<string> cors) any cors
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(CorsPolicyKey);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();



// to remove 

bool IsConnectionStringValid(string connectionString)
{
    try
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            if (connection.State == System.Data.ConnectionState.Open)
            {
                Console.WriteLine("valid.");
                return true;
            }
            else
            {
                Console.WriteLine("connection isn't valid.");
                return false;
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Exception -> {ex.Message}");
        return false;
    }
}