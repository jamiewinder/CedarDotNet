using System.Text.Json.Serialization;

namespace CedarDotNet.Models;

/// <summary>
/// An entity identifier.
/// </summary>
public sealed record class EntityUid
{
    /// <summary>
    /// Creates an <see cref="EntityUid"/> from the given type and ID.
    /// </summary>
    /// <param name="type">The entity type.</param>
    /// <param name="id">The entity ID.</param>
    /// <returns>The entity UID.</returns>
    public static EntityUid Create(string type, string id)
        => new()
        {
            Type = type,
            Id = id
        };

    /// <summary>
    /// The entity type.
    /// </summary>
    [JsonPropertyName("type")]
    public required string Type { get; init; }

    /// <summary>
    /// The entity ID.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }
}
