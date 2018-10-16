namespace MGNZ.Squidex.CLI.Tests.Commands
{
  using System.Collections.Generic;

  using FluentAssertions;

  using MGNZ.Squidex.CLI.Common.Commands;
  using MGNZ.Squidex.CLI.Common.Routing;
  using MGNZ.Squidex.CLI.Tests.Routing;

  using Xunit;

  [Trait("category", "unit")]
  public class AppLoginRequestUnitTests
  {
    // [Option("u", "url", required: true, ordanalityOrder: 1)] public string Url { get; set; }
    // [Option("n", "name", required: true, ordanalityOrder: 2)] public string Name { get; set; }
    // [Option("cid", "client-id")] public string ClientId { get; set; }
    // [Option("cs", "client-secret")] public string ClientSecret { get; set; }
    // [Option("t", "token")] public string Token { get; set; }
    // [Option("c", "alias-credentials")] public string AliasCredentials { get; set; }
    // [Option("a", "alias-credentials-as")] public string AliasCredentialsAs { get; set; }

    public static List<object[]> StaticRouter_Verify_SimpleMap_HappyPath_Data => new List<object[]>
    {
      new object[ ]
      {
        // expected
        new AppLoginRequest
        {
          Name = "name:appname",
          Url = "uri:uri",
          Token = "token:token",
          AliasCredentials = "alascredentials:aliascredentials",
          ClientId = "clientid:clientid",
          ClientSecret = "clientsecret:clientsecret",
          AliasCredentialsAs = "aliascredentialsas:aliascredentialsas"
        },
        // input : command line
        "app login uri:uri name:appname -t token:token -c alascredentials:aliascredentials -cid clientid:clientid -cs clientsecret:clientsecret -a aliascredentialsas:aliascredentialsas"
      },
      new object[ ]
      {
        // expected
        new AppLoginRequest
        {
          Name = "name:name",
          Url = "uri:uri",
          Token = "token:token",
          AliasCredentials = "alascredentials:aliascredentials",
          ClientId = "clientid:clientid",
          ClientSecret = "clientsecret:clientsecret",
          AliasCredentialsAs = "aliascredentialsas:aliascredentialsas"
        },
        // input : command line
        "login app uri:uri name:name -t token:token --alias-credentials alascredentials:aliascredentials --client-id clientid:clientid --client-secret clientsecret:clientsecret --alias-credentials-as aliascredentialsas:aliascredentialsas"
      }
    };

    [Theory]
    [MemberData(nameof(StaticRouter_Verify_SimpleMap_HappyPath_Data))]
    public void StaticRouter_Verify_SimpleMap_HappyPath(AppLoginRequest expected, string inCommandLine)
    {
      var cachedNouns = new RouteMetadataBuilderFixture().Build().GetMetadata(typeof(RouteCommandsParser).Assembly);
      var router = new StaticCommandLineRouterFixture()
      {
        WithRouteValidator = new RouteRequestValidatorFixture().Mock()
      }.Build();

      var actual = router.GetOne(cachedNouns.Values, inCommandLine.Split(' '));

      actual.Should().NotBeNull();
      actual.Should().BeAssignableTo<AppLoginRequest>();
      actual.Should().BeEquivalentTo(expected);
    }
  }
}