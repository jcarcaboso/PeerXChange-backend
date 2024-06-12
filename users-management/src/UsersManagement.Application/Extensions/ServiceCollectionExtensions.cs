using System.Reflection;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UsersManagement.Domain.Factories;
using UsersManagement.Infrastructure.Extensions;

namespace UsersManagement.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddDomain()
            .AddInfrastructure(configuration);
        
        services.AddMappings();
        services.AddMediator();
        
        return services;
    }

    private static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddSingleton<IUserFactory, UserFactory>();

        return services;
    }
    
    private static IServiceCollection AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        return services;
    }
    
    
    private static void AddMappings(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());
    }
}