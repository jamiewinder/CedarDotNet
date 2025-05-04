using CedarDotNet;
using CedarDotNet.Models;
using CedarDotNet.Values;

using static BasicExample.ConsoleHelpers;

#pragma warning disable CedarPartialExpressions

var schema = Schema.FromText("""
    entity User {
      age: Long
    };
    entity Photo;
    
    action view appliesTo {
      principal: User, 
      resource: Photo,
      context: {
        now?: datetime,
        dt?: datetime
      }
    };
    """);

var policySet = new PolicySet
{
    StaticPolicies = new Dictionary<string, string>
    {
        ["ID1"] = """
            permit(
              principal == User::"alice",
              action == Action::"view",
              resource
            ) when {
              context.now < context.dt
            };
            """
    }
};

WriteTitle("Versions");
Console.WriteLine($"LangVersion : {CedarFunctions.GetLangVersion()}");
Console.WriteLine($"SDKVersion  : {CedarFunctions.GetSdkVersion()}");
Console.WriteLine();

TestIsAuthorized(new()
{
    Principal = EntityUid.Create("User", "alice"),
    Action = EntityUid.Create("Action", "view"),
    Resource = EntityUid.Create("Photo", "door"),
    Context = new Dictionary<string, Value>()
    {
        ["now"] = DateTimeOffset.UtcNow,
        ["dt"] = DateTimeOffset.UtcNow.AddDays(1)
    },
    Schema = schema,
    ValidateRequest = true,
    Policies = policySet,
    Entities = [
        new()
        {
            Uid = EntityUid.Create("User", "alice"),
            Attrs = new Dictionary<string, Value>
            {
                ["age"] = 29
            }
        },
        new()
        {
            Uid = EntityUid.Create("User", "bob"),
            Attrs = new Dictionary<string, Value>
            {
                ["age"] = 31
            }
        }
    ]
});
Console.WriteLine();

TestIsAuthorizedPartial(new()
{
    Principal = EntityUid.Create("User", "alice"),
    Action = EntityUid.Create("Action", "view"),
    Resource = EntityUid.Create("Photo", "door"),
    Context = new Dictionary<string, Value>()
    {
        ["now"] = DateTimeOffset.UtcNow,
        ["dt"] = DateTimeOffset.UtcNow.AddDays(1)
    },
    Schema = schema,
    ValidateRequest = true,
    Policies = policySet,
    Entities = [
        new()
        {
            Uid = EntityUid.Create("User", "alice"),
            Attrs = new Dictionary<string, Value>
            {
                ["age"] = 29
            }
        },
        new()
        {
            Uid = EntityUid.Create("User", "bob"),
            Attrs = new Dictionary<string, Value>
            {
                ["age"] = 31
            }
        }
    ]
});
Console.WriteLine();

TestFormat(new()
{
    PolicyText = policySet.StaticPolicies.First().Value,
    LineWidth = 80,
    IndentWidth = 4
});
Console.WriteLine();

TestCheckParseSchema(schema);
Console.WriteLine();

TestCheckParsePolicySet(policySet);
Console.WriteLine();

TestCheckParseEntities(new()
{
    Entities =
    [
        new()
        {
            Uid = EntityUid.Create("User", "alice"),
            Attrs = new Dictionary<string, Value>
            {
                ["age"] = 29
            }
        }
    ],
    Schema = schema
});
Console.WriteLine();

TestCheckParseContext(new()
{
    Context = new Dictionary<string, Value>
    {
        { "now", DateTimeOffset.UtcNow },
        { "dt", DateTimeOffset.UtcNow.AddDays(1) }
    },
    Schema = schema,
    Action = EntityUid.Create("Action", "view")
});

Console.WriteLine();

TestPolicyFormatToJsonToText(policySet.StaticPolicies.First().Value);

Console.WriteLine();

static void TestIsAuthorized(AuthorizationCall call)
{
    var answer = CedarFunctions.IsAuthorized(call);

    WriteTitle("IsAuthorized");
    WriteAnswer(answer);
}

static void TestIsAuthorizedPartial(PartialAuthorizationCall call)
{
    var answer = CedarExperimentalFunctions.IsAuthorizedPartial(call);

    WriteTitle("IsAuthorizedPartial");
    WriteAnswer(answer);
}

static void TestFormat(FormattingCall call)
{
    var answer = CedarFunctions.Format(call);

    WriteTitle("Format");
    WriteAnswer(answer);
}

static void TestCheckParseSchema(Schema schema)
{
    var answer = CedarFunctions.CheckParseSchema(schema);

    WriteTitle("CheckParseSchema");
    WriteAnswer(answer);
}

static void TestCheckParsePolicySet(PolicySet policySet)
{
    var answer = CedarFunctions.CheckParsePolicySet(policySet);

    WriteTitle("CheckParsePolicySet");
    WriteAnswer(answer);
}

static void TestCheckParseEntities(EntitiesParsingCall call)
{
    var answer = CedarFunctions.CheckParseEntities(call);

    WriteTitle("CheckParseEntities");
    WriteAnswer(answer);
}

static void TestCheckParseContext(ContextParsingCall call)
{
    var answer = CedarFunctions.CheckParseContext(call);

    WriteTitle("CheckParseContext");
    WriteAnswer(answer);
}

static void TestPolicyFormatToJsonToText(string policyText)
{
    var policyJson = CedarUtilities.PolicyFormatTextToJson(policyText);

    WriteTitle("PolicyFormatTextToJson");
    WriteAnswer(policyJson);

    var policyTest2 = CedarUtilities.PolicyFormatJsonToText(policyJson);

    WriteTitle("PolicyFormatJsonToText");
    WriteAnswer(policyTest2);
}
