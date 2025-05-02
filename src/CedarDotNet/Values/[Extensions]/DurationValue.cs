using System.Text;
using System.Text.RegularExpressions;

namespace CedarDotNet.Values;

/// <summary>
/// The Cedar value that represents a duration.
/// </summary>
public sealed partial record class DurationValue : Value
{
    private static readonly Regex DurationRegex = GenerateDurationRegex();

    private static readonly string[] UnitOrder = ["d", "h", "m", "s", "ms"];

    private static void ValidateValue(TimeSpan value)
    {
        // Currently nothing to do here.
    }

    /// <summary>
    /// Creates a new <see cref="DurationValue"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    public DurationValue(TimeSpan value)
    {
        ValidateValue(value);
        Value = value;
    }

    /// <summary>
    /// The value.
    /// </summary>
    public TimeSpan Value { get; }

    /// <summary>
    /// Encodes the value to the Cedar string format.
    /// </summary>
    /// <returns>The string format.</returns>
    internal string Encode()
    {
        var sb = new StringBuilder();

        if (Value.Days > 0)
        {
            sb.Append($"{Value.Days}d");
        }
        if (Value.Hours > 0)
        {
            sb.Append($"{Value.Hours}h");
        }
        if (Value.Minutes > 0)
        {
            sb.Append($"{Value.Minutes}m");
        }
        if (Value.Seconds > 0)
        {
            sb.Append($"{Value.Seconds}s");
        }
        if (Value.Milliseconds > 0)
        {
            sb.Append($"{Value.Milliseconds}ms");
        }

        return (sb.Length > 0)
            ? sb.ToString()
            : "0s";
    }

    /// <summary>
    /// Decodes the value from the Cedar string format.
    /// </summary>
    /// <param name="text">The string format.</param>
    /// <returns>The value.</returns>
    internal static DurationValue Decode(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentException("Input cannot be null or empty.");
        }

        var matches = DurationRegex.Matches(text);
        if (matches.Count == 0)
        {
            throw new FormatException("No valid duration parts found.");
        }

        var lastUnitIndex = -1;
        var result = TimeSpan.Zero;

        foreach (var groups in matches.Select(x => x.Groups))
        {
            var value = int.Parse(groups["value"].Value);
            var unit = groups["unit"].Value;

            var currentUnitIndex = Array.IndexOf(UnitOrder, unit);

            if (currentUnitIndex == -1)
            {
                throw new FormatException($"Unknown unit '{unit}'.");
            }

            if (currentUnitIndex <= lastUnitIndex)
            {
                throw new FormatException("Units are not in descending order or repeated.");
            }

            lastUnitIndex = currentUnitIndex;

            result += unit switch
            {
                "d" => TimeSpan.FromDays(value),
                "h" => TimeSpan.FromHours(value),
                "m" => TimeSpan.FromMinutes(value),
                "s" => TimeSpan.FromSeconds(value),
                "ms" => TimeSpan.FromMilliseconds(value),
                _ => throw new FormatException($"Unhandled unit '{unit}'.")
            };
        }

        return new(result);
    }

    [GeneratedRegex(@"(?<value>\d+)(?<unit>d|h|m|s|ms)", RegexOptions.Compiled)]
    private static partial Regex GenerateDurationRegex();
}
