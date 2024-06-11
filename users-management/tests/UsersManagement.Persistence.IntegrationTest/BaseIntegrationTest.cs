using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UsersManagement.Persistence.Models;
using Xunit;

namespace UsersManagement.Persistence.IntegrationTest;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>, IDisposable
{
    private readonly IServiceScope _scope;
    protected readonly UsersManagementContext Context;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();

        Context = _scope.ServiceProvider.GetRequiredService<UsersManagementContext>();
        Context.Database.Migrate();
    }
    
    public void Dispose()
    {
        _scope?.Dispose();
        Context?.Dispose();
    }
}