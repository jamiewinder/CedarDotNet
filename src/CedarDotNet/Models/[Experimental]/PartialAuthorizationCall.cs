using CedarDotNet.Values;
using System.Collections.Frozen;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace CedarDotNet.Models;

/// <summary>
/// The partial authorization call.
/// </summary>
[Experimental(Experimental.CedarPartialExpressions)]
public sealed record class PartialAuthorizationCall
{
    /// <summary>
    /// The principal.
    /// </summary>
    [JsonPropertyName("principal")]
    public required EntityUid Principal { get; init; }

    /// <summary>
    /// The action.
    /// </summary>
    [JsonPropertyName("action")]
    public required EntityUid Action { get; init; }

    /// <summary>
    /// The resource.
    /// </summary>
    [JsonPropertyName("resource")]
    public required EntityUid Resource { get; init; }

    /// <summary>
    /// The context.
    /// </summary>
    [JsonPropertyName("context")]
    public IReadOnlyDictionary<string, Value> Context { get; init; } = FrozenDictionary<string, Value>.Empty;

    /// <summary>
    /// The schema.
    /// </summary>
    [JsonPropertyName("schema")]
    public required Schema? Schema { get; init; }

    /// <summary>
    /// Whether to validate the request.
    /// </summary>
    [JsonPropertyName("validateRequest")]
    public required bool ValidateRequest { get; init; }

    /// <summary>
    /// The policies.
    /// </summary>
    [JsonPropertyName("policies")]
    public required PolicySet Policies { get; init; }

    /// <summary>
    /// The entities.
    /// </summary>
    [JsonPropertyName("entities")]
    public required IReadOnlyCollection<Entity> Entities { get; init; }
}
