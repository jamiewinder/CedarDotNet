using System.Text.Json.Serialization;

namespace CedarDotNet.Models;

/// <summary>
/// The marker interface for check parse answer.
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(CheckParseSuccess), "success")]
[JsonDerivedType(typeof(CheckParseFailure), "failure")]
public interface ICheckParseAnswer;

/// <summary>
/// A successful check parse answer.
/// </summary>
public sealed record class CheckParseSuccess
    : ICheckParseAnswer;

/// <summary>
/// A failed check parse answer.
/// </summary>
public sealed record class CheckParseFailure
    : ICheckParseAnswer
{
    /// <summary>
    /// The errors.
    /// </summary>
    [JsonPropertyName("errors")]
    public required IReadOnlyCollection<DetailedError> Errors { get; init; }
}
