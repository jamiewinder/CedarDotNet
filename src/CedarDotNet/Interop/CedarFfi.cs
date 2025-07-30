using System.Runtime.InteropServices;

namespace CedarDotNet.Interop;

internal static partial class CedarFfi
{
    [LibraryImport(CedarNativeLibrary.Name, EntryPoint = "check_parse_policy_set", StringMarshalling = StringMarshalling.Utf8)]
    public static partial IntPtr CheckParsePolicySet(string call);

    [LibraryImport(CedarNativeLibrary.Name, EntryPoint = "check_parse_schema", StringMarshalling = StringMarshalling.Utf8)]
    public static partial IntPtr CheckParseSchema(string call);

    [LibraryImport(CedarNativeLibrary.Name, EntryPoint = "check_parse_entities", StringMarshalling = StringMarshalling.Utf8)]
    public static partial IntPtr CheckParseEntities(string call);

    [LibraryImport(CedarNativeLibrary.Name, EntryPoint = "check_parse_context", StringMarshalling = StringMarshalling.Utf8)]
    public static partial IntPtr CheckParseContext(string call);

    [LibraryImport(CedarNativeLibrary.Name, EntryPoint = "format", StringMarshalling = StringMarshalling.Utf8)]
    public static partial IntPtr Format(string call);

    [LibraryImport(CedarNativeLibrary.Name, EntryPoint = "is_authorized", StringMarshalling = StringMarshalling.Utf8)]
    public static partial IntPtr IsAuthorized(string call);

    [LibraryImport(CedarNativeLibrary.Name, EntryPoint = "is_authorized_partial", StringMarshalling = StringMarshalling.Utf8)]
    public static partial IntPtr IsAuthorizedPartial(string call);

    [LibraryImport(CedarNativeLibrary.Name, EntryPoint = "validate", StringMarshalling = StringMarshalling.Utf8)]
    public static partial IntPtr Validate(string call);

    [LibraryImport(CedarNativeLibrary.Name, EntryPoint = "policy_set_text_to_parts", StringMarshalling = StringMarshalling.Utf8)]
    public static partial IntPtr PolicySetTextToParts(string call);

    [LibraryImport(CedarNativeLibrary.Name, EntryPoint = "get_lang_version", StringMarshalling = StringMarshalling.Utf8)]
    public static partial IntPtr GetLangVersion();

    [LibraryImport(CedarNativeLibrary.Name, EntryPoint = "get_sdk_version", StringMarshalling = StringMarshalling.Utf8)]
    public static partial IntPtr GetSdkVersion();

    [LibraryImport(CedarNativeLibrary.Name, EntryPoint = "free_string")]
    public static partial void FreeString(IntPtr ptr);
}