using EmployeeManagementSystem.Employees.Dtos.Requests;
using FluentValidation;

namespace EmployeeManagementSystem.Employees.Validators;

public abstract class EmployeeRequestValidator<T> : AbstractValidator<T>
    where T : IEmployeeRequest
{
    protected EmployeeRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Email)
            .NotEmpty()
            .MaximumLength(255)
            .EmailAddress();

        RuleFor(x => x.Phone)
            .MaximumLength(30);

        RuleFor(x => x.DepartmentId)
            .GreaterThan(0);

        RuleFor(x => x.Position)
            .MaximumLength(100);

        RuleFor(x => x.HireDate)
            .Must(date => date != default)
            .WithMessage("Hire date is required.")
            .Must(date => date <= DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("Hire date cannot be in the future.");

        RuleFor(x => x.Status)
            .IsInEnum();

        RuleFor(x => x.Salary)
            .GreaterThanOrEqualTo(0);
    }
}
