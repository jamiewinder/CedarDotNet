using CedarDotNet.Models;
using CedarDotNet.Values;
using Shouldly;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CedarDotNet.UnitTests.Values;

public sealed class ValueJsonConverterTests
{
    [Fact]
    public void Values_SerializeAndDeserialize()
    {
        // Arrange
        var values = new Dictionary<string, Value>
        {
            { "true", true },
            { "false", false },
            { "string", "A string" },
            { "long", 123 },
            { "set", new List<Value>()
            {
                true,
                false,
                123,
                "A set string"
            } },
            { "record", new Dictionary<string, Value>()
            {
                { "true", true },
                { "false", false },
                { "string", "A record string" }
            } },
            { "entity", EntityUid.Create("User", "test") },
            { "datetime", DateTimeOffset.UnixEpoch },
            { "decimal", 123.456m },
            { "duration", TimeSpan.FromMinutes(10) },
            { "ipaddr", IPAddress.Loopback },
            { "ipaddrv6", IPAddress.IPv6Loopback }
        };

        // Act
        var serializeResult = JsonSerializer.Serialize(
            value: values,
            jsonTypeInfo: TestsJsonSerializerContext.Default.IReadOnlyDictionaryStringValue);

        var deserializeResult = JsonSerializer.Deserialize(
            json: serializeResult,
            jsonTypeInfo: TestsJsonSerializerContext.Default.IReadOnlyDictionaryStringValue)!;

        // Assert
        deserializeResult.Count.ShouldBe(values.Count);

        foreach (var (key, value) in values)
        {
            var deserializedValue = deserializeResult[key];

            deserializedValue.ShouldBeEquivalentTo(value);
        }
    }
}

[JsonSerializable(typeof(IReadOnlyDictionary<string, Value>))]
[JsonSourceGenerationOptions(UseStringEnumConverter = true)]
internal sealed partial class TestsJsonSerializerContext
    : JsonSerializerContext;