var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/health/live", () => Results.Ok(new
{
    status = "Healthy",
    service = "Karigari.Worker.Api"
}))
.WithName("Liveness");

app.Run();

public partial class Program
{
}
