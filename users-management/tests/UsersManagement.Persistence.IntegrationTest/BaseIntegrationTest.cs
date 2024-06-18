using DatabaseMigrations;
using DatabaseMigrations.MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UsersManagement.Persistence.Models;
using Xunit;

namespace UsersManagement.Persistence.IntegrationTest;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
{
    private readonly IServiceScope _scope;
 
    protected readonly UsersManagementContext Context;
    protected readonly HttpClient HttpClient;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();

        Context = _scope.ServiceProvider.GetRequiredService<UsersManagementContext>();
        var massTransitContext = _scope.ServiceProvider.GetRequiredService<MassTransitContext>();
        
        massTransitContext.Database.Migrate();
        massTransitContext.Database.EnsureCreated();
        
        ExecuteMigrations(Context.Database.GetConnectionString()!);

        HttpClient = factory.CreateClient();
    }
    
    public void Dispose()
    {
        _scope?.Dispose();
        Context?.Dispose();
    }

    private static void ExecuteMigrations(string connectionString)
    {
        var migrationService = new MigrationService(connectionString);

        var result = migrationService.ExecuteMigrations();

        if (!result)
        {
            throw new InvalidOperationException("Migrations failed");
        }
    }
}