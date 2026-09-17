using EmployeeManagementSystem.Common.Exceptions;
using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Data.Entities;
using EmployeeManagementSystem.Data.Repositories;
using EmployeeManagementSystem.Employees.Dtos.Requests;
using EmployeeManagementSystem.Employees.Dtos.Responses;

namespace EmployeeManagementSystem.Employees.Services;

public sealed class EmployeeService(
    IEmployeeRepository employeeRepository,
    AppDbContext context) : IEmployeeService
{
    public async Task<IReadOnlyList<EmployeeResponse>> SearchAsync(
        EmployeeSearchRequest request,
        CancellationToken cancellationToken = default)
    {
        var employees = await employeeRepository.SearchAsync(request.Search, cancellationToken);

        return employees.Select(ToResponse).ToList();
    }

    public async Task<EmployeeResponse> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var employee = await GetEmployeeAsync(id, cancellationToken);

        return ToResponse(employee);
    }

    public async Task<EmployeeResponse> CreateAsync(
        CreateEmployeeRequest request,
        CancellationToken cancellationToken = default)
    {
        var employee = new Employee
        {
            FirstName = request.FirstName.Trim(),
            LastName = request.LastName.Trim(),
            Email = request.Email.Trim(),
            Phone = request.Phone?.Trim(),
            DepartmentId = request.DepartmentId,
            Position = request.Position?.Trim(),
            HireDate = request.HireDate,
            Status = request.Status,
            Salary = request.Salary,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        employeeRepository.Add(employee);
        await context.SaveChangesAsync(cancellationToken);

        var created = await GetEmployeeAsync(employee.EmployeeId, cancellationToken);

        return ToResponse(created);
    }

    public async Task UpdateAsync(
        int id,
        UpdateEmployeeRequest request,
        CancellationToken cancellationToken = default)
    {
        var employee = await GetEmployeeAsync(id, cancellationToken);

        employee.FirstName = request.FirstName.Trim();
        employee.LastName = request.LastName.Trim();
        employee.Email = request.Email.Trim();
        employee.Phone = request.Phone?.Trim();
        employee.DepartmentId = request.DepartmentId;
        employee.Position = request.Position?.Trim();
        employee.HireDate = request.HireDate;
        employee.Status = request.Status;
        employee.Salary = request.Salary;
        employee.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var employee = await GetEmployeeAsync(id, cancellationToken);

        employeeRepository.Remove(employee);
        await context.SaveChangesAsync(cancellationToken);
    }

    private async Task<Employee> GetEmployeeAsync(int id, CancellationToken cancellationToken)
    {
        return await employeeRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException($"Employee {id} was not found.");
    }

    private static EmployeeResponse ToResponse(Employee employee)
    {
        return new EmployeeResponse(
            employee.EmployeeId,
            employee.FirstName,
            employee.LastName,
            employee.Email,
            employee.Phone,
            employee.DepartmentId,
            employee.Department?.Name ?? string.Empty,
            employee.Position,
            employee.HireDate,
            employee.Status,
            employee.Salary);
    }
}
