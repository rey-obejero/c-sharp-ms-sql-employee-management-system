using EmployeeManagementSystem.Data.Repositories;
using EmployeeManagementSystem.Employees.Dtos.Responses;

namespace EmployeeManagementSystem.Employees.Services;

public sealed class DepartmentService(IDepartmentRepository departmentRepository) : IDepartmentService
{
    public async Task<IReadOnlyList<DepartmentResponse>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        var departments = await departmentRepository.GetAllAsync(cancellationToken);

        return departments
            .Select(department => new DepartmentResponse(department.DepartmentId, department.Name))
            .ToList();
    }
}
