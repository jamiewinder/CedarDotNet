namespace CedarDotNet.Values;

/// <summary>
/// The Cedar value that represents an IP address.
/// </summary>
public sealed record class IpAddrValue : Value
{
    private static void ValidateValue(string value)
    {
        // TODO: Need to figure this out.
    }

    /// <summary>
    /// Creates a new <see cref="IpAddrValue"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    public IpAddrValue(string value)
    {
        ValidateValue(value);
        Value = value;
    }

    /// <summary>
    /// The value.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Encodes the value to the Cedar string format.
    /// </summary>
    /// <returns>The string format.</returns>
    internal string Encode()
        => Value;

    /// <summary>
    /// Decodes the value from the Cedar string format.
    /// </summary>
    /// <param name="text">The string format.</param>
    /// <returns>The value.</returns>
    internal static IpAddrValue Decode(string text)
        => new(text);
}
