namespace MGNZ.Squidex.CLI.Tests.CLI
{
  using System.Collections.Generic;

  using FluentAssertions;

  using MGNZ.Squidex.CLI.Common.CLI;
  using MGNZ.Squidex.CLI.Common.Commands;
  using MGNZ.Squidex.CLI.Tests.Platform;

  using Xunit;

  [Trait("category", "unit")]
  public class StaticCommandLineRouterUnitTests
  {
    public static List<object[ ]> GetOne_HappyPath_Data => new List<object[ ]>
    {
      // few ways to login
      new object[ ]
      {
        // expected
        new AppLoginRequest()
        {
          Name = "name:app_name",
          AliasCredentials = null,
          AliasCredentialsAs = "aliasas:a_abc123",
          ClientId = null,
          ClientSecret = null,
          Token = "token:t_abc123",
          Url = "url:https://some.site/squidex"
        },
        // input : command line
        @"app login url:https://some.site/squidex name:app_name -t token:t_abc123 -a aliasas:a_abc123"
      },
      new object[ ]
      {
        // expected
        new AppLoginRequest()
        {
          Name = "name:app_name",
          AliasCredentials = null,
          ClientId = "clientid:cid_abc123",
          ClientSecret = "clientsecret:cs_abc123",
          AliasCredentialsAs = "aliasas:a_abc123",
          Url = "url:https://some.site/squidex",
          Token = null
        },
        // input : command line
        @"login app url:https://some.site/squidex name:app_name --client-id clientid:cid_abc123 --client-secret clientsecret:cs_abc123 -a aliasas:a_abc123"
      },

      // some ways to list schemas

      // some ways to export a schema

      // try different ways to logout
      new object[ ]
      {
        // expected
        new AppLogoutRequest()
        {
          AliasCredentials = null,
          Name = "name:app_name"
        },
        // input : command line
        @"app logout --name name:app_name"
      },
      new object[ ]
      {
        // expected
        new AppLogoutRequest()
        {
          AliasCredentials = null,
          Name = "name:app_name"
        },
        // input : command line
        @"app logout -n name:app_name"
      },
      new object[ ]
      {
        // expected
        new AppLogoutRequest()
        {
          Name = "name:app_name",
          AliasCredentials = null
        },
        // input : command line
        @"logout app name:app_name"
      }
    };

    [Theory]
    [MemberData(nameof(GetOne_HappyPath_Data))]
    public void GetOne_HappyPath(BaseRequest expected, string inCommandLine)
    {
      var attributeReflector = new RouteAttributeReflector(SerilogFixture.UsefullLogger<RouteAttributeReflector>());
      var metadataBuilder = new RouteMetadataBuilder(SerilogFixture.UsefullLogger<RouteMetadataBuilder>(), attributeReflector);
      var cachedNouns = metadataBuilder.GetMetadata(typeof(RouteCommandsParser).Assembly);

      var commandParser = new RouteCommandsParser(SerilogFixture.UsefullLogger<RouteCommandsParser>());
      var optionParser = new RouteOptionsParser(SerilogFixture.UsefullLogger<RouteOptionsParser>());
      var routeBuilder = new RouteRequestBuilder(SerilogFixture.UsefullLogger<RouteRequestBuilder>());

      var sut = new StaticCommandLineRouter(SerilogFixture.UsefullLogger<RouteOptionsParser>(), commandParser, optionParser, routeBuilder);

      var actual = sut.GetOne(cachedNouns.Values, inCommandLine.Split(' '));

      actual.Should().NotBeNull();
    }
  }
}