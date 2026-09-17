using EmployeeManagementSystem.Data.Entities;
using EmployeeManagementSystem.Employees.Dtos.Requests;
using EmployeeManagementSystem.Employees.Validators;

namespace EmployeeManagementSystem.UnitTests.Validators;

public class CreateEmployeeRequestValidatorTests
{
    private readonly CreateEmployeeRequestValidator _validator = new();

    private static CreateEmployeeRequest ValidRequest() => new(
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

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Validate_WithMissingFirstName_Fails(string firstName)
    {
        AssertFailsOn(
            ValidRequest() with { FirstName = firstName },
            nameof(CreateEmployeeRequest.FirstName));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Validate_WithMissingLastName_Fails(string lastName)
    {
        AssertFailsOn(
            ValidRequest() with { LastName = lastName },
            nameof(CreateEmployeeRequest.LastName));
    }

    [Fact]
    public void Validate_WithFirstNameTooLong_Fails()
    {
        AssertFailsOn(
            ValidRequest() with { FirstName = new string('a', 51) },
            nameof(CreateEmployeeRequest.FirstName));
    }

    [Fact]
    public void Validate_WithLastNameTooLong_Fails()
    {
        AssertFailsOn(
            ValidRequest() with { LastName = new string('a', 51) },
            nameof(CreateEmployeeRequest.LastName));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("not-an-email")]
    public void Validate_WithInvalidEmail_Fails(string email)
    {
        AssertFailsOn(
            ValidRequest() with { Email = email },
            nameof(CreateEmployeeRequest.Email));
    }

    [Fact]
    public void Validate_WithEmailTooLong_Fails()
    {
        var email = $"{new string('a', 250)}@example.com";

        AssertFailsOn(
            ValidRequest() with { Email = email },
            nameof(CreateEmployeeRequest.Email));
    }

    [Fact]
    public void Validate_WithPhoneTooLong_Fails()
    {
        AssertFailsOn(
            ValidRequest() with { Phone = new string('9', 31) },
            nameof(CreateEmployeeRequest.Phone));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validate_WithInvalidDepartmentId_Fails(int departmentId)
    {
        AssertFailsOn(
            ValidRequest() with { DepartmentId = departmentId },
            nameof(CreateEmployeeRequest.DepartmentId));
    }

    [Fact]
    public void Validate_WithPositionTooLong_Fails()
    {
        AssertFailsOn(
            ValidRequest() with { Position = new string('a', 101) },
            nameof(CreateEmployeeRequest.Position));
    }

    [Fact]
    public void Validate_WithMissingHireDate_Fails()
    {
        AssertFailsOn(
            ValidRequest() with { HireDate = default },
            nameof(CreateEmployeeRequest.HireDate));
    }

    [Fact]
    public void Validate_WithFutureHireDate_Fails()
    {
        var future = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(1);

        AssertFailsOn(
            ValidRequest() with { HireDate = future },
            nameof(CreateEmployeeRequest.HireDate));
    }

    [Fact]
    public void Validate_WithNegativeSalary_Fails()
    {
        AssertFailsOn(
            ValidRequest() with { Salary = -1m },
            nameof(CreateEmployeeRequest.Salary));
    }

    [Fact]
    public void Validate_WithUnknownStatus_Fails()
    {
        AssertFailsOn(
            ValidRequest() with { Status = (EmployeeStatus)99 },
            nameof(CreateEmployeeRequest.Status));
    }

    private void AssertFailsOn(CreateEmployeeRequest request, string propertyName)
    {
        var result = _validator.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.PropertyName == propertyName);
    }
}
