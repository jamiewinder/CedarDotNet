using System.Text.Json.Serialization;

namespace CedarDotNet.Models;

/// <summary>
/// The marker interface for check parse answer.
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(CheckParseSuccess), "success")]
[JsonDerivedType(typeof(CheckParseFailure), "failure")]
public interface ICheckParseAnswer
{
    /// <summary>
    /// The answer type.
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; }
}

/// <summary>
/// A successful check parse answer.
/// </summary>
public sealed record class CheckParseSuccess
    : ICheckParseAnswer
{
    /// <inheritdoc />
    [JsonIgnore]
    public string Type => "success";
}

/// <summary>
/// A failed check parse answer.
/// </summary>
public sealed record class CheckParseFailure
    : ICheckParseAnswer
{
    /// <inheritdoc />
    [JsonPropertyName("type")]
    public string Type => "failure";

    /// <summary>
    /// The errors.
    /// </summary>
    [JsonPropertyName("errors")]
    public required IReadOnlyCollection<DetailedError> Errors { get; init; }
}
