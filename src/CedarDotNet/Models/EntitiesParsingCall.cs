using System.Text.Json.Serialization;

namespace CedarDotNet.Models;

/// <summary>
/// The entity parsing call.
/// </summary>
public sealed record class EntitiesParsingCall
{
    /// <summary>
    /// The entities.
    /// </summary>
    [JsonPropertyName("entities")]
    public required IReadOnlyCollection<Entity> Entities { get; init; }

    /// <summary>
    /// The schema.
    /// </summary>
    [JsonPropertyName("schema")]
    public Schema? Schema { get; init; } = null;
}
