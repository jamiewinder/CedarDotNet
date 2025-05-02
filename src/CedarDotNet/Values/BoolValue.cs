namespace CedarDotNet.Values;

/// <summary>
/// A Cedar value that represents a boolean.
/// </summary>
public sealed record class BoolValue : Value
{
    /// <summary>
    /// Gets the <see cref="BoolValue"/> that represents <see langword="true" />.
    /// </summary>
    public static BoolValue True => new(true);

    /// <summary>
    /// Gets the <see cref="BoolValue"/> that represents <see langword="false" />.
    /// </summary>
    public static BoolValue False => new(false);

    /// <summary>
    /// Returns the <see cref="BoolValue"/> that represents the given boolean value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// The <see cref="BoolValue"/> that represents the given boolean value.
    /// </returns>
    public static BoolValue For(bool value)
        => value ? True : False;

    private BoolValue(bool value)
    {
        Value = value;
    }

    /// <summary>
    /// The value.
    /// </summary>
    public bool Value { get; }

    /// <inheritdoc />
    public override string ToString()
        => Value.ToString();
}
