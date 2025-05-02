namespace CedarDotNet.Values;

/// <summary>
/// A Cedar value that represents a record.
/// </summary>
/// <param name="Value">The value.</param>
public sealed record class RecordValue(
    IReadOnlyDictionary<string, Value> Value)
    : Value
{
    /// <inheritdoc />
    public override string ToString()
        => $"[{Value.Count} items]";
}
