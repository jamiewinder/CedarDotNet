namespace CedarDotNet.Values;

/// <summary>
/// A Cedar value that represents a long.
/// </summary>
/// <param name="Value">The value.</param>
public sealed record class LongValue(
    long Value)
    : Value
{
    /// <inheritdoc />
    public override string ToString()
        => Value.ToString();
}
