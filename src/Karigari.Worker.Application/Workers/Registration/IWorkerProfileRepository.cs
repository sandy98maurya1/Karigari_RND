using Karigari.Worker.Domain.Entities;

namespace Karigari.Worker.Application.Workers.Registration;

/// <summary>
/// Persistence boundary for worker profiles.
/// Implementations belong to the Infrastructure layer.
/// </summary>
public interface IWorkerProfileRepository
{
    /// <summary>
    /// Persists a new profile and returns the persisted profile with its generated identifier.
    /// </summary>
    Task<WorkerProfile> AddAsync(
        WorkerProfile profile,
        CancellationToken cancellationToken = default);
}
