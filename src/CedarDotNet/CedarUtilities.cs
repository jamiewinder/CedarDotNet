using CedarDotNet.Interop;
using CedarDotNet.Models;
using System.Runtime.InteropServices;
using System.Text.Json;

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

    /// <summary>
    /// Loads a policy set from the given text.
    /// </summary>
    /// <param name="text">The policies text.</param>
    /// <returns>The policy collection.</returns>
    public static IReadOnlyCollection<string> LoadPolicySet(
        string text)
    {
        var result = CedarFfi.LoadPolicySet(text);

        try
        {
            var resultJson = Marshal.PtrToStringUTF8(result)!;

            return JsonSerializer.Deserialize(
                json: resultJson,
                jsonTypeInfo: CedarJsonSerializerContext.Default.IReadOnlyCollectionString)!;
        }
        finally
        {
            CedarFfi.FreeString(result);
        }
    }
}
