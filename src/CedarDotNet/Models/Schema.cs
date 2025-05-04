using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace CedarDotNet.Models;

/// <summary>
/// A Cedar schema.
/// </summary>
[JsonConverter(typeof(SchemaJsonConverter))]
public sealed class Schema
{
    /// <summary>
    /// Creates a schema from Cedar schema grammar.
    /// </summary>
    /// <param name="text">The schema text.</param>
    /// <returns>The schema.</returns>
    public static Schema FromText(string text)
        => new(text, null);

    /// <summary>
    /// Creates a schema from Cedar JSON grammar.
    /// </summary>
    /// <param name="node">The schema node.</param>
    /// <returns>The schema node.</returns>
    public static Schema FromNode(JsonObject node)
        => new(null, node);

    private Schema(
        string? schemaText,
        JsonObject? schemaNode)
    {
        SchemaText = schemaText;
        SchemaNode = schemaNode;
    }

    /// <summary>
    /// The schema text.
    /// </summary>
    public string? SchemaText { get; private set; }

    /// <summary>
    /// The schema node.
    /// </summary>
    public JsonObject? SchemaNode { get; private set; }
}

/// <summary>
/// The JSON converter for <see cref="Schema"/>.
/// </summary>
public sealed class SchemaJsonConverter
    : JsonConverter<Schema>
{
    /// <inheritdoc />
    public override Schema? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var schemaText = reader.GetString()!;
            return Schema.FromText(schemaText);
        }
        if (reader.TokenType == JsonTokenType.StartObject)
        {
            var schemaNode = JsonNode.Parse(ref reader)!.AsObject();

            return Schema.FromNode(schemaNode);
        }
        return null;
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, Schema value, JsonSerializerOptions options)
    {
        if (value.SchemaText is not null)
        {
            writer.WriteStringValue(value.SchemaText);
        }
        else if (value.SchemaNode is not null)
        {
            value.SchemaNode.WriteTo(writer, options);
        }
        else
        {
            writer.WriteNullValue();
        }
    }
}