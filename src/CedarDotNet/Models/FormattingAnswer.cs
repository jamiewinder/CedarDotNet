using System.Text.Json.Serialization;

namespace CedarDotNet.Models;

/// <summary>
/// The marker interface for formatting answer.
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(FormattingAnswerSuccess), "success")]
[JsonDerivedType(typeof(FormattingAnswerFailure), "failure")]
public interface IFormattingAnswer
{
    /// <summary>
    /// The answer type.
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; }
}

/// <summary>
/// A successful formatting answer.
/// </summary>
public sealed record class FormattingAnswerSuccess
    : IFormattingAnswer
{
    /// <inheritdoc />
    [JsonIgnore]
    public string Type => "success";

    /// <summary>
    /// The formatted policy.
    /// </summary>
    [JsonPropertyName("formatted_policy")]
    public required string FormattedPolicy { get; init; }
}

/// <summary>
/// A failed authorization answer.
/// </summary>
public sealed record class FormattingAnswerFailure
    : IFormattingAnswer
{
    /// <inheritdoc />
    [JsonIgnore]
    public string Type => "failure";

    /// <summary>
    /// The errors.
    /// </summary>
    [JsonPropertyName("errors")]
    public required IReadOnlyCollection<DetailedError> Errors { get; init; }
}
