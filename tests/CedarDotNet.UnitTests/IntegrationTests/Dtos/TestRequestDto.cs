using CedarDotNet.Models;
using CedarDotNet.Values;
using System.Text.Json.Serialization;

namespace CedarDotNet.UnitTests.IntegrationTests.Dtos;

public sealed class TestRequestDto
{
    [JsonPropertyName("description")]
    public required string Description { get; init; }

    [JsonPropertyName("principal")]
    public required EntityUid Principal { get; init; }

    [JsonPropertyName("action")]
    public required EntityUid Action { get; init; }

    [JsonPropertyName("resource")]
    public required EntityUid Resource { get; init; }

    [JsonPropertyName("context")]
    public required IReadOnlyDictionary<string, Value> Context { get; init; }

    [JsonPropertyName("decision")]
    public required Decision Decision { get; init; }

    [JsonPropertyName("reason")]
    public required IReadOnlyCollection<string> Reason { get; init; }

    [JsonPropertyName("errors")]
    public required IReadOnlyCollection<string> Errors { get; init; }
}