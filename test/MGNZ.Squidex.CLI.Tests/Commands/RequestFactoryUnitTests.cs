namespace MGNZ.Squidex.CLI.Tests.Commands
{
  using System;
  using System.Collections.Generic;

  using FluentAssertions;

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
        new Dictionary<string, Option>()
        {
          {"url", GetOptionWithValue("app", "login", "url", "https://some.site/squidex")},
          {"name", GetOptionWithValue("app", "login", "name", "app_name")},
          {"token", GetOptionWithValue("app", "login", "token", "t_abc123")},
          {"alias-credentials-as", GetOptionWithValue("app", "login", "alias-credentials-as", "a_abc123")},
        },
        Nouns["app"], Nouns["app"].Verbs["login"],
        @"app login https://some.site/squidex app_name -t t_abc123 -a a_abc123"
      },
      new object[ ]
      {
        new Dictionary<string, Option>()
        {
          {"url", GetOptionWithValue("app", "login", "url", "https://some.site/squidex")},
          {"name", GetOptionWithValue("app", "login", "name", "app_name")},
          {"client-id", GetOptionWithValue("app", "login", "client-id", "cid_abc123")},
          {"client-secret", GetOptionWithValue("app", "login", "client-secret", "cs_abc123")},
          {"alias-credentials-as", GetOptionWithValue("app", "login", "alias-credentials-as", "a_abc123")},
        },
        Nouns["app"], Nouns["app"].Verbs["login"],
        @"login app https://some.site/squidex app_name --client-id cid_abc123 --client-secret cs_abc123 -a a_abc123"
      },

      // some ways to list schemas

      // some ways to export a schema

      // try different ways to logout
      new object[ ]
      {
        new Dictionary<string, Option>()
        {
          {"name", GetOptionWithValue("app", "login", "name", "app_name")},
        },
        Nouns["app"], Nouns["app"].Verbs["logout"],
        @"app logout --name app_name"
      },
      new object[ ]
      {
        new Dictionary<string, Option>()
        {
          {"name", GetOptionWithValue("app", "login", "name", "app_name")},
        },
        Nouns["app"], Nouns["app"].Verbs["logout"],
        @"app logout -n app_name"
      },
      new object[ ]
      {
        new Dictionary<string, Option>()
        {
          {"name", GetOptionWithValue("app", "login", "name", "app_name")},
        },
        Nouns["app"], Nouns["app"].Verbs["logout"],
        @"logout app app_name"
      }
    };


    [Theory]
    [MemberData(nameof(MapOperation_HappyPath_Data))]
    public void MapOperation_HappyPath(Dictionary<string, Option> expectedOptions, Noun noun, Verb verb, string commandLine)
    {
      var sut = new RequestFactory(SerilogFixture.UsefullLogger<RequestFactory>());

      var actual = sut.MapParameters(noun, verb, commandLine);
      actual.Should().NotBeNull();
      actual.Count.Should().Be(verb.Options.Count);

      foreach (var option in expectedOptions)
      {
        var expectedOption = option.Value.Item1;
        var expectedOptionValue = option.Value.Item2;

        var actualOption = actual[expectedOption.GetLongNameFormatted];

        actualOption.Value.Should().BeEquivalentTo(expectedOptionValue);
      }
    }
}