namespace MGNZ.Squidex.CLI.Tests.CLI
{
  using System.Collections.Generic;

  using FluentAssertions;

  using MGNZ.Squidex.CLI.Common.CLI;
  using MGNZ.Squidex.CLI.Common.Commands;
  using MGNZ.Squidex.CLI.Tests.Platform;

  using Xunit;

  [Trait("category", "unit")]
  public class CommandLineOperationMapperUnitTests
  {
    private static Dictionary<string, Noun> Nouns =>
      new Dictionary<string, Noun>()
      {
        { "app", new AppNoun() },
        { "asset", new AssetNoun() },
        { "content", new ContentNoun() },
        { "schema", new SchemaNoun() }
      };

    public static List<object[ ]> MapOperation_HappyPath_Data => new List<object[]>
    {
      // few ways to login
      new object[ ]
      {
        Nouns["app"], Nouns["app"].Verbs["login"],
        @"app login https://some.site/squidex app_name -t t_abc123 -a a_abc123"
      },
      new object[ ]
      {
        Nouns["app"], Nouns["app"].Verbs["login"],
        @"login app https://some.site/squidex app_name --client-id cid_abc123 --client-secret cs_abc123 -a a_abc123"
      },

      // some ways to list schemas

      // some ways to export a schema

      // try different ways to logout
      new object[ ]
      {
        Nouns["app"], Nouns["app"].Verbs["logout"],
        @"app logout --name app_name"
      },
      new object[ ]
      {
        Nouns["app"], Nouns["app"].Verbs["logout"],
        @"app logout -n app_name"
      },
      new object[ ]
      {
        Nouns["app"], Nouns["app"].Verbs["logout"],
        @"logout app app_name"
      }
    };


    [Theory]
    [MemberData(nameof(MapOperation_HappyPath_Data))]
    public void MapOperation_HappyPath(Noun expectedNoun, Verb expectedVerb, string commandLine)
    {
      var sut = new CommandLineOperationMapper(SerilogFixture.UsefullLogger<CommandLineOperationMapper>(),
        new Noun[ ] {new AppNoun(), new AssetNoun(), new ContentNoun(), new SchemaNoun()});

      var actual = sut.MapOperation(commandLine);

      actual.noun.Should().BeEquivalentTo(expectedNoun);
      actual.verb.Should().BeEquivalentTo(expectedVerb);
    }
  }
}