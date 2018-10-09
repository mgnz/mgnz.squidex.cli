namespace MGNZ.Squidex.CLI.Tests.Commands
{
  using System.Collections.Generic;

  using FluentAssertions;

  using MGNZ.Squidex.CLI.Common.Commands;
  using MGNZ.Squidex.CLI.Common.Routing;
  using MGNZ.Squidex.CLI.Tests.Routing;

  using Xunit;

  [Trait("category", "unit")]
  public class AssetImportRequestUnitTests
  {
    // [Option("n", "name", required: true, ordanalityOrder: 1)] public string Name { get; set; }
    // [Option("p", "path", required: true, ordanalityOrder: 2)] public string Path { get; set; }
    // [Option("t", "tags", required: true, ordanalityOrder: 3)] public string Tags { get; set; }
    // [Option("c", "alias-credentials")] public string AliasCredentials { get; set; }

    public static List<object[]> StaticRouter_Verify_SimpleMap_HappyPath_Data => new List<object[]>
    {
      new object[ ]
      {
        // expected
        new AssetImportRequest
        {
          Name = "name:appname",
          Path = "path:path",
          Tags = "tags:tags",
          AliasCredentials = "aliascredentials:aliascredentials"
        },
        // input : command line
        "asset import name:appname path:path tags:tags -c aliascredentials:aliascredentials"
      },
      new object[ ]
      {
        // expected
        new AssetImportRequest
        {
          Name = "name:appname",
          Path = "path:path",
          Tags = "tags:tags",
          AliasCredentials = "aliascredentials:aliascredentials"
        },
        // input : command line
        "import asset name:appname path:path tags:tags --alias-credentials aliascredentials:aliascredentials"
      }
    };

    [Theory]
    [MemberData(nameof(StaticRouter_Verify_SimpleMap_HappyPath_Data))]
    public void StaticRouter_Verify_SimpleMap_HappyPath(AssetImportRequest expected, string inCommandLine)
    {
      var cachedNouns = new RouteMetadataBuilderFixture().Build().GetMetadata(typeof(RouteCommandsParser).Assembly);
      var router = new StaticCommandLineRouterFixture()
      {
        WithRouteValidator = new RouteRequestValidatorFixture().Mock()
      }.Build();

      var actual = router.GetOne(cachedNouns.Values, inCommandLine.Split(' '));

      actual.Should().NotBeNull();
      actual.Should().BeAssignableTo<AssetImportRequest>();
      actual.Should().BeEquivalentTo(expected);
    }
  }
}