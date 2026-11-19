namespace Karigari.Worker.Api.Contracts.Workers;

public sealed record RegisterWorkerRequest(
    string FirstName,
    string LastName,
    string ContactNumber,
    string? AlternateContactNumber,
    DateOnly? DateOfBirth);
