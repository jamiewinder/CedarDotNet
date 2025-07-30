using CedarDotNet.Models;

namespace CedarDotNet.UnitTests;

public partial class CedarFunctionTests
{
    [Fact]
    public void PolicySetTextToParts_ValidPolicySet_ReturnsPoliciesAndTemplates()
    {
        // Arrange
        var policySetText = """
            permit(principal, action, resource)
            when { principal has "Email" && principal.Email == "a@a.com" };

            permit(principal in UserGroup::"DeathRowRecords", action == Action::"pop", resource);

            permit(principal in ?principal, action, resource);
            """;

        // Act
        var result = CedarFunctions.PolicySetTextToParts(policySetText);

        // Assert
        Assert.IsType<PolicySetTextToPartsAnswerSuccess>(result);

        var success = (PolicySetTextToPartsAnswerSuccess)result;

        Assert.Equal(2, success.Policies.Count);
        Assert.Single(success.PolicyTemplates);
    }

    [Fact]
    public void PolicySetTextToParts_InvalidPolicySet_ReturnsFailure()
    {
        // Arrange
        var policySetText = """
            invalid
            """;

        // Act
        var result = CedarFunctions.PolicySetTextToParts(policySetText);

        // Assert
        Assert.IsType<PolicySetTextToPartsAnswerFailure>(result);
    }
}
