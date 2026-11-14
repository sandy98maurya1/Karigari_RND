using Karigari.Worker.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddWorkerInfrastructure();

var app = builder.Build();

app.MapGet("/health/live", () => Results.Ok(new
{
    status = "Healthy",
    service = "Karigari.Worker.Api"
}))
.WithName("Liveness");

app.Run();

public partial class Program;
