namespace MGNZ.Squidex.CLI.Tests.Commands
{
  using System.Collections.Generic;

  using FluentAssertions;

  using MGNZ.Squidex.CLI.Common.Commands;
  using MGNZ.Squidex.CLI.Common.Routing;
  using MGNZ.Squidex.CLI.Tests.Routing;

  using Xunit;

  [Trait("category", "unit")]
  public class AppNewRequestUnitTests
  {
    // [Option("n", "name", required: true, ordanalityOrder: 1)] public string Name { get; set; }
    // [Option("t", "token")] public string Token { get; set; } 

    public static List<object[]> StaticRouter_Verify_SimpleMap_HappyPath_Data => new List<object[]>
    {
      new object[ ]
      {
        // expected
        new AppNewRequest()
        {
          Name = "name:appname",
          Token = "token:token"
        },
        // input : command line
        "app new name:appname -t token:token"
      },
      new object[ ]
      {
        // expected
        new AppNewRequest()
        {
          Name = "name:appname",
          Token = "token:token"
        },
        // input : command line
        "new app name:appname --token token:token"
      }
    };

    [Theory]
    [MemberData(nameof(StaticRouter_Verify_SimpleMap_HappyPath_Data))]
    public void StaticRouter_Verify_SimpleMap_HappyPath(AppNewRequest expected, string inCommandLine)
    {
      var cachedNouns = new RouteMetadataBuilderFixture().Build().GetMetadata(typeof(RouteCommandsParser).Assembly);
      var router = new StaticCommandLineRouterFixture()
      {
        WithRouteValidator = new RouteRequestValidatorFixture().Mock()
      }.Build();

      var actual = router.GetOne(cachedNouns.Values, inCommandLine.Split(' '));

      actual.Should().NotBeNull();
      actual.Should().BeAssignableTo<AppNewRequest>();
      actual.Should().BeEquivalentTo(expected);
    }
  }
}