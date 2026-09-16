using EmployeeManagementSystem.Data.Entities;

namespace EmployeeManagementSystem.Data.Repositories;

public interface IEmployeeRepository
{
    Task<IReadOnlyList<Employee>> SearchAsync(
        string? search,
        CancellationToken cancellationToken = default);

    Task<Employee?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    void Add(Employee employee);

    void Update(Employee employee);

    void Remove(Employee employee);
}
