using EmployeeManagementSystem.Data.Entities;

namespace EmployeeManagementSystem.Employees.Dtos.Requests;

public sealed record CreateEmployeeRequest(
    string FirstName,
    string LastName,
    string Email,
    string? Phone,
    int DepartmentId,
    string? Position,
    DateOnly HireDate,
    EmployeeStatus Status,
    decimal Salary) : IEmployeeRequest;
