using System.Text.Json.Serialization;

namespace CedarDotNet.Models;

/// <summary>
/// The formatting call.
/// </summary>
public sealed record class FormattingCall
{
    /// <summary>
    /// The policy text.
    /// </summary>
    [JsonPropertyName("policyText")]
    public required string PolicyText { get; init; }

    /// <summary>
    /// The line width.
    /// </summary>
    [JsonPropertyName("lineWidth")]
    public required int LineWidth { get; init; }

    /// <summary>
    /// The indent width.
    /// </summary>
    [JsonPropertyName("indentWidth")]
    public required int IndentWidth { get; init; }
}
