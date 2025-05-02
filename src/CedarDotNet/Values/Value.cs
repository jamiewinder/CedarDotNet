using CedarDotNet.Models;
using System.Collections.Frozen;
using System.Collections.Immutable;
using System.Net;
using System.Text.Json.Serialization;

namespace CedarDotNet.Values;

/// <summary>
/// The base class for a Cedar value.
/// </summary>
[JsonConverter(typeof(ValueJsonConverter))]
public abstract record class Value
{
    /// <summary>
    /// Converts a string to a Value type using an implicit operator.
    /// </summary>
    /// <param name="value">The string.</param>
    public static implicit operator Value(string value) => new StringValue(value);

    /// <summary>
    /// Converts a boolean to a Value type using an implicit operator.
    /// </summary>
    /// <param name="value">The boolean.</param>
    public static implicit operator Value(bool value) => BoolValue.For(value);

    /// <summary>
    /// Converts a long to a Value type using an implicit operator.
    /// </summary>
    /// <param name="value">The long.</param>
    public static implicit operator Value(long value) => new LongValue(value);

    /// <summary>
    /// Converts a list to a Value type using an implicit operator.
    /// </summary>
    /// <param name="value">The list.</param>
    public static implicit operator Value(List<Value> value) => new SetValue(value);

    /// <summary>
    /// Converts an array to a Value type using an implicit operator.
    /// </summary>
    /// <param name="value">The array.</param>
    public static implicit operator Value(Value[] value) => new SetValue(value);

    /// <summary>
    /// Converts a dictionary to a Value type using an implicit operator.
    /// </summary>
    /// <param name="value">The dictionary.</param>
    public static implicit operator Value(Dictionary<string, Value> value) => new RecordValue(value);

    /// <summary>
    /// Converts an immutable dictionary to a Value type using an implicit operator.
    /// </summary>
    /// <param name="value">The dictionary.</param>
    public static implicit operator Value(ImmutableDictionary<string, Value> value) => new RecordValue(value);

    /// <summary>
    /// Converts an frozen dictionary to a Value type using an implicit operator.
    /// </summary>
    /// <param name="value">The dictionary.</param>
    public static implicit operator Value(FrozenDictionary<string, Value> value) => new RecordValue(value);

    /// <summary>
    /// Converts an entity reference to a Value type using an implicit operator.
    /// </summary>
    /// <param name="value">The entity reference.</param>
    public static implicit operator Value(EntityUid value) => new EntityValue(value);

    /// <summary>
    /// Converts a date time offset to a Value type using an implicit operator.
    /// </summary>
    /// <param name="value">The date time offset.</param>
    public static implicit operator Value(DateTimeOffset value) => new DateTimeValue(value);

    /// <summary>
    /// Converts a decimal offset to a Value type using an implicit operator.
    /// </summary>
    /// <param name="value">The decimal.</param>
    public static implicit operator Value(decimal value) => new DecimalValue(value);

    /// <summary>
    /// Converts a timespan to a Value type using an implicit operator.
    /// </summary>
    /// <param name="value">The time span.</param>
    public static implicit operator Value(TimeSpan value) => new DurationValue(value);

    /// <summary>
    /// Converts an IP address to a Value type using an implicit operator.
    /// </summary>
    /// <param name="value">The IP address.</param>
    public static implicit operator Value(IPAddress value) => new IpAddrValue(value.ToString());
}
