using EmployeeManagementSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Data.Repositories;

public sealed class EmployeeRepository(AppDbContext context) : IEmployeeRepository
{
    public async Task<IReadOnlyList<Employee>> SearchAsync(
        string? search,
        CancellationToken cancellationToken = default)
    {
        var query = context.Employees
            .AsNoTracking()
            .Include(e => e.Department)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim();
            query = query.Where(e =>
                e.FirstName.Contains(term)
                || e.LastName.Contains(term)
                || e.Email.Contains(term));
        }

        return await query
            .OrderBy(e => e.LastName)
            .ThenBy(e => e.FirstName)
            .ToListAsync(cancellationToken);
    }

    public Task<Employee?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return context.Employees
            .Include(e => e.Department)
            .FirstOrDefaultAsync(e => e.EmployeeId == id, cancellationToken);
    }

    public void Add(Employee employee) => context.Employees.Add(employee);

    public void Update(Employee employee) => context.Employees.Update(employee);

    public void Remove(Employee employee) => context.Employees.Remove(employee);
}
