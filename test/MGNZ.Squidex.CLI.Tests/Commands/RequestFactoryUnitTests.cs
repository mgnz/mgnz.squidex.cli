namespace MGNZ.Squidex.CLI.Tests.Commands
{
  using System;
  using System.Collections.Generic;

  using FluentAssertions;

  using MediatR;

  using MGNZ.Squidex.CLI.Common.CLI;
  using MGNZ.Squidex.CLI.Common.Commands;
  using MGNZ.Squidex.CLI.Tests.Platform;

  using Xunit;

  [Trait("category", "unit")]
  public class RequestFactoryUnitTests
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
        null,
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
        null,
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
        null,
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
    public void MapOperation_HappyPath(IRequest expectedRequest, Verb verb)
    {
      var sut = new RequestFactory(SerilogFixture.UsefullLogger<RequestFactory>());

      var actual = sut.GetRequestForVerb(verb);
      //actual.Should().NotBeNull();
      //actual.Count.Should().Be(verb.Options.Count);

      //foreach (var option in expectedOptions)
      //{
      //  var expectedOption = option.Value.Item1;
      //  var expectedOptionValue = option.Value.Item2;

      //  var actualOption = actual[expectedOption.GetLongNameFormatted];

      //  actualOption.Value.Should().BeEquivalentTo(expectedOptionValue);
      //}
    }
  }
}