using EmployeeManagementSystem.Common;
using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Employees;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddSimpleConsole();

builder.Services.AddData(builder.Configuration);
builder.Services.AddEmployees();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddControllers();

builder.Services
    .AddHealthChecks()
    .AddDbContextCheck<AppDbContext>();

var app = builder.Build();

app.UseExceptionHandler();

using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<DbInitializer>();
    await initializer.InitializeAsync();
}

app.MapControllers();
app.MapHealthChecks("/health/ready");

app.Run();
