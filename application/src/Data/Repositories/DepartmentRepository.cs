using EmployeeManagementSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Data.Repositories;

public sealed class DepartmentRepository(AppDbContext context) : IDepartmentRepository
{
    public async Task<IReadOnlyList<Department>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await context.Departments
            .AsNoTracking()
            .OrderBy(d => d.Name)
            .ToListAsync(cancellationToken);
    }
}
