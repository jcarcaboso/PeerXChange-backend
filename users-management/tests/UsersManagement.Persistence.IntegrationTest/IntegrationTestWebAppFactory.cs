using DatabaseMigrations.MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;
using Testcontainers.RabbitMq;
using UsersManagement.Persistence.Models;

namespace UsersManagement.Persistence.IntegrationTest;

public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:15.4-bullseye")
        .WithDatabase("postgresql")
        .WithUsername("test")
        .WithPassword("1234")
        .WithCleanUp(true)
        .Build();

    private readonly RabbitMqContainer _rabbitContainer = new RabbitMqBuilder()
        .WithImage("rabbitmq:3-management")
        .WithUsername("rabbit")
        .WithPassword("rabbit")
        .Build();
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.Remove(services.Single(srv =>
                typeof(DbContextOptions<UsersManagementContext>) == srv.ServiceType));
            services.AddDbContext<UsersManagementContext>(opt => 
                opt.UseNpgsql(_dbContainer.GetConnectionString()));
            services.AddDbContext<MassTransitContext>(opt => 
                opt.UseNpgsql(_dbContainer.GetConnectionString()));
        });
    }
    
    async Task IAsyncLifetime.InitializeAsync()
    {
        await _dbContainer.StartAsync();
        await _rabbitContainer.StartAsync();
    }
    
    async Task IAsyncLifetime.DisposeAsync()
    {
        await _dbContainer.DisposeAsync();
        await _rabbitContainer.DisposeAsync();
    }
}