using EmployeeManagementSystem.Employees.Dtos.Responses;
using EmployeeManagementSystem.Employees.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Employees.Controllers;

[ApiController]
[Route("api/v1/departments")]
public sealed class DepartmentsController(IDepartmentService departmentService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<DepartmentResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var departments = await departmentService.GetAllAsync(cancellationToken);

        return Ok(departments);
    }
}
