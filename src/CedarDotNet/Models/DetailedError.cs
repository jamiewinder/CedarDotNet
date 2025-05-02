using System.Text.Json.Serialization;

namespace CedarDotNet.Models;

/// <summary>
/// A detailed error.
/// </summary>
public sealed record class DetailedError
{
    /// <summary>
    /// The message.
    /// </summary>
    [JsonPropertyName("message")]
    public required string Message { get; init; }

    /// <summary>
    /// The help comment.
    /// </summary>
    [JsonPropertyName("help")]
    public string? Help { get; init; }

    /// <summary>
    /// The error code.
    /// </summary>
    [JsonPropertyName("code")]
    public string? Code { get; init; }

    /// <summary>
    /// A URL to a page with more information about the error.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; init; }

    /// <summary>
    /// The severity.
    /// </summary>
    [JsonPropertyName("severity")]
    public Severity? Severity { get; init; }

    /// <summary>
    /// The source locations.
    /// </summary>
    [JsonPropertyName("sourceLocations")]
    public IReadOnlyCollection<SourceLabel> SourceLocations { get; init; } = [];

    /// <summary>
    /// The related errors.
    /// </summary>
    [JsonPropertyName("related")]
    public IReadOnlyCollection<DetailedError> Related { get; init; } = [];
}

/// <summary>
/// An error severity.
/// </summary>
public enum Severity
{
    /// <summary>
    /// An advisory.
    /// </summary>
    [JsonPropertyName("advice")]
    Advice,

    /// <summary>
    /// A warning.
    /// </summary>
    [JsonPropertyName("warning")]
    Warning,

    /// <summary>
    /// An error.
    /// </summary>
    [JsonPropertyName("error")]
    Error
}

/// <summary>
/// A source label.
/// </summary>
public sealed record class SourceLabel
{
    /// <summary>
    /// The label.
    /// </summary>
    [JsonPropertyName("label")]
    public string? Label { get; init; }

    /// <summary>
    /// The start position.
    /// </summary>
    [JsonPropertyName("start")]
    public required long Start { get; init; }

    /// <summary>
    /// The end position.
    /// </summary>
    [JsonPropertyName("end")]
    public required long End { get; init; }
}
