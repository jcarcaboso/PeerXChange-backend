using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
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
        .Build();

    private readonly RabbitMqContainer _rabbitContainer = new RabbitMqBuilder()
        .WithImage("rabbitmq:3-management")
        .WithUsername("rabbit")
        .WithPassword("rabbit")
        .Build();

    public required HttpClient HttpClient;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.AddDbContext<UsersManagementContext>(opt => opt.UseNpgsql(_dbContainer.GetConnectionString()));
        });

        HttpClient = CreateClient();
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