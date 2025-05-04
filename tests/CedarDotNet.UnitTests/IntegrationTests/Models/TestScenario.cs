using CedarDotNet.Models;
using CedarDotNet.UnitTests.IntegrationTests.Dtos;

namespace CedarDotNet.UnitTests.IntegrationTests.Models;

public sealed class TestScenario
{
    public required PolicySet Policies { get; init; }
    public required IReadOnlyCollection<Entity> Entities { get; init; }
    public required Schema Schema { get; init; }
    public required bool ShouldValidate { get; init; }
    public required IReadOnlyCollection<TestRequestDto> Requests { get; init; }
}
