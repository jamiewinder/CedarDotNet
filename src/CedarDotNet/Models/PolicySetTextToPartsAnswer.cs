using System.Text.Json.Serialization;

namespace CedarDotNet.Models;

/// <summary>
/// The marker interface for 'policy set text to parts' answer.
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(PolicySetTextToPartsAnswerSuccess), "success")]
[JsonDerivedType(typeof(PolicySetTextToPartsAnswerFailure), "failure")]
public interface IPolicySetTextToPartsAnswer;

/// <summary>
/// A successful 'policy set text to parts' answer.
/// </summary>
public sealed record class PolicySetTextToPartsAnswerSuccess
    : IPolicySetTextToPartsAnswer
{
    /// <summary>
    /// The policies.
    /// </summary>
    [JsonPropertyName("policies")]
    public required IReadOnlyCollection<string> Policies { get; init; }

    /// <summary>
    /// The policy templates.
    /// </summary>
    [JsonPropertyName("policy_templates")]
    public required IReadOnlyCollection<string> PolicyTemplates { get; init; }
}

/// <summary>
/// A failed 'policy set text to parts' answer.
/// </summary>
public sealed record class PolicySetTextToPartsAnswerFailure
    : IPolicySetTextToPartsAnswer
{
    [JsonPropertyName("errors")]
    public required IReadOnlyCollection<DetailedError> Errors { get; init; }
}