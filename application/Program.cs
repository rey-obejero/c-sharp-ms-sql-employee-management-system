using EmployeeManagementSystem.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddData(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<DbInitializer>();
    await initializer.InitializeAsync();
}

app.MapGet("/", () => "EmployeeManagementSystem API");

app.Run();
