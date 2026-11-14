namespace Karigari.Worker.Application.Workers.Registration;

/// <summary>
/// Coordinates creation and persistence of a worker profile.
/// </summary>
public sealed class RegisterWorkerHandler(IWorkerProfileRepository workerProfiles)
{
    public async Task<RegisterWorkerResult> HandleAsync(
        RegisterWorkerCommand command,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command);
        cancellationToken.ThrowIfCancellationRequested();

        var profile = Domain.Entities.WorkerProfile.Create(
            command.FirstName,
            command.LastName,
            command.ContactNumber,
            command.AlternateContactNumber,
            command.DateOfBirth);

        var persistedProfile = await workerProfiles
            .AddAsync(profile, cancellationToken)
            .ConfigureAwait(false);

        return new RegisterWorkerResult(
            persistedProfile.Id,
            persistedProfile.FirstName,
            persistedProfile.LastName,
            persistedProfile.PrimaryContactNumber.Value,
            persistedProfile.AlternateContactNumber?.Value,
            persistedProfile.DateOfBirth,
            persistedProfile.IsActive);
    }
}
