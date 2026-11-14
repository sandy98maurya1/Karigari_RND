using Karigari.Worker.Domain.ValueObjects;

namespace Karigari.Worker.Domain.Entities;

/// <summary>
/// Represents a worker's profile information.
/// Authentication credentials and token material are intentionally excluded.
/// </summary>
public sealed class WorkerProfile
{
    private WorkerProfile(
        long id,
        string firstName,
        string lastName,
        ContactNumber contactNumber,
        ContactNumber? alternateContactNumber,
        DateOnly? dateOfBirth,
        bool isActive)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        ContactNumber = contactNumber;
        AlternateContactNumber = alternateContactNumber;
        DateOfBirth = dateOfBirth;
        IsActive = isActive;
    }

    /// <summary>
    /// Gets the database identity. Zero means the profile has not been persisted.
    /// The long type aligns with the historical worker identifier.
    /// </summary>
    public long Id { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public ContactNumber ContactNumber { get; private set; }

    public ContactNumber? AlternateContactNumber { get; private set; }

    public DateOnly? DateOfBirth { get; private set; }

    /// <summary>
    /// New profiles remain inactive until a later account-verification workflow activates them.
    /// </summary>
    public bool IsActive { get; private set; }

    public static WorkerProfile Create(
        string firstName,
        string lastName,
        string contactNumber,
        string? alternateContactNumber = null,
        DateOnly? dateOfBirth = null)
    {
        ValidateName(firstName, nameof(firstName));
        ValidateName(lastName, nameof(lastName));
        ValidateDateOfBirth(dateOfBirth);

        return new WorkerProfile(
            id: 0,
            firstName.Trim(),
            lastName.Trim(),
            ContactNumber.Create(contactNumber, nameof(contactNumber)),
            CreateOptionalContactNumber(alternateContactNumber, nameof(alternateContactNumber)),
            dateOfBirth,
            isActive: false);
    }

    /// <summary>
    /// Rehydrates a profile from persistence. This factory is intended for repository use.
    /// </summary>
    public static WorkerProfile Rehydrate(
        long id,
        string firstName,
        string lastName,
        string contactNumber,
        string? alternateContactNumber,
        DateOnly? dateOfBirth,
        bool isActive)
    {
        if (id <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(id), "A persisted worker ID must be positive.");
        }

        ValidateName(firstName, nameof(firstName));
        ValidateName(lastName, nameof(lastName));
        ValidateDateOfBirth(dateOfBirth);

        return new WorkerProfile(
            id,
            firstName.Trim(),
            lastName.Trim(),
            ContactNumber.Create(contactNumber, nameof(contactNumber)),
            CreateOptionalContactNumber(alternateContactNumber, nameof(alternateContactNumber)),
            dateOfBirth,
            isActive);
    }

    public void UpdatePersonalDetails(
        string firstName,
        string lastName,
        string? alternateContactNumber,
        DateOnly? dateOfBirth)
    {
        ValidateName(firstName, nameof(firstName));
        ValidateName(lastName, nameof(lastName));
        ValidateDateOfBirth(dateOfBirth);

        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        AlternateContactNumber = CreateOptionalContactNumber(
            alternateContactNumber,
            nameof(alternateContactNumber));
        DateOfBirth = dateOfBirth;
    }

    public void ChangePrimaryContactNumber(string contactNumber)
    {
        ContactNumber = ContactNumber.Create(contactNumber, nameof(contactNumber));
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    private static void ValidateName(string value, string parameterName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, parameterName);
    }

    private static void ValidateDateOfBirth(DateOnly? dateOfBirth)
    {
        if (dateOfBirth > DateOnly.FromDateTime(DateTime.UtcNow))
        {
            throw new ArgumentOutOfRangeException(
                nameof(dateOfBirth),
                "Date of birth cannot be in the future.");
        }
    }

    private static ContactNumber? CreateOptionalContactNumber(
        string? value,
        string parameterName)
    {
        return string.IsNullOrWhiteSpace(value)
            ? null
            : ContactNumber.Create(value, parameterName);
    }
}
