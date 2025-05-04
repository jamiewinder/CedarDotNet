using CedarDotNet.Models;
using CedarDotNet.UnitTests.IntegrationTests.Dtos;
using CedarDotNet.UnitTests.IntegrationTests.Models;
using System.Collections;
using System.Text.Json;

namespace CedarDotNet.UnitTests.IntegrationTests;

public sealed class IntegrationTestData
    : IEnumerable<object[]>
{
    private static readonly string BaseDirectory = "../../../../../cedar-integration-tests";

    public IEnumerator<object[]> GetEnumerator()
    {
        var testsDirectory = Path.Combine(BaseDirectory, "tests");

        var testFiles = Directory.EnumerateFiles(testsDirectory, "*.json", SearchOption.AllDirectories);

        foreach (var testFile in testFiles)
        {
            yield return LoadTestFile(testFile);
        }
    }

    private static object[] LoadTestFile(string path)
    {
        var json = File.ReadAllText(path);

        var scenarioDto = JsonSerializer.Deserialize(
            json: json,
            jsonTypeInfo: TestJsonSerializedContext.Default.TestScenarioDto)!;

        var policiesText = File.ReadAllText(Path.Combine(BaseDirectory, scenarioDto.Policies));

        var policies = CedarUtilities.LoadPolicySet(policiesText);

        var policySet = new PolicySet()
        {
            StaticPolicies = policies
                .Select((x, i) => KeyValuePair.Create($"#{i + 1}", x))
                .ToDictionary()
        };

        var entitiesText = File.ReadAllText(Path.Combine(BaseDirectory, scenarioDto.Entities));

        var entities = JsonSerializer.Deserialize(
            json: entitiesText,
            jsonTypeInfo: TestJsonSerializedContext.Default.IReadOnlyCollectionEntity)!;

        var schemaText = File.ReadAllText(Path.Combine(BaseDirectory, scenarioDto.Schema));

        var schema = Schema.FromText(schemaText);

        var scenario = new TestScenario
        {
            Policies = policySet,
            Entities = entities,
            Schema = schema,
            ShouldValidate = scenarioDto.ShouldValidate,
            Requests = scenarioDto.Requests
        };

        return [scenario];
    }

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();
}