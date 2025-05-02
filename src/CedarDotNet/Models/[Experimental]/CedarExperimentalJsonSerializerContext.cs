using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace CedarDotNet.Models;

[Experimental(Experimental.CedarPartialExpressions)]
[JsonSerializable(typeof(PartialAuthorizationCall))]
[JsonSerializable(typeof(IPartialAuthorizationAnswer))]
[JsonSourceGenerationOptions(UseStringEnumConverter = true)]
internal sealed partial class CedarExperimentalJsonSerializerContext
    : JsonSerializerContext;