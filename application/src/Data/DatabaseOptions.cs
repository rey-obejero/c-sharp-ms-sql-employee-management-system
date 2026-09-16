using Microsoft.Extensions.Configuration;

namespace EmployeeManagementSystem.Data;

public sealed class DatabaseOptions
{
    public const string SectionName = "Database";

    [ConfigurationKeyName("Initialize")]
    public bool ShouldInitialize { get; init; }
}
