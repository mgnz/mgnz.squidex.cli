namespace MGNZ.Squidex.CLI.Tests.Commands
{
  using System.Collections.Generic;

  using FluentAssertions;

  using MGNZ.Squidex.CLI.Common.Commands;
  using MGNZ.Squidex.CLI.Common.Routing;
  using MGNZ.Squidex.CLI.Tests.Routing;

  using Xunit;

  [Trait("category", "unit")]
  public class AppLogoutRequestUnitTests
  {
    // [Option("n", "name", required:true, ordanalityOrder:1)] public string Name { get; set; }
    // [Option("c", "alias-credentials")] public string AliasCredentials { get; set; }

    public static List<object[]> StaticRouter_Verify_SimpleMap_HappyPath_Data => new List<object[]>
    {
      new object[ ]
      {
        // expected
        new AppLogoutRequest
        {
          Name = "name:appname",
          AliasCredentials = "aliascredentials:aliascredential"
        },
        // input : command line
        "app logout name:appname -c aliascredentials:aliascredential"
      },
      new object[ ]
      {
        // expected
        new AppLogoutRequest
        {
          Name = "name:appname",
          AliasCredentials = "aliascredentials:aliascredential"
        },
        // input : command line
        "logout app name:appname --alias-credentials aliascredentials:aliascredential"
      }
    };

    [Theory]
    [MemberData(nameof(StaticRouter_Verify_SimpleMap_HappyPath_Data))]
    public void StaticRouter_Verify_SimpleMap_HappyPath(AppLogoutRequest expected, string inCommandLine)
    {
      var cachedNouns = new RouteMetadataBuilderFixture().Build().GetMetadata(typeof(RouteCommandsParser).Assembly);
      var router = new StaticCommandLineRouterFixture()
      {
        WithRouteValidator = new RouteRequestValidatorFixture().Mock()
      }.Build();

      var actual = router.GetOne(cachedNouns.Values, inCommandLine.Split(' '));

      actual.Should().NotBeNull();
      actual.Should().BeAssignableTo<AppLogoutRequest>();
      actual.Should().BeEquivalentTo(expected);
    }
  }
}