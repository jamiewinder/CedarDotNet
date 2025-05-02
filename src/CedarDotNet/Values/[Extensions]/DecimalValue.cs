namespace CedarDotNet.Values;

/// <summary>
/// The Cedar value that represents a decimal.
/// </summary>
public sealed record class DecimalValue : Value
{
    private static readonly decimal MinValue = -922337203685477.5808m;
    private static readonly decimal MaxValue = 922337203685477.5807m;

    private static void ValidateValue(decimal value)
    {
        if (value < MinValue || value > MaxValue)
        {
            throw new FormatException($"The decimal value must be between {MinValue} and {MaxValue}.");
        }

        if (value.Scale > 4)
        {
            throw new FormatException("The scale of the decimal may not be greater than 4 digits.");
        }
    }

    /// <summary>
    /// Creates a new <see cref="DecimalValue"/> instance.
    /// </summary>
    /// <param name="value">The value.</param>
    public DecimalValue(decimal value)
    {
        ValidateValue(value);
        Value = value;
    }

    /// <summary>
    /// The value.
    /// </summary>
    public decimal Value { get; }

    /// <summary>
    /// Encodes the value to the Cedar string format.
    /// </summary>
    /// <returns>The string format.</returns>
    internal string Encode()
        => Value.ToString("G");

    /// <summary>
    /// Decodes the value from the Cedar string format.
    /// </summary>
    /// <param name="text">The string format.</param>
    /// <returns>The value.</returns>
    internal static DecimalValue Decode(string text)
    {
        if (!decimal.TryParse(text, out var value))
        {
            throw new ArgumentException($"Invalid decimal value: {text}", nameof(text));
        }

        return new(value);
    }
}
