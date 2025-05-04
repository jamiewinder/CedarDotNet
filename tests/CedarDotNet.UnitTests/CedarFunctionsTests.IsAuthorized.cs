using CedarDotNet.Models;

namespace CedarDotNet.UnitTests;

public sealed class CedarFunctionsTests
{
    private static readonly Schema Schema = Schema.FromText("""
        entity User;
        entity Photo;
    
        action view appliesTo {
            principal: User, 
            resource: Photo
        };
        """);

    private static readonly PolicySet PolicySet = new()
    {
        StaticPolicies = new Dictionary<string, string>
        {
            ["ID1"] = """
                permit(
                    principal == User::"alice",
                    action == Action::"view",
                    resource
                );
                """
        }
    };

    [Fact]
    public void IsAuthorized_AuthorisableRequest_DecisionIsAllow()
    {
        var result = CedarFunctions.IsAuthorized(new()
        {
            Principal = EntityUid.Create("User", "alice"),
            Action = EntityUid.Create("Action", "view"),
            Resource = EntityUid.Create("Photo", "door"),
            Schema = Schema,
            ValidateRequest = true,
            Policies = PolicySet,
            Entities = []
        });

        // Assert
        Assert.IsType<AuthorizationAnswerSuccess>(result);

        var decision = ((AuthorizationAnswerSuccess)result).Response.Decision;

        Assert.Equal(Decision.Allow, decision);
    }

    [Fact]
    public void IsAuthorized_NonAuthorisableRequest_DecisionIsDeny()
    {
        var result = CedarFunctions.IsAuthorized(new()
        {
            Principal = EntityUid.Create("User", "bob"),
            Action = EntityUid.Create("Action", "view"),
            Resource = EntityUid.Create("Photo", "door"),
            Schema = Schema,
            ValidateRequest = true,
            Policies = PolicySet,
            Entities = []
        });

        // Assert
        Assert.IsType<AuthorizationAnswerSuccess>(result);

        var decision = ((AuthorizationAnswerSuccess)result).Response.Decision;

        Assert.Equal(Decision.Deny, decision);
    }
}
