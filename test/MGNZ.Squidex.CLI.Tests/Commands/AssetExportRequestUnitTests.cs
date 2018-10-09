namespace MGNZ.Squidex.CLI.Tests.Commands
{
  using System.Collections.Generic;

  using FluentAssertions;

  using MGNZ.Squidex.CLI.Common.Commands;
  using MGNZ.Squidex.CLI.Common.Routing;
  using MGNZ.Squidex.CLI.Tests.Routing;

  using Xunit;

  [Trait("category", "unit")]
  public class AssetExportRequestUnitTests
  {
    // [Option("n", "name", required: true, ordanalityOrder: 1)] public string Name { get; set; }
    // [Option("p", "path", required: true, ordanalityOrder: 2)] public string Path { get; set; }
    // [Option("c", "alias-credentials")] public string AliasCredentials { get; set; }

    public static List<object[ ]> StaticRouter_Verify_SimpleMap_HappyPath_Data => new List<object[ ]>
    {
      new object[ ]
      {
        // expected
        new AssetExportRequest()
        {
          Name = "name:appname",
          Path = "path:path",
          AliasCredentials = "aliascredentials:aliascredentals"
        },
        // input : command line
        "asset export name:appname path:path -c aliascredentials:aliascredentals"
      },
      new object[ ]
      {
        // expected
        new AssetExportRequest()
        {
          Name = "name:appname",
          Path = "path:path",
          AliasCredentials = "aliascredentials:aliascredentals"
        },
        // input : command line
        "export asset name:appname path:path --alias-credentials aliascredentials:aliascredentals"
      }
    };

    [Theory]
    [MemberData(nameof(StaticRouter_Verify_SimpleMap_HappyPath_Data))]
    public void StaticRouter_Verify_SimpleMap_HappyPath(AssetExportRequest expected, string inCommandLine)
    {
      var cachedNouns = new RouteMetadataBuilderFixture().Build().GetMetadata(typeof(RouteCommandsParser).Assembly);
      var router = new StaticCommandLineRouterFixture()
      {
        WithRouteValidator = new RouteRequestValidatorFixture().Mock()
      }.Build();

      var actual = router.GetOne(cachedNouns.Values, inCommandLine.Split(' '));

      actual.Should().NotBeNull();
      actual.Should().BeAssignableTo<AssetExportRequest>();
      actual.Should().BeEquivalentTo(expected);
    }
  }
}