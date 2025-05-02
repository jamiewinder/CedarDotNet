using CedarDotNet.Models;

namespace CedarDotNet.UnitTests;

public sealed class CedarFunctionsTests
{
    private static Schema Schema = Schema.FromText("""
        entity User;
        entity Photo;
    
        action view appliesTo {
            principal: User, 
            resource: Photo
        };
        """);

    private static PolicySet PolicySet = new PolicySet
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
    public void IsAuthorized_SimpleTest_Success()
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
    public void IsAuthorized_SimpleTest_Failure()
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
