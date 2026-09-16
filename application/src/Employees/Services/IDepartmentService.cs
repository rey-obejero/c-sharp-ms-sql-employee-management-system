using EmployeeManagementSystem.Employees.Dtos.Responses;

namespace EmployeeManagementSystem.Employees.Services;

public interface IDepartmentService
{
    Task<IReadOnlyList<DepartmentResponse>> GetAllAsync(
        CancellationToken cancellationToken = default);
}
