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
builder.Services.AddOpenApi();

const string clientCorsPolicy = "Client";

builder.Services.AddCors(options =>
{
    options.AddPolicy(clientCorsPolicy, policy =>
    {
        policy
            .WithOrigins("http://localhost:5173", "http://127.0.0.1:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services
    .AddHealthChecks()
    .AddDbContextCheck<AppDbContext>();

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseCors(clientCorsPolicy);
}

app.UseDefaultFiles();
app.UseStaticFiles();

using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<DbInitializer>();
    await initializer.InitializeAsync();
}

app.MapControllers();
app.MapHealthChecks("/health/ready");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapFallbackToFile("index.html");

app.Run();
