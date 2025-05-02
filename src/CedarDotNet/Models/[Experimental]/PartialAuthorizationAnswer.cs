using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace CedarDotNet.Models;

/// <summary>
/// The marker interface for partial authorization answer.
/// </summary>
[Experimental(Experimental.CedarPartialExpressions)]
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(PartialAuthorizationAnswerResiduals), "residuals")]
[JsonDerivedType(typeof(PartialAuthorizationAnswerFailure), "failure")]
public interface IPartialAuthorizationAnswer
{
    /// <summary>
    /// The answer type.
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; }
}

/// <summary>
/// A residuals partial authorization answer.
/// </summary>
[Experimental(Experimental.CedarPartialExpressions)]
public sealed record class PartialAuthorizationAnswerResiduals
    : IPartialAuthorizationAnswer
{
    /// <inheritdoc />
    [JsonIgnore]
    public string Type => "residuals";

    /// <summary>
    /// The response.
    /// </summary>
    [JsonPropertyName("response")]
    public required ResidualResponse Response { get; init; }

    /// <summary>
    /// The warnings.
    /// </summary>
    [JsonPropertyName("warnings")]
    public required IReadOnlyCollection<DetailedError> Warnings { get; init; }
}

/// <summary>
/// A failed partial authorization answer.
/// </summary>
[Experimental(Experimental.CedarPartialExpressions)]
public sealed record class PartialAuthorizationAnswerFailure
    : IPartialAuthorizationAnswer
{
    /// <inheritdoc />
    [JsonPropertyName("type")]
    public string Type => "failure";

    /// <summary>
    /// The errors.
    /// </summary>
    [JsonPropertyName("errors")]
    public required IReadOnlyCollection<DetailedError> Errors { get; init; }

    /// <summary>
    /// The warnings.
    /// </summary>
    [JsonPropertyName("warnings")]
    public required IReadOnlyCollection<DetailedError> Warnings { get; init; }
}

/// <summary>
/// The response of a <see cref="PartialAuthorizationAnswerResiduals" />.
/// </summary>
[Experimental(Experimental.CedarPartialExpressions)]
public sealed record class ResidualResponse
{
    /// <summary>
    /// The decision.
    /// </summary>
    [JsonPropertyName("decision")]
    public required Decision? Decision { get; init; }

    /// <summary>
    /// The satisfied policies.
    /// </summary>
    [JsonPropertyName("satisfied")]
    public required IReadOnlyCollection<string> Satisfied { get; init; }

    /// <summary>
    /// The errored policies.
    /// </summary>
    [JsonPropertyName("errored")]
    public required IReadOnlyCollection<string> Errored { get; init; }

    /// <summary>
    /// The may-be-determining policies.
    /// </summary>
    [JsonPropertyName("mayBeDetermining")]
    public required IReadOnlyCollection<string> MayBeDetermining { get; init; }

    /// <summary>
    /// The must-be-determining policies.
    /// </summary>
    [JsonPropertyName("mustBeDetermining")]
    public required IReadOnlyCollection<string> MustBeDetermining { get; init; }

    /// <summary>
    /// The residuals. TODO: determine value type.
    /// </summary>
    [JsonPropertyName("residuals")]
    public required IReadOnlyDictionary<string, JsonObject> Residuals { get; init; }

    /// <summary>
    /// The non-trivial residuals.
    /// </summary>
    [JsonPropertyName("nontrivialResiduals")]
    public required IReadOnlyCollection<string> NontrivialResiduals { get; init; }
}
