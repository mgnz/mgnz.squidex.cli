namespace MGNZ.Squidex.CLI.Tests.Commands
{
  using System.Collections.Generic;

  using FluentAssertions;

  using MGNZ.Squidex.CLI.Common.Commands;
  using MGNZ.Squidex.CLI.Common.Routing;
  using MGNZ.Squidex.CLI.Tests.Routing;

  using Xunit;

  [Trait("category", "unit")]
  public class SchemaImportRequestUnitTests
  {
    // [Option("n", "name", required: true, ordanalityOrder: 1)] public string Name { get; set; }
    // [Option("p", "path", required: true, ordanalityOrder: 2)] public string Path { get; set; }
    // [Option("c", "alias-credentials")] public string AliasCredentials { get; set; }

    public static List<object[]> StaticRouter_Verify_SimpleMap_HappyPath_Data => new List<object[]>
    {
      new object[ ]
      {
        // expected
        new SchemaImportRequest
        {
          Name = "name:appname",
          Path = "path:path",
          AliasCredentials = "aliascredentials:aliascredentials"
        },
        // input : command line
        "schema import name:appname path:path -c aliascredentials:aliascredentials"
      },
      new object[ ]
      {
        // expected
        new SchemaImportRequest
        {
          Name = "name:appname",
          Path = "path:path",
          AliasCredentials = "aliascredentials:aliascredentials"
        },
        // input : command line
        "import schema name:appname path:path --alias-credentials aliascredentials:aliascredentials"
      }
    };

    [Theory]
    [MemberData(nameof(StaticRouter_Verify_SimpleMap_HappyPath_Data))]
    public void StaticRouter_Verify_SimpleMap_HappyPath(SchemaImportRequest expected, string inCommandLine)
    {
      var cachedNouns = new RouteMetadataBuilderFixture().Build().GetMetadata(typeof(RouteCommandsParser).Assembly);
      var router = new StaticCommandLineRouterFixture()
      {
        WithRouteValidator = new RouteRequestValidatorFixture().Mock()
      }.Build();

      var actual = router.GetOne(cachedNouns.Values, inCommandLine.Split(' '));

      actual.Should().NotBeNull();
      actual.Should().BeAssignableTo<SchemaImportRequest>();
      actual.Should().BeEquivalentTo(expected);
    }
  }
}