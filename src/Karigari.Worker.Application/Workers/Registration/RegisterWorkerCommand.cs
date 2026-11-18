namespace Karigari.Worker.Application.Workers.Registration;

/// <summary>
/// Application input for registering a worker profile.
/// Authentication and verification data are intentionally excluded.
/// </summary>
public sealed record RegisterWorkerCommand(
    string FirstName,
    string LastName,
    string ContactNumber,
    string? AlternateContactNumber = null,
    DateOnly? DateOfBirth = null);
