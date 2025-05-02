using System.Globalization;

namespace CedarDotNet.Values;

/// <summary>
/// The Cedar value that represents a date and time.
/// </summary>
public sealed record class DateTimeValue : Value
{
    private static readonly string[] SupportedFormats = [
        "yyyy-MM-dd",                        // Date only
        "yyyy-MM-ddTHH:mm:ssZ",              // UTC
        "yyyy-MM-ddTHH:mm:ss.fffZ",          // UTC with milliseconds
        "yyyy-MM-ddTHH:mm:sszzz",            // With timezone offset
        "yyyy-MM-ddTHH:mm:ss.fffzzz"         // With milliseconds and offset
    ];

    private static void ValidateValue(DateTimeOffset value)
    {
        // Currently nothing to do here.
    }

    /// <summary>
    /// Creates a new <see cref="DateTimeValue"/> instance.
    /// </summary>
    /// <param name="value">The value.</param>
    public DateTimeValue(DateTimeOffset value)
    {
        ValidateValue(value);
        Value = value;
    }

    /// <summary>
    /// The value.
    /// </summary>
    public DateTimeOffset Value { get; }

    /// <summary>
    /// Encodes the value to the Cedar string format.
    /// </summary>
    /// <returns>The string format.</returns>
    internal string Encode()
        => Value.ToString("yyyy-MM-dd'T'HH:mm:ss.fff") + Value.ToString("zzz").Replace(":", "");

    /// <summary>
    /// Decodes the value from the Cedar string format.
    /// </summary>
    /// <param name="text">The string format.</param>
    /// <returns>The value.</returns>
    internal static DateTimeValue Decode(string text)
    {
        // Normalise +0100 to +01:00 to match zzz
        text = text.Trim();

        if (text.Length > 5 && (text[^5] == '+' || text[^5] == '-'))
        {
            text = text[..^5] + text[^5..^2] + ":" + text[^2..];
        }

        if (!DateTimeOffset.TryParseExact(
            input: text,
            formats: SupportedFormats,
            formatProvider: CultureInfo.InvariantCulture,
            styles: DateTimeStyles.AssumeUniversal,
            result: out var value))
        {
            throw new FormatException($"Invalid date format: {text}");
        }

        return new(value);
    }
}
