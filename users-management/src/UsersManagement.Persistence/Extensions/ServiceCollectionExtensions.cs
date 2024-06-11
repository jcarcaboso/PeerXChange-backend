using System.Reflection;
using Mapster;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UsersManagement.Domain.Repositories;
using UsersManagement.Persistence.Models;

namespace UsersManagement.Persistence.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistenceConfiguration(configuration);
        services.AddMappings();
        
        services.AddDbContext<UsersManagementContext>(builder => 
            builder.UseNpgsql(configuration.GetConnectionString("Sql"), opt => opt.EnableRetryOnFailure()));
        
        services.AddScoped<IUserRepository, UserRepository>();
        
        services.AddMassTransit(x =>
        {
            x.AddEntityFrameworkOutbox<UsersManagementContext>(opt =>
            {
                opt.UsePostgres();
                opt.UseBusOutbox();
            });
            
            x.SetKebabCaseEndpointNameFormatter();
            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("rabbit");
                    h.Password("rabbit");
                });
                
                cfg.ConfigureEndpoints(ctx);
            });
        });
        
        return services;
    }

    private static void AddPersistenceConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<UserSettings>()
            .BindConfiguration(UserSettings.SettingsKey)
            .ValidateOnStart();
    }

    private static void AddMappings(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());
    }
}