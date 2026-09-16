using System.Text.Json.Serialization;

namespace EmployeeManagementSystem.Data.Entities;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EmployeeStatus
{
    Active,
    Inactive,
}
