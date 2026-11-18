using Karigari.Worker.Application.Workers.Registration;
using Karigari.Worker.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Karigari.Worker.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddWorkerInfrastructure(this IServiceCollection services, string connectionString)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);
        services.AddDbContext<WorkerDbContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<IWorkerProfileRepository, WorkerProfileRepository>();
        return services;
    }
}
