using EmployeeManagementSystem.Employees.Dtos.Requests;
using EmployeeManagementSystem.Employees.Services;
using EmployeeManagementSystem.Employees.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagementSystem.Employees;

public static class DependencyInjection
{
    public static IServiceCollection AddEmployees(this IServiceCollection services)
    {
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IDepartmentService, DepartmentService>();

        services.AddScoped<IValidator<CreateEmployeeRequest>, CreateEmployeeRequestValidator>();
        services.AddScoped<IValidator<UpdateEmployeeRequest>, UpdateEmployeeRequestValidator>();

        return services;
    }
}
