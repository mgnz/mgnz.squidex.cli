namespace MGNZ.Squidex.CLI.Tests.CLI
{
  using System.Collections.Generic;
  using System.Linq;

  using FluentAssertions;

  using MGNZ.Squidex.CLI.Common.CLI;
  using MGNZ.Squidex.CLI.Common.Commands;
  using MGNZ.Squidex.CLI.Tests.Platform;

  using Xunit;

  [Trait("category", "unit")]
  public class RouteOptionsParserUnitTests
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
        // expected
        new AppNoun(new Dictionary<string, Dictionary<string, string>>()
        {
          {"login", new Dictionary<string, string>()
          {
            {"url", "https://some.site/squidex"},
            {"name","app_name"},
            {"token", "t_abc123"},

            {"alias-credentials-as", "a_abc123"}
          }}
        }),
        // input : app-login
        new AppNoun(new Dictionary<string, Dictionary<string, string>>()
        {
          {"login", new Dictionary<string, string>()
          {
          }}
        }),
        // input : command line
        @"app login https://some.site/squidex app_name -t t_abc123 -a a_abc123"
      },
      new object[ ]
      {
        // expected
        new AppNoun(new Dictionary<string, Dictionary<string, string>>()
        {
          {"login", new Dictionary<string, string>()
          {
            {"url", "https://some.site/squidex"},
            {"name", "app_name"},
            {"client-id", "cid_abc123"},
            {"client-secret", "cs_abc123"},
            {"alias-credentials-as", "a_abc123"}
          }}
        }),
        // input : app-login
        new AppNoun(new Dictionary<string, Dictionary<string, string>>()
        {
          {"login", new Dictionary<string, string>()
          {
          }}
        }),
        // input : command line
        @"login app https://some.site/squidex app_name --client-id cid_abc123 --client-secret cs_abc123 -a a_abc123"
      },

      // some ways to list schemas

      // some ways to export a schema

      // try different ways to logout
      new object[ ]
      {
        // expected
        new AppNoun(new Dictionary<string, Dictionary<string, string>>()
        {
          {"logout", new Dictionary<string, string>()
          {
            {"name", "app_name"},
          }}
        }),
        // input : app-login
        new AppNoun(new Dictionary<string, Dictionary<string, string>>()
        {
          {"logout", new Dictionary<string, string>()
          {
          }}
        }),
        // input : command line
        @"app logout --name app_name"
      },
      new object[ ]
      {
        // expected
        new AppNoun(new Dictionary<string, Dictionary<string, string>>()
        {
          {"logout", new Dictionary<string, string>()
          {
            {"name", "app_name"},
          }}
        }),
        // input : app-login
        new AppNoun(new Dictionary<string, Dictionary<string, string>>()
        {
          {"logout", new Dictionary<string, string>()
          {
          }}
        }),
        // input : command line
        @"app logout -n app_name"
      },
      new object[ ]
      {
        // expected
        new AppNoun(new Dictionary<string, Dictionary<string, string>>()
        {
          {"logout", new Dictionary<string, string>()
          {
            {"name", "app_name"},
          }}
        }),
        // input : app-login
        new AppNoun(new Dictionary<string, Dictionary<string, string>>()
        {
          {"logout", new Dictionary<string, string>()
          {
          }}
        }),
        // input : command line
        @"logout app app_name"
      }
    };


    [Theory]
    [MemberData(nameof(MapOperation_HappyPath_Data))]
    public void MapOperation_HappyPath(Noun expectedNoun, Noun inputNoun, string commandLine)
    {
      // remember : inputNoun is partially pre-populated; ParseAndPopulateOptions is to fill in the
      // values for any options that have been defined.

      var sut = new RouteOptionsParser(SerilogFixture.UsefullLogger<RouteOptionsParser>());

      sut.ParseAndPopulateOptions(ref inputNoun, commandLine);
      inputNoun.Should().NotBeNull();
      inputNoun.Verbs.Should().HaveCount(1);
      inputNoun.Verbs.Single().Value.Should().NotBeNull();

      var expectedOptions = expectedNoun.Verbs.Single().Value.Options;
      var actualOptions = inputNoun.Verbs.Single().Value.Options;

      actualOptions.Keys.Should().Contain(expectedOptions.Keys);

      foreach (var currentKey in expectedOptions.Keys)
      {
        var currentExpectedOption = expectedOptions[currentKey];
        var currentActualOption = actualOptions[currentKey];

        currentActualOption.Should().BeEquivalentTo(currentExpectedOption);
      }
    }
  }
}