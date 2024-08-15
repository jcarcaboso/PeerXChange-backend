using System.Reflection;
using System.Text;
using DbUp;

namespace DatabaseMigrations;

public class MigrationService(string connectionString)
{
    public bool ExecuteMigrations()
    {
        EnsureDatabase.For.PostgresqlDatabase(connectionString);
        var engine = DeployChanges.To
            .PostgresqlDatabase(connectionString)
            .WithScriptsEmbeddedInAssembly(
                Assembly.GetExecutingAssembly(),
                s => s.Contains("up"),
                Encoding.UTF8)
            .LogToConsole()
            .Build();

        var result = engine.PerformUpgrade();

        return result.Successful;
    }
}