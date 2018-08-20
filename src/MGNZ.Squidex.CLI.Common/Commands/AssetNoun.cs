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
              new Option() {ShortName = "n", LongName = "name", Required = true, Ordinal = true},
              new Option() {ShortName = "c", LongName = "alias-credentials", Required = false, Ordinal = false},
            }
          }
        },
        {
          "list-asset", new Verb
          {
            Names = new[ ] {"list"},
            Options = new[ ]
            {
              new Option() {ShortName = "c", LongName = "alias-credentials", Required = false, Ordinal = false},
            }
          }
        },
        {
          "import-asset", new Verb
          {
            Names = new[ ] {"import"},
            Options = new[ ]
            {
              new Option() {ShortName = "p", LongName = "path", Required = true, Ordinal = true},
              new Option() {ShortName = "n", LongName = "name", Required = true, Ordinal = true},
              new Option() {ShortName = "t", LongName = "tags", Required = true, Ordinal = true},
              new Option() {ShortName = "c", LongName = "alias-credentials", Required = false, Ordinal = false},
            }
          }
        },
        {
          "export-asset", new Verb
          {
            Names = new[ ] {"export"},
            Options = new[ ]
            {
              new Option() {ShortName = "n", LongName = "name", Required = true, Ordinal = false},
              new Option() {ShortName = "p", LongName = "path", Required = true, Ordinal = true},
              new Option() {ShortName = "c", LongName = "alias-credentials", Required = false, Ordinal = false},
            }
          }
        },
        {
          "tag-asset", new Verb
          {
            Names = new[ ] {"tag"},
            Options = new[ ]
            {
              new Option() {ShortName = "n", LongName = "name", Required = true, Ordinal = true},
              new Option() {ShortName = "t", LongName = "tags", Required = true, Ordinal = true},
              new Option() {ShortName = "c", LongName = "alias-credentials", Required = false, Ordinal = false},
            }
          }
        }
      };
    }
  }
}