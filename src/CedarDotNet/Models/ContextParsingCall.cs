using CedarDotNet.Values;
using System.Text.Json.Serialization;

namespace CedarDotNet.Models;

/// <summary>
/// The context parsing call.
/// </summary>
public sealed record class ContextParsingCall
{
    /// <summary>
    /// The context.
    /// </summary>
    [JsonPropertyName("context")]
    public required IReadOnlyDictionary<string, Value> Context { get; init; }

    /// <summary>
    /// The schema.
    /// </summary>
    [JsonPropertyName("schema")]
    public Schema? Schema { get; init; } = null;

    /// <summary>
    /// The action.
    /// </summary>
    [JsonPropertyName("action")]
    public EntityUid? Action { get; init; } = null;
}
