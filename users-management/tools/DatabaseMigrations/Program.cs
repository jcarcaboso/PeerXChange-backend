using DatabaseMigrations;
using DatabaseMigrations.MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<MassTransitContext>(opt => 
    opt.UseNpgsql(builder.Configuration.GetConnectionString("Sql")));

builder.Services.AddSingleton<MigrationService>(_ =>
    new MigrationService(builder.Configuration.GetConnectionString("Sql")!));

using var host = builder.Build();

var migrationService = host.Services.GetRequiredService<MigrationService>();

Console.WriteLine("Running migrations...");
var isSuccess = migrationService.ExecuteMigrations();

if (!isSuccess)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Failure!");
}
else
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Success!");
}

Console.ResetColor();
Console.WriteLine("Migrations finished!");



