using System.Resources;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using Radzen;
using WorkoutTracker.Application.Contracts;
using WorkoutTracker.Application.Service;
using WorkoutTracker.Auth;
using WorkoutTracker.Config;
using WorkoutTracker.Infrastructure.Db;

namespace WorkoutTracker;

public static class ConfigureApp
{
    public static void Setup(this WebApplicationBuilder builder)
    {
        AddConfig(builder);
        AddDb(builder);
        AddScrutor(builder.Services);
        builder.Services.AddScoped<AuthService>();
        builder.Services.AddRadzenComponents();
        builder.Services.AddScoped<IUserContext, UserContext>();
        builder.Services.AddScoped<ProtectedLocalStorage>();
        builder.Services.AddScoped<AuthenticationStateProvider, WorkoutTrackerAuthStateProvider>();
    }

    private static void AddDb(this WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        var config = configuration.GetSection("DatabaseSettings").Get<DbSettings>();
        builder.Services.AddPooledDbContextFactory<WorkoutTrackerDbContext>(x => 
            x
                .UseNpgsql(config?.ConnectionString())
                .UseSnakeCaseNamingConvention());
    }
    
    private static void AddScrutor(this IServiceCollection services)
    {
        services.Scan(scan => scan.FromAssemblies(typeof(IScopedService).Assembly)
            .AddClasses(classes => classes.AssignableTo(typeof(IScopedService)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());
    }
    
    private static WebApplicationBuilder AddConfig(this WebApplicationBuilder builder)
    {
        var config = builder.Configuration;
        
        const string configurationsDirectory = "Config";
            
        var env = builder.Environment;
        
        config
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"{configurationsDirectory}/dbsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"{configurationsDirectory}/dbsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"{configurationsDirectory}/urls.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"{configurationsDirectory}/urls.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"{configurationsDirectory}/dbsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

        return builder;
    }
}