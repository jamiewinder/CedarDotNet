using CedarDotNet.Models;
using CedarDotNet.UnitTests.IntegrationTests.Models;

namespace CedarDotNet.UnitTests.IntegrationTests;

public sealed class IntegrationTests
{
    [Theory]
    [ClassData(typeof(IntegrationTestData))]
    public void IsAuthorized_IntegrationTest_Succeeds(
        TestScenario scenario)
    {
        foreach (var request in scenario.Requests)
        {
            var result = CedarFunctions.IsAuthorized(new()
            {
                Principal = request.Principal,
                Action = request.Action,
                Resource = request.Resource,
                Context = request.Context,
                Schema = scenario.Schema,
                ValidateRequest = scenario.ShouldValidate,
                Policies = scenario.Policies,
                Entities = scenario.Entities
            });

            Assert.IsType<AuthorizationAnswerSuccess>(result);

            var actualDecision = ((AuthorizationAnswerSuccess)result).Response.Decision;

            Assert.Equal(request.Decision, actualDecision);
        }
    }
}
