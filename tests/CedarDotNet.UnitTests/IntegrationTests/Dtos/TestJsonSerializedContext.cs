using CedarDotNet.Models;
using System.Text.Json.Serialization;

namespace CedarDotNet.UnitTests.IntegrationTests.Dtos;

[JsonSerializable(typeof(TestScenarioDto))]
[JsonSerializable(typeof(IReadOnlyCollection<Entity>))]
[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower, UseStringEnumConverter = true)]
internal sealed partial class TestJsonSerializedContext
    : JsonSerializerContext;