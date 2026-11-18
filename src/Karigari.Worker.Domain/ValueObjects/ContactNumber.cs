namespace Karigari.Worker.Domain.ValueObjects;

/// <summary>
/// Represents a contact number in a normalized, formatting-free form.
/// Country-specific numbering rules are deliberately not enforced here.
/// </summary>
public sealed record ContactNumber
{
    private ContactNumber(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Gets the normalized number. A leading plus sign is retained when supplied.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Creates a contact number after removing common visual separators.
    /// Accepts 7 to 15 ASCII digits, optionally prefixed with a plus sign.
    /// </summary>
    public static ContactNumber Create(string value, string parameterName = "value")
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, parameterName);

        var input = value.Trim();
        var normalized = new System.Text.StringBuilder(input.Length);
        var hasLeadingPlus = false;

        for (var index = 0; index < input.Length; index++)
        {
            var character = input[index];

            if (character == '+' && index == 0)
            {
                hasLeadingPlus = true;
                continue;
            }

            if (char.IsAsciiDigit(character))
            {
                normalized.Append(character);
                continue;
            }

            if (character is ' ' or '-' or '(' or ')' or '.')
            {
                continue;
            }

            throw new ArgumentException(
                "The contact number contains an unsupported character.",
                parameterName);
        }

        if (normalized.Length is < 7 or > 15)
        {
            throw new ArgumentException(
                "The contact number must contain between 7 and 15 digits.",
                parameterName);
        }

        var result = hasLeadingPlus
            ? $"+{normalized}"
            : normalized.ToString();

        return new ContactNumber(result);
    }

    public override string ToString() => Value;
}
