namespace EmployeeManagementSystem.Data.Entities;

public class Department
{
    public int DepartmentId { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public ICollection<Employee> Employees { get; set; } = [];
}
