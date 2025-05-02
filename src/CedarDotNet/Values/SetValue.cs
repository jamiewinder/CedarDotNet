namespace CedarDotNet.Values;

/// <summary>
/// A Cedar value that represents a set of values.
/// </summary>
/// <param name="Value">The value.</param>
public sealed record class SetValue(
    IReadOnlyCollection<Value> Value)
    : Value
{
    /// <inheritdoc />
    public override string ToString()
        => $"[{Value.Count} items]";
}
