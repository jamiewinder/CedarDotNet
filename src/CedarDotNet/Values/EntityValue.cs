using CedarDotNet.Models;

namespace CedarDotNet.Values;

/// <summary>
/// The Cedar value that represents an entity reference.
/// </summary>
/// <param name="Value">The value.</param>
public sealed record class EntityValue(
    EntityUid Value)
    : Value
{
    /// <inheritdoc />
    public override string ToString()
        => Value.ToString();
}
