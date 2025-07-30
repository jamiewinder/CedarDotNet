using System.Text.Json.Serialization;

namespace CedarDotNet.Models;

[JsonSerializable(typeof(AuthorizationCall))]
[JsonSerializable(typeof(PolicySet))]
[JsonSerializable(typeof(FormattingCall))]
[JsonSerializable(typeof(EntitiesParsingCall))]
[JsonSerializable(typeof(ContextParsingCall))]
[JsonSerializable(typeof(IAuthorizationAnswer))]
[JsonSerializable(typeof(ICheckParseAnswer))]
[JsonSerializable(typeof(IFormattingAnswer))]
[JsonSerializable(typeof(IPolicySetTextToPartsAnswer))]
[JsonSourceGenerationOptions(UseStringEnumConverter = true)]
internal sealed partial class CedarJsonSerializerContext
    : JsonSerializerContext;