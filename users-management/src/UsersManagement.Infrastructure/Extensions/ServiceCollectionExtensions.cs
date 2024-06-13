using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UsersManagement.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // services.AddPersistence(configuration);
        
        return services;
    }
}