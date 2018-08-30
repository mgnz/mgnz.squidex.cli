namespace MGNZ.Squidex.CLI.Tests.CLI
{
  using System.Collections.Generic;

  using MGNZ.Squidex.CLI.Common.CLI;
  using MGNZ.Squidex.CLI.Common.Commands;
  using MGNZ.Squidex.CLI.Tests.Platform;

  using Xunit;

  public class CommandLineDictionaryConverterUnitTests
  {
    public static List<object[ ]> Parse_HappyPath_Data =>
      new List<object[ ]>
      {
        // few ways to login
        new object[ ] { new Dictionary<string, string>()
        {
          { "noun", "app" }, { "verb", "login"},
          { "1", "https://some.site/squidex" },{ "2", "app_name" },{ "-t", "t_abc123" },{ "-a", "a_abc123" }
        }, @"app login https://some.site/squidex app_name -t t_abc123 -a a_abc123" },
        new object[ ] { new Dictionary<string, string>()
        {
          { "noun", "app" }, { "verb", "login"},
          { "1", "https://some.site/squidex" },{ "2", "app_name" },{ "--client-id", "cid_abc123" },{ "--client-secret", "cs_abc123" },{ "-a", "a_abc123" }
        }, @"login app https://some.site/squidex app_name --client-id cid_abc123 --client-secret cs_abc123 -a a_abc123"},

        // some ways to list schemas

        // some ways to export a schema

        // try different ways to logout
        new object[ ] { new Dictionary<string, string>()
        {
          { "noun", "app" }, { "verb", "logout"},
          { "--name", "app_name" }
        }, @"app logout --name app_name"},
        new object[ ] { new Dictionary<string, string>()
        {
          { "noun", "app" }, { "verb", "logout"},
          { "-n", "app_name" }
        }, @"app logout -n app_name"},
        new object[ ] { new Dictionary<string, string>()
        {
          { "noun", "app" }, { "verb", "logout"},
          { "1", "app_name" }
        }, @"logout app app_name"}
      };

    [Theory]
    [MemberData(nameof(Parse_HappyPath_Data))]
    public void Parse_HappyPath(object expected, string commandLine)
    {
      var sut = new CommandLineOperationMapper(SerilogFixture.UsefullLogger<CommandLineOperationMapper>(), new Noun[]{ new AppNoun(), new AssetNoun(), new ContentNoun(), new SchemaNoun() });

      var actual = sut.MapOperation(commandLine);
    }
  }
}