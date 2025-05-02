using CedarDotNet.Models;
using System.Diagnostics.CodeAnalysis;
namespace CedarDotNet;

/// <summary>
/// The Cedar functions (Experimental)
/// </summary>
public static partial class CedarFunctions
{
    /// <summary>
    /// Evaluates an partial authorization query.
    /// </summary>
    /// <param name="call">The partial authorization call.</param>
    /// <returns>The partial authorization answer.</returns>
    [Experimental(Experimental.CedarPartialExpressions)]
    public static IPartialAuthorizationAnswer IsAuthorizedPartial(
        PartialAuthorizationCall call)
        => WrapJsonCall(
            input: call,
            func: CedarFfi.IsAuthorizedPartial,
            inputTypeInfo: CedarExperimentalJsonSerializerContext.Default.PartialAuthorizationCall,
            outputTypeInfo: CedarExperimentalJsonSerializerContext.Default.IPartialAuthorizationAnswer);
}
