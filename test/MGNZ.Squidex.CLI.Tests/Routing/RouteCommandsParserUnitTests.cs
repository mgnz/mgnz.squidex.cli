namespace MGNZ.Squidex.CLI.Tests.Routing
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  using FluentAssertions;

  using MGNZ.Squidex.CLI.Common.Routing;
  using MGNZ.Squidex.CLI.Common.Commands;
  using MGNZ.Squidex.CLI.Tests.Commands;
  using MGNZ.Squidex.CLI.Tests.Platform;

  using Xunit;

  [Trait("category", "unit")]
  public class RouteCommandsParserUnitTests
  {
    private static Dictionary<string, Noun> Nouns =>
      new Dictionary<string, Noun>()
      {
        { "app", new AppNoun() },
        { "asset", new AssetNoun() },
        { "content", new ContentNoun() },
        { "schema", new SchemaNoun() }
      };

    public static List<object[ ]> ParseAndPopulateOperation_HappyPath_Data => new List<object[]>
    {
      // few ways to login
      new object[ ]
      {
        // expected
        new AppNoun(new Dictionary<string, Dictionary<string, string>>()
        {
          {"login", new Dictionary<string, string>()
          {
            // keep defaults
          }}
        }),
        @"app login https://some.site/squidex app_name -t t_abc123 -a a_abc123"
      },
      new object[ ]
      {
        // expected
        new AppNoun(new Dictionary<string, Dictionary<string, string>>()
        {
          {"login", new Dictionary<string, string>()
          {
            // keep defaults
          }}
        }),
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
            // keep defaults
          }}
        }),
        @"app logout --name app_name"
      },
      new object[ ]
      {
        // expected
        new AppNoun(new Dictionary<string, Dictionary<string, string>>()
        {
          {"logout", new Dictionary<string, string>()
          {
            // keep defaults
          }}
        }),
        @"app logout -n app_name"
      },
      new object[ ]
      {
        // expected
        new AppNoun(new Dictionary<string, Dictionary<string, string>>()
        {
          {"logout", new Dictionary<string, string>()
          {
            // keep defaults
          }}
        }),
        @"logout app app_name"
      }
    };


    [Theory]
    [MemberData(nameof(ParseAndPopulateOperation_HappyPath_Data))]
    public void ParseAndPopulateOperation_HappyPath(Noun expectedNounVerbPair, string commandLine)
    {
      var preCachedNouns = new Noun[ ] {new AppNoun(), new AssetNoun(), new ContentNoun(), new SchemaNoun()};

      var sut = new RouteCommandsParser(SerilogFixture.UsefullLogger<RouteCommandsParser>());

      var actualNounVerbPair = sut.ParseAndPopulateOperation(preCachedNouns, commandLine.Split(' '));

      actualNounVerbPair.Should().BeEquivalentTo(expectedNounVerbPair);
    }
  }
}