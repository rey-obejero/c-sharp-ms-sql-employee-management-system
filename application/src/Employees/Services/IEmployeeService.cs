using EmployeeManagementSystem.Employees.Dtos.Requests;
using EmployeeManagementSystem.Employees.Dtos.Responses;

namespace EmployeeManagementSystem.Employees.Services;

public interface IEmployeeService
{
    Task<IReadOnlyList<EmployeeResponse>> SearchAsync(
        EmployeeSearchRequest request,
        CancellationToken cancellationToken = default);

    Task<EmployeeResponse> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<EmployeeResponse> CreateAsync(
        CreateEmployeeRequest request,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        int id,
        UpdateEmployeeRequest request,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
