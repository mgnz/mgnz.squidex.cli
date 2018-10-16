namespace MGNZ.Squidex.CLI.Tests.Commands
{
  using System.Collections.Generic;

  using FluentAssertions;

  using MGNZ.Squidex.CLI.Common.Commands;
  using MGNZ.Squidex.CLI.Common.Routing;
  using MGNZ.Squidex.CLI.Tests.Routing;

  using Xunit;

  [Trait("category", "unit")]
  public class SchemaTagRequestUnitTests
  {
    // [Option("n", "name", required: true, ordanalityOrder: 1)] public string Name { get; set; }
    // [Option("t", "tags", required: true, ordanalityOrder: 2)] public string Tags { get; set; }
    // [Option("c", "alias-credentials")] public string AliasCredentials { get; set; }

    public static List<object[]> StaticRouter_Verify_SimpleMap_HappyPath_Data => new List<object[]>
    {
      new object[ ]
      {
        // expected
        new SchemaTagRequest
        {
          Name = "name:appname",
          Tags = "tags:tags",
          AliasCredentials = "aliascredentials:aliascredentials"
        },
        // input : command line
        "schema tag name:appname tags:tags -c aliascredentials:aliascredentials"
      },
      new object[ ]
      {
        // expected
        new SchemaTagRequest
        {
          Name = "name:appname",
          Tags = "tags:tags",
          AliasCredentials = "aliascredentials:aliascredentials"
        },
        // input : command line
        "tag schema name:appname tags:tags --alias-credentials aliascredentials:aliascredentials"
      }
    };

    [Theory]
    [MemberData(nameof(StaticRouter_Verify_SimpleMap_HappyPath_Data))]
    public void StaticRouter_Verify_SimpleMap_HappyPath(SchemaTagRequest expected, string inCommandLine)
    {
      var cachedNouns = new RouteMetadataBuilderFixture().Build().GetMetadata(typeof(RouteCommandsParser).Assembly);
      var router = new StaticCommandLineRouterFixture()
      {
        WithRouteValidator = new RouteRequestValidatorFixture().Mock()
      }.Build();

      var actual = router.GetOne(cachedNouns.Values, inCommandLine.Split(' '));

      actual.Should().NotBeNull();
      actual.Should().BeAssignableTo<SchemaTagRequest>();
      actual.Should().BeEquivalentTo(expected);
    }
  }
}