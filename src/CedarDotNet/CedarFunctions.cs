using CedarDotNet.Models;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace CedarDotNet;

/// <summary>
/// The Cedar functions.
/// </summary>
public static partial class CedarFunctions
{
    /// <summary>
    /// Checks parsing a policy set.
    /// </summary>
    /// <param name="policySet">The policy set.</param>
    /// <returns>The check parse answer.</returns>
    public static ICheckParseAnswer CheckParsePolicySet(
        PolicySet policySet)
        => WrapJsonCall(
            input: policySet,
            func: CedarFfi.CheckParsePolicySet,
            inputTypeInfo: CedarJsonSerializerContext.Default.PolicySet,
            outputTypeInfo: CedarJsonSerializerContext.Default.ICheckParseAnswer);

    /// <summary>
    /// Checks parsing a schema.
    /// </summary>
    /// <param name="schema">The schema.</param>
    /// <returns>The check parse answer.</returns>
    public static ICheckParseAnswer CheckParseSchema(
        Schema schema)
        => WrapJsonCall(
            input: schema,
            func: CedarFfi.CheckParseSchema,
            inputTypeInfo: CedarJsonSerializerContext.Default.Schema,
            outputTypeInfo: CedarJsonSerializerContext.Default.ICheckParseAnswer);

    /// <summary>
    /// Checks parsing a set of entities.
    /// </summary>
    /// <param name="call">The call.</param>
    /// <returns>The check parse answer.</returns>
    public static ICheckParseAnswer CheckParseEntities(
        EntitiesParsingCall call)
        => WrapJsonCall(
            input: call,
            func: CedarFfi.CheckParseEntities,
            inputTypeInfo: CedarJsonSerializerContext.Default.EntitiesParsingCall,
            outputTypeInfo: CedarJsonSerializerContext.Default.ICheckParseAnswer);

    /// <summary>
    /// Checks parsing context.
    /// </summary>
    /// <param name="call">The call.</param>
    /// <returns>The check parse answer.</returns>
    public static ICheckParseAnswer CheckParseContext(
        ContextParsingCall call)
        => WrapJsonCall(
            input: call,
            func: CedarFfi.CheckParseContext,
            inputTypeInfo: CedarJsonSerializerContext.Default.ContextParsingCall,
            outputTypeInfo: CedarJsonSerializerContext.Default.ICheckParseAnswer);

    /// <summary>
    /// Formats a policy.
    /// </summary>
    /// <param name="call">The formatting call.</param>
    /// <returns>The formatting answer.</returns>
    public static IFormattingAnswer Format(
        FormattingCall call)
        => WrapJsonCall(
            input: call,
            func: CedarFfi.Format,
            inputTypeInfo: CedarJsonSerializerContext.Default.FormattingCall,
            outputTypeInfo: CedarJsonSerializerContext.Default.IFormattingAnswer);

    /// <summary>
    /// Evaluates an authorization query.
    /// </summary>
    /// <param name="call">The authorization call.</param>
    /// <returns>The authorization answer.</returns>
    public static IAuthorizationAnswer IsAuthorized(
        AuthorizationCall call)
        => WrapJsonCall(
            input: call,
            func: CedarFfi.IsAuthorized,
            inputTypeInfo: CedarJsonSerializerContext.Default.AuthorizationCall,
            outputTypeInfo: CedarJsonSerializerContext.Default.IAuthorizationAnswer);

    /// <summary>
    /// Gets the language version.
    /// </summary>
    /// <returns>The language version.</returns>
    public static string GetLangVersion()
    {
        var resultPtr = CedarFfi.GetLangVersion();

        try
        {
            return Marshal.PtrToStringUTF8(resultPtr)!;
        }
        finally
        {
            CedarFfi.FreeString(resultPtr);
        }
    }

    /// <summary>
    /// Gets the SDK version.
    /// </summary>
    /// <returns>The SDK version.</returns>
    public static string GetSdkVersion()
    {
        var resultPtr = CedarFfi.GetSdkVersion();

        try
        {
            return Marshal.PtrToStringUTF8(resultPtr)!;
        }
        finally
        {
            CedarFfi.FreeString(resultPtr);
        }
    }

    private static TOutput WrapJsonCall<TInput, TOutput>(
        TInput input,
        Func<string, nint> func,
        JsonTypeInfo<TInput> inputTypeInfo,
        JsonTypeInfo<TOutput> outputTypeInfo)
    {
        ArgumentNullException.ThrowIfNull(input);
        ArgumentNullException.ThrowIfNull(func);
        ArgumentNullException.ThrowIfNull(inputTypeInfo);
        ArgumentNullException.ThrowIfNull(outputTypeInfo);

        var inputJson = JsonSerializer.Serialize(
            value: input,
            jsonTypeInfo: inputTypeInfo);

        var resultPtr = func(inputJson);

        try
        {
            var resultJson = Marshal.PtrToStringUTF8(resultPtr)!;

            return JsonSerializer.Deserialize(
                json: resultJson,
                jsonTypeInfo: outputTypeInfo)!;
        }
        finally
        {
            CedarFfi.FreeString(resultPtr);
        }
    }
}
