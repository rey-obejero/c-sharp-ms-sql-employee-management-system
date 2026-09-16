using EmployeeManagementSystem.Data.Entities;

namespace EmployeeManagementSystem.Data.Repositories;

public interface IDepartmentRepository
{
    Task<IReadOnlyList<Department>> GetAllAsync(CancellationToken cancellationToken = default);
}
