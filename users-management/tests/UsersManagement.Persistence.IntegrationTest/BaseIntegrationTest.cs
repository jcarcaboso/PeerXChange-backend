using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UsersManagement.Persistence.Models;
using Xunit;

namespace UsersManagement.Persistence.IntegrationTest;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
{
    private readonly IServiceScope _scope;

    protected readonly IntegrationTestWebAppFactory _factory; 
    protected readonly UsersManagementContext Context;
    protected readonly HttpClient HttpClient;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _factory = factory;
        _factory.WithWebHostBuilder(cfg =>
            cfg.ConfigureServices(srv => srv.AddDbContext<UsersManagementContext>(
                opt => opt.UseNpgsql(_factory._dbContainer.GetConnectionString()))));
            
        _scope = factory.Services.CreateScope();

        Context = _scope.ServiceProvider.GetRequiredService<UsersManagementContext>();
        
        Context.Database.Migrate();
        Context.Database.EnsureCreated();

        HttpClient = factory.CreateClient();
    }
    
    public void Dispose()
    {
        _scope?.Dispose();
        Context?.Dispose();
    }
}