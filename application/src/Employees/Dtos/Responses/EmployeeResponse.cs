using EmployeeManagementSystem.Data.Entities;

namespace EmployeeManagementSystem.Employees.Dtos.Responses;

public sealed record EmployeeResponse(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    string? Phone,
    int DepartmentId,
    string Department,
    string? Position,
    DateOnly HireDate,
    EmployeeStatus Status,
    decimal Salary);
