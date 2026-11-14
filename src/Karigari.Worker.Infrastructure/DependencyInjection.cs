using Microsoft.Extensions.DependencyInjection;

namespace Karigari.Worker.Infrastructure;

/// <summary>
/// Infrastructure registrations are intentionally empty until the isolated
/// development database and approved persistence approach are confirmed.
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddWorkerInfrastructure(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        return services;
    }
}
