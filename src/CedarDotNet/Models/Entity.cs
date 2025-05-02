using CedarDotNet.Values;
using System.Collections.Frozen;
using System.Text.Json.Serialization;

namespace CedarDotNet.Models;

/// <summary>
/// An entity.
/// </summary>
public sealed record class Entity
{
    /// <summary>
    /// The entity UID.
    /// </summary>
    [JsonPropertyName("uid")]
    public required EntityUid Uid { get; init; }

    /// <summary>
    /// The attributes.
    /// </summary>
    [JsonPropertyName("attrs")]
    public IReadOnlyDictionary<string, Value> Attrs { get; init; } = FrozenDictionary<string, Value>.Empty;

    /// <summary>
    /// The references to the parent entities.
    /// </summary>
    [JsonPropertyName("parents")]
    public IReadOnlyCollection<EntityUid> Parents { get; init; } = FrozenSet<EntityUid>.Empty;

    /// <summary>
    /// The tags.
    /// </summary>
    [JsonPropertyName("tags")]
    public IReadOnlyDictionary<string, Value> Tags { get; init; } = FrozenDictionary<string, Value>.Empty;

}
