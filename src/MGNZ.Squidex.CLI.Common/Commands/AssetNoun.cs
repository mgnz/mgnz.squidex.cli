namespace MGNZ.Squidex.CLI.Common.Commands
{
  using System.Collections.Generic;

  using MGNZ.Squidex.CLI.Common.CLI;

  public class AssetNoun : Noun
  {
    public AssetNoun()
    {
      Names = new[ ] {"asset", "assets"};
      Verbs = new Dictionary<string, Verb>
      {
        {
          "delete", new Verb
          {
            Names = new[ ] {"delete"},
            Options = new Dictionary<string, Option>()
            {
              { "name", new Option() {ShortName = "n", LongName = "name", Required = true, OrdanalityOrder = 1} },
              { "alias-credentials", new Option() {ShortName = "c", LongName = "alias-credentials", Required = false} }
            }
          }
        },
        {
          "list", new Verb
          {
            Names = new[ ] {"list"},
            Options = new Dictionary<string, Option>()
            {
              { "alias-credentials", new Option() {ShortName = "c", LongName = "alias-credentials", Required = false} }
            }
          }
        },
        {
          "import", new Verb
          {
            Names = new[ ] {"import"},
            Options = new Dictionary<string, Option>()
            {
              { "path", new Option() {ShortName = "p", LongName = "path", Required = true, OrdanalityOrder = 1} },
              { "name", new Option() {ShortName = "n", LongName = "name", Required = true, OrdanalityOrder = 2} },
              { "token", new Option() {ShortName = "t", LongName = "tags", Required = true, OrdanalityOrder = 3} },
              { "alias-credentials", new Option() {ShortName = "c", LongName = "alias-credentials", Required = false} }
            }
          }
        },
        {
          "export", new Verb
          {
            Names = new[ ] {"export"},
            Options = new Dictionary<string, Option>()
            {
              { "name", new Option() {ShortName = "n", LongName = "name", Required = true, OrdanalityOrder = 1} },
              { "path", new Option() {ShortName = "p", LongName = "path", Required = true, OrdanalityOrder = 2} },
              { "alias-credentials", new Option() {ShortName = "c", LongName = "alias-credentials", Required = false} }
            }
          }
        },
        {
          "tag", new Verb
          {
            Names = new[ ] {"tag"},
            Options = new Dictionary<string, Option>()
            {
              { "name", new Option() {ShortName = "n", LongName = "name", Required = true, OrdanalityOrder = 1} },
              { "token", new Option() {ShortName = "t", LongName = "tags", Required = true, OrdanalityOrder = 2} },
              { "alias-credentials", new Option() {ShortName = "c", LongName = "alias-credentials", Required = false} }
            }
          }
        }
      };
    }
  }
}