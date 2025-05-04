using System.Runtime.InteropServices;

namespace CedarDotNet.Interop;

internal static partial class CedarFfi
{
    [LibraryImport(CedarNativeLibrary.Name, EntryPoint = "policy_format_text_to_json", StringMarshalling = StringMarshalling.Utf8)]
    public static partial IntPtr PolicyFormatTextToJson(string call);

    [LibraryImport(CedarNativeLibrary.Name, EntryPoint = "policy_format_json_to_text", StringMarshalling = StringMarshalling.Utf8)]
    public static partial IntPtr PolicyFormatJsonToText(string call);
}