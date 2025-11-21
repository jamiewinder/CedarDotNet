using System.Text.Json.Serialization;

namespace CedarDotNet.Models;

/// <summary>
/// The marker interface for authorization answer.
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(AuthorizationAnswerSuccess), "success")]
[JsonDerivedType(typeof(AuthorizationAnswerFailure), "failure")]
public interface IAuthorizationAnswer;

/// <summary>
/// A successful authorization answer.
/// </summary>
public sealed record class AuthorizationAnswerSuccess
    : IAuthorizationAnswer
{
    /// <summary>
    /// The response.
    /// </summary>
    [JsonPropertyName("response")]
    public required Response Response { get; init; }

    /// <summary>
    /// The warnings.
    /// </summary>
    [JsonPropertyName("warnings")]
    public required IReadOnlyCollection<DetailedError> Warnings { get; init; }
}

/// <summary>
/// A failed authorization answer.
/// </summary>
public sealed record class AuthorizationAnswerFailure
    : IAuthorizationAnswer
{
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
/// The response of a <see cref="AuthorizationAnswerSuccess" />.
/// </summary>
public sealed record class Response
{
    /// <summary>
    /// The decision.
    /// </summary>
    [JsonPropertyName("decision")]
    public required Decision Decision { get; init; }

    /// <summary>
    /// The diagnostics.
    /// </summary>
    [JsonPropertyName("diagnostics")]
    public required Diagnostics Diagnostics { get; init; }
}

/// <summary>
/// A decision status.
/// </summary>
public enum Decision
{
    /// <summary>
    /// The allow decision.
    /// </summary>
    [JsonPropertyName("allow")]
    Allow,

    /// <summary>
    /// The deny decision.
    /// </summary>
    [JsonPropertyName("deny")]
    Deny,

    /// <summary>
    /// An unknown / inconclusive decision.
    /// </summary>
    [JsonPropertyName("unknown")]
    Unknown
}

/// <summary>
/// The diagnostics of a <see cref="Response" />.
/// </summary>
public sealed record class Diagnostics
{
    /// <summary>
    /// The reason for the decision.
    /// </summary>
    [JsonPropertyName("reason")]
    public required IReadOnlyCollection<string> Reason { get; init; }

    /// <summary>
    /// The authorization errors.
    /// </summary>
    [JsonPropertyName("errors")]
    public required IReadOnlyCollection<AuthorizationError> Errors { get; init; }
}

/// <summary>
/// An authorization error of a <see cref="Diagnostics" />.
/// </summary>
public sealed record AuthorizationError
{
    /// <summary>
    /// The policy ID.
    /// </summary>
    [JsonPropertyName("policyId")]
    public required string PolicyId { get; init; }

    /// <summary>
    /// The error.
    /// </summary>
    [JsonPropertyName("error")]
    public required DetailedError Error { get; init; }
}
