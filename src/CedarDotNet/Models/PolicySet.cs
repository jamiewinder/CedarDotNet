using System.Collections.Frozen;
using System.Text.Json.Serialization;

namespace CedarDotNet.Models;

/// <summary>
/// A policy set.
/// </summary>
public sealed record class PolicySet
{
    /// <summary>
    /// The static policies, keyed by ID.
    /// </summary>
    [JsonPropertyName("staticPolicies")]
    public IReadOnlyDictionary<string, string> StaticPolicies { get; init; } = FrozenDictionary<string, string>.Empty;

    /// <summary>
    /// The templated policies.
    /// </summary>
    [JsonPropertyName("templates")]
    public IReadOnlyDictionary<string, string> Templates { get; init; } = FrozenDictionary<string, string>.Empty;

    /// <summary>
    /// The templated policy links.
    /// </summary>
    [JsonPropertyName("templateLinks")]
    public IReadOnlyCollection<TemplateLink> TemplateLinks { get; init; } = FrozenSet<TemplateLink>.Empty;
}

/// <summary>
/// A template link.
/// </summary>
public sealed record class TemplateLink
{
    /// <summary>
    /// The ID of the template to link against.
    /// </summary>
    [JsonPropertyName("templateId")]
    public required string TemplateId { get; init; }

    /// <summary>
    /// The generated policy ID.
    /// </summary>
    [JsonPropertyName("newId")]
    public required string NewId { get; init; }

    /// <summary>
    /// The slot values.
    /// </summary>
    [JsonPropertyName("values")]
    public required IReadOnlyDictionary<string, EntityUid> Values { get; init; }
}

/// <summary>
/// A template slot.
/// </summary>
public static class SlotId
{
    /// <summary>
    /// The principal slot.
    /// </summary>
    public static string Principal => "?principal";

    /// <summary>
    /// The resource slot.
    /// </summary>
    public static string Resource => "?resource";
}
