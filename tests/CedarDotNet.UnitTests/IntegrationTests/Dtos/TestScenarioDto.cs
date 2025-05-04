using System.Text.Json.Serialization;

namespace CedarDotNet.UnitTests.IntegrationTests.Dtos;

public sealed class TestScenarioDto
{
    [JsonPropertyName("policies")]
    public required string Policies { get; init; }

    [JsonPropertyName("entities")]
    public required string Entities { get; init; }

    [JsonPropertyName("schema")]
    public required string Schema { get; init; }

    [JsonPropertyName("shouldValidate")]
    public required bool ShouldValidate { get; init; }

    [JsonPropertyName("requests")]
    public required IReadOnlyCollection<TestRequestDto> Requests { get; init; }
}
