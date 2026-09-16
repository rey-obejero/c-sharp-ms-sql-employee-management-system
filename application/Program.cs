var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", () => "EmployeeManagementSystem API");

app.Run();
