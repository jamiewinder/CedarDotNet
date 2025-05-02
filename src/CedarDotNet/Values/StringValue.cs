namespace CedarDotNet.Values;

/// <summary>
/// A Cedar value that represents a string.
/// </summary>
/// <param name="Value">The value.</param>
public sealed record class StringValue(
    string Value)
    : Value
{
    /// <inheritdoc />
    public override string ToString()
        => Value;
}
