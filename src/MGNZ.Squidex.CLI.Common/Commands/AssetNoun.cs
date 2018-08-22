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
          "delete-asset", new Verb
          {
            Names = new[ ] {"delete"},
            Options = new[ ]
            {
              new Option() {ShortName = "n", LongName = "name", Required = true, OrdanalityOrder = 1},
              new Option() {ShortName = "c", LongName = "alias-credentials", Required = false},
            }
          }
        },
        {
          "list-asset", new Verb
          {
            Names = new[ ] {"list"},
            Options = new[ ]
            {
              new Option() {ShortName = "c", LongName = "alias-credentials", Required = false},
            }
          }
        },
        {
          "import-asset", new Verb
          {
            Names = new[ ] {"import"},
            Options = new[ ]
            {
              new Option() {ShortName = "p", LongName = "path", Required = true, OrdanalityOrder = 1},
              new Option() {ShortName = "n", LongName = "name", Required = true, OrdanalityOrder = 2},
              new Option() {ShortName = "t", LongName = "tags", Required = true, OrdanalityOrder = 3},
              new Option() {ShortName = "c", LongName = "alias-credentials", Required = false},
            }
          }
        },
        {
          "export-asset", new Verb
          {
            Names = new[ ] {"export"},
            Options = new[ ]
            {
              new Option() {ShortName = "n", LongName = "name", Required = true, OrdanalityOrder = 1},
              new Option() {ShortName = "p", LongName = "path", Required = true, OrdanalityOrder = 2},
              new Option() {ShortName = "c", LongName = "alias-credentials", Required = false},
            }
          }
        },
        {
          "tag-asset", new Verb
          {
            Names = new[ ] {"tag"},
            Options = new[ ]
            {
              new Option() {ShortName = "n", LongName = "name", Required = true, OrdanalityOrder = 1},
              new Option() {ShortName = "t", LongName = "tags", Required = true, OrdanalityOrder = 2},
              new Option() {ShortName = "c", LongName = "alias-credentials", Required = false},
            }
          }
        }
      };
    }
  }
}