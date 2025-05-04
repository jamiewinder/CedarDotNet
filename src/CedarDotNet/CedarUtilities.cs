using CedarDotNet.Interop;

namespace CedarDotNet;

/// <summary>
/// Additional Cedar utilities which aren't directly part of the FFI.
/// </summary>
public static class CedarUtilities
{
    /// <summary>
    /// Converts a policy from text to JSON grammar.
    /// </summary>
    /// <param name="policyText">The policy text.</param>
    /// <returns>The policy JSON.</returns>
    public static string PolicyFormatTextToJson(
        string policyText)
        => FfiUtilities.CallUnaryStr(
            policyText,
            CedarFfi.PolicyFormatTextToJson);

    /// <summary>
    /// Converts a policy from JSON to text grammar.
    /// </summary>
    /// <param name="policyJson">The policy JSON.</param>
    /// <returns>The policy text.</returns>
    public static string PolicyFormatJsonToText(
        string policyJson)
        => FfiUtilities.CallUnaryStr(
            policyJson,
            CedarFfi.PolicyFormatJsonToText);
}
