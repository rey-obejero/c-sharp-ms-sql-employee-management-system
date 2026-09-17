using EmployeeManagementSystem.Data.Entities;
using EmployeeManagementSystem.Employees.Dtos.Requests;
using EmployeeManagementSystem.Employees.Validators;

namespace EmployeeManagementSystem.UnitTests.Validators;

public class UpdateEmployeeRequestValidatorTests
{
    private readonly UpdateEmployeeRequestValidator _validator = new();

    private static UpdateEmployeeRequest ValidRequest() => new(
        FirstName: "Maria",
        LastName: "Dela Cruz",
        Email: "maria.delacruz@example.com",
        Phone: "+63 917 555 0200",
        DepartmentId: 1,
        Position: "Software Engineer",
        HireDate: new DateOnly(2024, 2, 1),
        Status: EmployeeStatus.Active,
        Salary: 60000m);

    [Fact]
    public void Validate_WithValidRequest_Passes()
    {
        var result = _validator.Validate(ValidRequest());

        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validate_WithMissingFirstName_Fails()
    {
        AssertFailsOn(
            ValidRequest() with { FirstName = "" },
            nameof(UpdateEmployeeRequest.FirstName));
    }

    [Theory]
    [InlineData("")]
    [InlineData("not-an-email")]
    public void Validate_WithInvalidEmail_Fails(string email)
    {
        AssertFailsOn(
            ValidRequest() with { Email = email },
            nameof(UpdateEmployeeRequest.Email));
    }

    [Fact]
    public void Validate_WithInvalidDepartmentId_Fails()
    {
        AssertFailsOn(
            ValidRequest() with { DepartmentId = 0 },
            nameof(UpdateEmployeeRequest.DepartmentId));
    }

    [Fact]
    public void Validate_WithFutureHireDate_Fails()
    {
        var future = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(1);

        AssertFailsOn(
            ValidRequest() with { HireDate = future },
            nameof(UpdateEmployeeRequest.HireDate));
    }

    [Fact]
    public void Validate_WithNegativeSalary_Fails()
    {
        AssertFailsOn(
            ValidRequest() with { Salary = -1m },
            nameof(UpdateEmployeeRequest.Salary));
    }

    [Fact]
    public void Validate_WithUnknownStatus_Fails()
    {
        AssertFailsOn(
            ValidRequest() with { Status = (EmployeeStatus)99 },
            nameof(UpdateEmployeeRequest.Status));
    }

    private void AssertFailsOn(UpdateEmployeeRequest request, string propertyName)
    {
        var result = _validator.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.PropertyName == propertyName);
    }
}
