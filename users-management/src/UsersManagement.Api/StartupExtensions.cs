using System.Reflection;
using Mapster;

namespace UsersManagement.Api;

public static class StartupExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMappings();
        return services;
    }

    private static void AddMappings(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services
            .AddSingleton(TypeAdapterConfig.GlobalSettings);
    }
}