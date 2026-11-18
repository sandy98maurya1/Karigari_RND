namespace Karigari.Worker.Application.Workers.Registration;

/// <summary>
/// Safe result returned after a worker profile has been persisted.
/// Contains no credentials, OTPs, or token material.
/// </summary>
public sealed record RegisterWorkerResult(
    long WorkerId,
    string FirstName,
    string LastName,
    string ContactNumber,
    string? AlternateContactNumber,
    DateOnly? DateOfBirth,
    bool IsActive);
