using Karigari.Worker.Application.Workers.Registration;
using Karigari.Worker.Domain.Entities;

namespace Karigari.Worker.Infrastructure.Persistence;

public sealed class WorkerProfileRepository(WorkerDbContext dbContext) : IWorkerProfileRepository
{
    public async Task<WorkerProfile> AddAsync(WorkerProfile profile, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(profile);
        dbContext.WorkerProfiles.Add(profile);
        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return profile;
    }
}
