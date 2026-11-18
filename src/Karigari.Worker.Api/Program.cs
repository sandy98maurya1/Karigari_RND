using Karigari.Worker.Api.Endpoints;
using Karigari.Worker.Application.Workers.Registration;
using Karigari.Worker.Infrastructure;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Karigari Worker API",
        Version = "v1",
        Description = "Worker registration and profile API for the Karigari platform."
    });
});

var connectionString = builder.Configuration.GetConnectionString("Karigari");
if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException(
        "Connection string 'Karigari' is missing. Configure it using .NET User Secrets.");
}

builder.Services.AddScoped<RegisterWorkerHandler>();
builder.Services.AddWorkerInfrastructure(connectionString);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Karigari Worker API v1");
    });
}

app.MapGet("/health/live", () => Results.Ok(new
{
    status = "Healthy",
    service = "Karigari.Worker.Api"
}))
.WithName("Liveness")
.WithTags("Health");

app.MapWorkerRegistrationEndpoints();

app.Run();

public partial class Program
{
}
