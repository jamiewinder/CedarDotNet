using CedarDotNet.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CedarDotNet.Values;

/// <summary>
/// The JSON converter for <see cref="Value"/>.
/// </summary>
[JsonConverter(typeof(ValueJsonConverter))]
public sealed class ValueJsonConverter
    : JsonConverter<Value>
{
    /// <inheritdoc />
    public override Value? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => reader.TokenType switch
        {
            JsonTokenType.True => BoolValue.True,
            JsonTokenType.False => BoolValue.False,
            JsonTokenType.String => new StringValue(reader.GetString()!),
            JsonTokenType.Number => new LongValue(reader.GetInt64()),
            JsonTokenType.StartArray => ReadArray(ref reader, options),
            JsonTokenType.StartObject => ReadObject(ref reader, options),
            _ => throw new JsonException($"Unexpected token {reader.TokenType} when parsing value.")
        };

    private SetValue ReadArray(ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        var items = new List<Value>();
        while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
        {
            items.Add(Read(ref reader, typeof(Value), options)!);
        }
        return new SetValue(items);
    }

    private Value ReadObject(
        ref Utf8JsonReader reader,
        JsonSerializerOptions options)
    {
        using var document = JsonDocument.ParseValue(ref reader);
        var root = document.RootElement;

        if (root.TryGetProperty("__entity", out var entity))
        {
            return ParseEntity(entity);
        }

        if (root.TryGetProperty("__extn", out var extn))
        {
            return ParseExtension(extn);
        }

        return ParseRecord(root, options);
    }

    private static EntityValue ParseEntity(JsonElement entity)
    {
        var type = entity.GetProperty("type").GetString()!;
        var id = entity.GetProperty("id").GetString()!;
        return new EntityValue(EntityUid.Create(type, id));
    }

    private static Value ParseExtension(JsonElement extn)
    {
        var fn = extn.GetProperty("fn").GetString()!;
        var arg = extn.GetProperty("arg").GetString()!;

        return fn switch
        {
            "decimal" => DecimalValue.Decode(arg),
            "datetime" => DateTimeValue.Decode(arg),
            "duration" => DurationValue.Decode(arg),
            "ip" => IpAddrValue.Decode(arg),
            _ => throw new NotSupportedException($"Unsupported extension type: {fn}")
        };
    }

    private RecordValue ParseRecord(JsonElement root, JsonSerializerOptions options)
    {
        var items = new Dictionary<string, Value>();

        foreach (var prop in root.EnumerateObject())
        {
            var json = prop.Value.GetRawText();
            var valueReader = new Utf8JsonReader(System.Text.Encoding.UTF8.GetBytes(json));
            valueReader.Read();

            var item = Read(ref valueReader, typeof(Value), options);

            items[prop.Name] = item!;
        }

        return new RecordValue(items);
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, Value value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case BoolValue boolValue:
                writer.WriteBooleanValue(boolValue.Value);
                break;
            case StringValue stringValue:
                writer.WriteStringValue(stringValue.Value);
                break;
            case LongValue longValue:
                writer.WriteNumberValue(longValue.Value);
                break;
            case SetValue setValue:
                WriteSet(writer, options, setValue);
                break;
            case RecordValue recordValue:
                WriteRecord(writer, options, recordValue);
                break;
            case EntityValue entityValue:
                WriteEntity(writer, entityValue.Value);
                break;
            case DecimalValue decimalValue:
                WriteExtensionType(writer, "decimal", decimalValue.Encode());
                break;
            case DateTimeValue dateTimeValue:
                WriteExtensionType(writer, "datetime", dateTimeValue.Encode());
                break;
            case DurationValue durationValue:
                WriteExtensionType(writer, "duration", durationValue.Encode());
                break;
            case IpAddrValue ipAddrValue:
                WriteExtensionType(writer, "ip", ipAddrValue.Encode());
                break;
            default:
                throw new NotSupportedException($"Unsupported value type: {value.GetType()}");
        }
    }

    private void WriteSet(
        Utf8JsonWriter writer,
        JsonSerializerOptions options,
        SetValue setValue)
    {
        writer.WriteStartArray();
        foreach (var item in setValue.Value)
        {
            Write(writer, item, options);
        }
        writer.WriteEndArray();
    }

    private void WriteRecord(
        Utf8JsonWriter writer,
        JsonSerializerOptions options,
        RecordValue recordValue)
    {
        writer.WriteStartObject();

        foreach (var kvp in recordValue.Value)
        {
            writer.WritePropertyName(kvp.Key);
            Write(writer, kvp.Value, options);
        }

        writer.WriteEndObject();
    }

    private static void WriteEntity(
        Utf8JsonWriter writer,
        EntityUid entityUid)
    {
        writer.WriteStartObject();
        writer.WriteStartObject("__entity");
        writer.WriteString("type", entityUid.Type);
        writer.WriteString("id", entityUid.Id);
        writer.WriteEndObject();
        writer.WriteEndObject();
    }

    private static void WriteExtensionType(
        Utf8JsonWriter writer,
        string fn,
        string arg)
    {
        writer.WriteStartObject();
        writer.WriteStartObject("__extn");
        writer.WriteString("fn", fn);
        writer.WriteString("arg", arg);
        writer.WriteEndObject();
        writer.WriteEndObject();
    }
}