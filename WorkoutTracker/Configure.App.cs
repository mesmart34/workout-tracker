using System.Resources;
using AutoMapper.Extensions.ExpressionMapping;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using Radzen;
using WorkoutTracker.Application.Contracts;
using WorkoutTracker.Application.Profile;
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
        builder.Services.AddMapping();
    }
    
    private static IServiceCollection AddMapping(this IServiceCollection services)
    {
        services.AddAutoMapper(config =>
        {
            config.AddExpressionMapping();
                
            config.AddMaps(typeof(UserProfile));

            // config.CreateMap<IServiceEntity, IHasDateCreated>()
            //     .ForMember(x => x.DateCreated, o => o.Ignore())
            //     .IncludeAllDerived();
            // config.CreateMap<IServiceEntity, IHasDateUpdated>()
            //     .ForMember(x => x.DateUpdated, o => o.Ignore())
            //     .IncludeAllDerived();
            //     
            // config.CreateMap<IServiceEntity, IHasCreateUser>()
            //     .ForMember(x => x.CreatedByUserId, o => o.Ignore())
            //     .IncludeAllDerived();
            // config.CreateMap<IServiceEntity, IHasCreateUserReference>()
            //     .ForMember(x => x.CreatedByUser, o => o.Ignore())
            //     .IncludeAllDerived();
            //     
            // config.CreateMap<IServiceEntity, IHasUpdateUser>()
            //     .ForMember(x => x.UpdatedByUserId, o => o.Ignore())
            //     .IncludeAllDerived();
            // config.CreateMap<IServiceEntity, IHasUpdateUserReference>()
            //     .ForMember(x => x.UpdatedByUser, o => o.Ignore())
            //     .IncludeAllDerived();
            //     
            // config.CreateMap<IServiceEntity, ISoftDeleteDbEntity>()
            //     .ForMember(x => x.IsDeleted, o => o.Ignore())
            //     .IncludeAllDerived();
        });

        //services.AddScoped<IServiceMapper, ServiceMapper>();

        return services;
    }

    private static void AddDb(this WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        var config = configuration.GetSection("DatabaseSettings").Get<DbSettings>();
        builder.Services.AddPooledDbContextFactory<WorkoutTrackerDbContext>(x => 
            x
                .UseNpgsql(config?.ConnectionString())
                .UseLazyLoadingProxies()
                .UseSnakeCaseNamingConvention());
    }
    
    private static void AddScrutor(this IServiceCollection services)
    {
        services.Scan(scan => scan.FromAssemblies(typeof(IScopedService<>).Assembly)
            .AddClasses(classes => classes.AssignableTo(typeof(IScopedService<>)))
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