namespace MGNZ.Squidex.CLI.Tests.Routing
{
  using System.Collections.Generic;

  using FluentAssertions;

  using MediatR;

  using MGNZ.Squidex.CLI.Common.Routing;
  using MGNZ.Squidex.CLI.Common.Commands;
  using MGNZ.Squidex.CLI.Tests.Platform;

  using Xunit;

  [Trait("category", "unit")]
  public class RouteRequestBuilderUnitTests
  {
    private static Option GetOption(string nounKey, string verbKey, string optionKey) => Nouns[nounKey].Verbs[verbKey].Options[optionKey];

    private static Option GetOptionWithValue(string nounKey, string verbKey, string optionKey, string @value)
    {
      var option = GetOption(nounKey, verbKey, optionKey);
      option.Value = @value;

      return option;
    }

    private static Dictionary<string, Noun> Nouns =>
      new Dictionary<string, Noun>
      {
        {"app", new AppNoun()},
        {"asset", new AssetNoun()},
        {"content", new ContentNoun()},
        {"schema", new SchemaNoun()}
      };

    public static List<object[]> MapOperation_HappyPath_Data => new List<object[]>
    {
      // few ways to login
      new object[ ]
      {
        new AppLoginRequest()
        {
          Url = "https://some.site/squidex",
          Name = "app_name",
          Token = "t_abc123",
          AliasCredentialsAs = "a_abc123"
        }, 
        new AppNoun(new Dictionary<string, Dictionary<string, string>>()
        {
          {"login", new Dictionary<string, string>()
          {
            {"url", "https://some.site/squidex"},
            {"name","app_name"},
            {"token", "t_abc123"},
            {"alias-credentials-as", "a_abc123"}
          }}
        })
      },
      new object[ ]
      {
        new AppLoginRequest()
        {
          Url = "https://some.site/squidex",
          Name = "app_name",
          ClientId = "cid_abc123",
          ClientSecret = "cs_abc123",
          AliasCredentialsAs = "a_abc123"
        },
        new AppNoun(new Dictionary<string, Dictionary<string, string>>()
        {
          {"login", new Dictionary<string, string>()
          {
            {"url", "https://some.site/squidex"},
            {"name","app_name"},
            {"client-id", "cid_abc123"},
            {"client-secret", "cs_abc123"},
            {"alias-credentials-as", "a_abc123"}
          }}
        })
      },
      
      // some ways to list schemas

      // some ways to export a schema

      // try different ways to logout

      new object[ ]
      {
        new AppLogoutRequest()
        {
          Name = "app_name"
        }, 
        new AppNoun(new Dictionary<string, Dictionary<string, string>>()
        {
          {"logout", new Dictionary<string, string>()
          {
            {"name","app_name"}
          }}
        })
      }
    };


    [Theory]
    [MemberData(nameof(MapOperation_HappyPath_Data))]
    public void MapOperation_HappyPath(IRequest expectedRequest, Noun noun)
    {
      var sut = new RouteRequestBuilder(SerilogFixture.UsefullLogger<RouteRequestBuilder>());

      var actualRequest = sut.GetRequestForVerb(noun);
      actualRequest.Should().NotBeNull();

      if (expectedRequest is AppLoginRequest expectedAppLoginRequest && actualRequest is AppLoginRequest actualAppLoginRequest)
      {
        actualAppLoginRequest.Should().BeEquivalentTo(expectedAppLoginRequest);
      }
      else if (expectedRequest is AppLogoutRequest expectedAppLogoutRequest && actualRequest is AppLogoutRequest actualAppLogoutRequest)
      {
        actualAppLogoutRequest.Should().BeEquivalentTo(expectedAppLogoutRequest);
      }
      else
        Assert.True(false, $"Unexpected actual ({expectedRequest.GetType().Name}) or expected ({actualRequest.GetType().Name}) found");
    }
  }
}