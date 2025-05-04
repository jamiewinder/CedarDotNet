using CedarDotNet.Interop;
using CedarDotNet.Models;

namespace CedarDotNet;

/// <summary>
/// The Cedar functions available through the main Cedar FFI.
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
        => FfiUtilities.CallUnaryJson(
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
        => FfiUtilities.CallUnaryJson(
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
        => FfiUtilities.CallUnaryJson(
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
        => FfiUtilities.CallUnaryJson(
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
        => FfiUtilities.CallUnaryJson(
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
        => FfiUtilities.CallUnaryJson(
            input: call,
            func: CedarFfi.IsAuthorized,
            inputTypeInfo: CedarJsonSerializerContext.Default.AuthorizationCall,
            outputTypeInfo: CedarJsonSerializerContext.Default.IAuthorizationAnswer);

    /// <summary>
    /// Gets the language version.
    /// </summary>
    /// <returns>The language version.</returns>
    public static string GetLangVersion()
        => FfiUtilities.CallNullaryStr(
            func: CedarFfi.GetLangVersion);

    /// <summary>
    /// Gets the SDK version.
    /// </summary>
    /// <returns>The SDK version.</returns>
    public static string GetSdkVersion()
        => FfiUtilities.CallNullaryStr(
            func: CedarFfi.GetSdkVersion);
}
