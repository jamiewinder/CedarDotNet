﻿using CedarDotNet.Interop;
using CedarDotNet.Models;
using System.Diagnostics.CodeAnalysis;

namespace CedarDotNet;

/// <summary>
/// The Cedar experimental functions available through the main Cedar FFI.
/// </summary>
public static partial class CedarExperimentalFunctions
{
    /// <summary>
    /// Evaluates an partial authorization query.
    /// </summary>
    /// <param name="call">The partial authorization call.</param>
    /// <returns>The partial authorization answer.</returns>
    [Experimental(Experimental.CedarPartialExpressions)]
    public static IPartialAuthorizationAnswer IsAuthorizedPartial(
        PartialAuthorizationCall call)
        => FfiUtilities.CallUnaryJson(
            input: call,
            func: CedarFfi.IsAuthorizedPartial,
            inputTypeInfo: CedarExperimentalJsonSerializerContext.Default.PartialAuthorizationCall,
            outputTypeInfo: CedarExperimentalJsonSerializerContext.Default.IPartialAuthorizationAnswer);
}
