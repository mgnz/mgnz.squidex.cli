namespace MGNZ.Squidex.CLI.Tests.CLI
{
  using System.Collections.Generic;

  using MGNZ.Squidex.CLI.Common.CLI;

  using Xunit;

  public class CommandLineDictionaryConverterUnitTests
  {
    public static List<object[ ]> Parse_HappyPath_Data =>
      new List<object[ ]>
      {
        // few ways to login
        new[ ] {new object(), @"app login https://some.site/squidex app_name -t t_abc123 -a a_abc123" },
        new[ ] {new object(), @"app login https://some.site/squidex app_name --client-id cid_abc123 client-secret cs_abc123 -a a_abc123"},

        // some ways to list schemas

        // some ways to export a schema

        // try different ways to logout
        new[ ] {new object(), @"app logout --app-name app_name"},
        new[ ] {new object(), @"app logout -n app_name"},
        new[ ] {new object(), @"app logout app_name"}
      };

    [Fact]
    public void Parse_HappyPath()
    {
      //var sut = new CommandLineDictionaryConverter();
    }
  }
}