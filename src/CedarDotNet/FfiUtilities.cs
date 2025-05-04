using CedarDotNet.Interop;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace CedarDotNet;

internal static class FfiUtilities
{
    public static string CallNullaryStr(
        Func<nint> func)
    {
        ArgumentNullException.ThrowIfNull(func);

        var resultPtr = func();
        try
        {
            return Marshal.PtrToStringUTF8(resultPtr)!;
        }
        finally
        {
            CedarFfi.FreeString(resultPtr);
        }
    }

    public static string CallUnaryStr(
        string input,
        Func<string, nint> func)
    {
        ArgumentNullException.ThrowIfNull(input);
        ArgumentNullException.ThrowIfNull(func);

        var resultPtr = func(input);
        try
        {
            return Marshal.PtrToStringUTF8(resultPtr)!;
        }
        finally
        {
            CedarFfi.FreeString(resultPtr);
        }
    }

    public static TOutput CallUnaryJson<TInput, TOutput>(
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