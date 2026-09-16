using System.Text;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EmployeeManagementSystem.Data;

public sealed class DbInitializer(
    AppDbContext context,
    IOptions<DatabaseOptions> options,
    ILogger<DbInitializer> logger)
{
    private const string DatabaseName = "EmployeeManagementSystem";
    private static readonly string ScriptsPath =
        Path.Combine(AppContext.BaseDirectory, "src", "Data", "Scripts");

    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        if (!options.Value.ShouldInitialize)
        {
            logger.LogInformation("Database initialization is disabled.");
            return;
        }

        var connectionString = context.Database.GetConnectionString();

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("The database connection string is not configured.");
        }

        var masterConnectionString = new SqlConnectionStringBuilder(connectionString)
        {
            InitialCatalog = "master",
        }.ConnectionString;

        logger.LogInformation("Ensuring the {DatabaseName} database exists.", DatabaseName);
        await ExecuteScriptAsync(masterConnectionString, "00_create_database.sql", cancellationToken);

        logger.LogInformation("Applying the schema and seed data.");
        await ExecuteScriptAsync(connectionString, "01_migrate_schema.sql", cancellationToken);
        await ExecuteScriptAsync(connectionString, "02_seed.sql", cancellationToken);

        logger.LogInformation("Database initialization complete.");
    }

    private async Task ExecuteScriptAsync(
        string connectionString,
        string fileName,
        CancellationToken cancellationToken)
    {
        var scriptPath = Path.Combine(ScriptsPath, fileName);
        var script = await File.ReadAllTextAsync(scriptPath, cancellationToken);

        await using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        foreach (var batch in SplitBatches(script))
        {
            await using var command = connection.CreateCommand();
            command.CommandText = batch;
            await command.ExecuteNonQueryAsync(cancellationToken);
        }
    }

    private static IEnumerable<string> SplitBatches(string script)
    {
        var batch = new StringBuilder();

        foreach (var line in script.Split('\n'))
        {
            if (line.Trim().Equals("GO", StringComparison.OrdinalIgnoreCase))
            {
                if (batch.Length > 0)
                {
                    yield return batch.ToString();
                    batch.Clear();
                }

                continue;
            }

            batch.AppendLine(line);
        }

        if (batch.Length > 0)
        {
            yield return batch.ToString();
        }
    }
}
