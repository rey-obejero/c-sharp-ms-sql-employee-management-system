namespace EmployeeManagementSystem.Data;

public sealed class DatabaseOptions
{
    public const string SectionName = "Database";

    public bool ShouldInitialize { get; init; }
}
