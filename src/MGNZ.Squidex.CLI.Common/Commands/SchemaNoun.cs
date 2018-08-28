namespace MGNZ.Squidex.CLI.Common.Commands
{
  using System.Collections.Generic;

  using MGNZ.Squidex.CLI.Common.CLI;

  public class SchemaNoun : Noun
  {
    public SchemaNoun()
    {
      Names = new[ ] {"schema", "schemas ", "schemata"};
      Verbs = new Dictionary<string, Verb>
      {
        {
          "tag", new Verb(this)
          {
            Names = new[ ] {"category", "categorise", "tag"},
            Options = new Dictionary<string, Option>()
            {
              { "name", new Option() {ShortName = "n", LongName = "name", Required = true, OrdanalityOrder = 1} },
              { "token", new Option() {ShortName = "t", LongName = "tag", Required = true, OrdanalityOrder = 2} },
              { "alias-credentials", new Option() {ShortName = "c", LongName = "alias-credentials", Required = false} }
            }
          }
        },
        {
          "delete", new Verb(this)
          {
            Names = new[ ] {"delete"},
            Options = new Dictionary<string, Option>()
            {
              { "name", new Option() {ShortName = "n", LongName = "name", Required = true, OrdanalityOrder = 2} },
              { "alias-credentials", new Option() {ShortName = "c", LongName = "alias-credentials", Required = false} }
            }
          }
        },
        {
          "export", new Verb(this)
          {
            Names = new[ ] {"export"},
            Options = new Dictionary<string, Option>()
            {
              { "name", new Option() {ShortName = "n", LongName = "name", Required = true, OrdanalityOrder = 1} },
              { "alias-credentials", new Option() {ShortName = "c", LongName = "alias-credentials", Required = false} }
            }
          }
        },
        {
          "import", new Verb(this)
          {
            Names = new[ ] {"import"},
            Options = new Dictionary<string, Option>()
            {
              { "name", new Option() {ShortName = "n", LongName = "name", Required = true, OrdanalityOrder = 1} },
              { "path", new Option() {ShortName = "p", LongName = "path", Required = true, OrdanalityOrder = 2} },
              { "alias-credentials", new Option() {ShortName = "c", LongName = "alias-credentials", Required = false} }
            }
          }
        },
        {
          "list", new Verb(this)
          {
            Names = new[ ] {"list"},
            Options = new Dictionary<string, Option>()
            {
              { "alias-credentials", new Option() {ShortName = "c", LongName = "alias-credentials", Required = false} }
            }
          }
        }
      };
    }
  }
}