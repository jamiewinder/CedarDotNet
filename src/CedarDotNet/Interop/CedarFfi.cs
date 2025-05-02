using System.Runtime.InteropServices;

namespace CedarDotNet;

internal static partial class CedarFfi
{
    private const string NativeLibraryName = "cedar_dotnet_ffi";

    [LibraryImport(NativeLibraryName, EntryPoint = "check_parse_policy_set", StringMarshalling = StringMarshalling.Utf8)]
    public static partial IntPtr CheckParsePolicySet(string call);

    [LibraryImport(NativeLibraryName, EntryPoint = "check_parse_schema", StringMarshalling = StringMarshalling.Utf8)]
    public static partial IntPtr CheckParseSchema(string call);

    [LibraryImport(NativeLibraryName, EntryPoint = "check_parse_entities", StringMarshalling = StringMarshalling.Utf8)]
    public static partial IntPtr CheckParseEntities(string call);

    [LibraryImport(NativeLibraryName, EntryPoint = "check_parse_context", StringMarshalling = StringMarshalling.Utf8)]
    public static partial IntPtr CheckParseContext(string call);

    [LibraryImport(NativeLibraryName, EntryPoint = "format", StringMarshalling = StringMarshalling.Utf8)]
    public static partial IntPtr Format(string call);

    [LibraryImport(NativeLibraryName, EntryPoint = "is_authorized", StringMarshalling = StringMarshalling.Utf8)]
    public static partial IntPtr IsAuthorized(string call);

    [LibraryImport(NativeLibraryName, EntryPoint = "is_authorized_partial", StringMarshalling = StringMarshalling.Utf8)]
    public static partial IntPtr IsAuthorizedPartial(string call);

    [LibraryImport(NativeLibraryName, EntryPoint = "validate", StringMarshalling = StringMarshalling.Utf8)]
    public static partial IntPtr Validate(string call);

    [LibraryImport(NativeLibraryName, EntryPoint = "get_lang_version", StringMarshalling = StringMarshalling.Utf8)]
    public static partial IntPtr GetLangVersion();

    [LibraryImport(NativeLibraryName, EntryPoint = "get_sdk_version", StringMarshalling = StringMarshalling.Utf8)]
    public static partial IntPtr GetSdkVersion();

    [LibraryImport(NativeLibraryName, EntryPoint = "free_string")]
    public static partial void FreeString(IntPtr ptr);
}