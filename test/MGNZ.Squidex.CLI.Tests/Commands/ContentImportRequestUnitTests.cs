namespace MGNZ.Squidex.CLI.Tests.Commands
{
  using System.Collections.Generic;

  using FluentAssertions;

  using MGNZ.Squidex.CLI.Common.Commands;
  using MGNZ.Squidex.CLI.Common.Routing;
  using MGNZ.Squidex.CLI.Tests.Routing;

  using Xunit;

  [Trait("category", "working")]
  public class ContentImportRequestUnitTests
  {
    // [Option("sc", "schema", required: true, ordanalityOrder: 1)] public string Schema { get; set; }
    // [Option("p", "path", required: true, ordanalityOrder: 2)] public string Path { get; set; }
    // [Option("c", "alias-credentials")] public string AliasCredentials { get; set; }

    public static List<object[]> StaticRouter_Verify_SimpleMap_HappyPath_Data => new List<object[]>
    {
      new object[ ]
      {
        // expected
        new ContentImportRequest
        {
          Schema = "schema:schema",
          Path = "path:path",
          AliasCredentials = "aliascredentials:aliascredentials"
        },
        // input : command line
        "content import schema:schema path:path -c aliascredentials:aliascredentials"
      },
      new object[ ]
      {
        // expected
        new ContentImportRequest
        {
          Schema = "schema:schema",
          Path = "path:path",
          AliasCredentials = "aliascredentials:aliascredentials"
        },
        // input : command line
        "import content schema:schema path:path --alias-credentials aliascredentials:aliascredentials"
      }
    };

    [Theory]
    [MemberData(nameof(StaticRouter_Verify_SimpleMap_HappyPath_Data))]
    public void StaticRouter_Verify_SimpleMap_HappyPath(ContentImportRequest expected, string inCommandLine)
    {
      var cachedNouns = new RouteMetadataBuilderFixture().Build().GetMetadata(typeof(RouteCommandsParser).Assembly);
      var router = new StaticCommandLineRouterFixture()
      {
        WithRouteValidator = new RouteRequestValidatorFixture().Mock()
      }.Build();

      var actual = router.GetOne(cachedNouns.Values, inCommandLine.Split(' '));

      actual.Should().NotBeNull();
      actual.Should().BeAssignableTo<ContentImportRequest>();
      actual.Should().BeEquivalentTo(expected);
    }
  }
}