using EmployeeManagementSystem.Data.Entities;

namespace EmployeeManagementSystem.Employees.Dtos.Requests;

public interface IEmployeeRequest
{
    string FirstName { get; }
    string LastName { get; }
    string Email { get; }
    string? Phone { get; }
    int DepartmentId { get; }
    string? Position { get; }
    DateOnly HireDate { get; }
    EmployeeStatus Status { get; }
    decimal Salary { get; }
}
