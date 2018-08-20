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
          "tag-schema", new Verb
          {
            Names = new[ ] {"category", "categorise", "tag"},
            Options = new[ ]
            {
              new Option() {ShortName = "n", LongName = "name", Required = true, Ordinal = true},
              new Option() {ShortName = "t", LongName = "tag", Required = true, Ordinal = true},
              new Option() {ShortName = "c", LongName = "alias-credentials", Required = false, Ordinal = false},
            }
          }
        },
        {
          "delete-schema", new Verb
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
          "export-schema", new Verb
          {
            Names = new[ ] {"export"},
            Options = new[ ]
            {
              new Option() {ShortName = "n", LongName = "name", Required = true, Ordinal = true},
              new Option() {ShortName = "c", LongName = "alias-credentials", Required = false, Ordinal = false},
            }
          }
        },
        {
          "import-schema", new Verb
          {
            Names = new[ ] {"import"},
            Options = new[ ]
            {
              new Option() {ShortName = "n", LongName = "name", Required = true, Ordinal = true},
              new Option() {ShortName = "p", LongName = "path", Required = true, Ordinal = true},
              new Option() {ShortName = "c", LongName = "alias-credentials", Required = false, Ordinal = false},
            }
          }
        },
        {
          "list-schema", new Verb
          {
            Names = new[ ] {"list"},
            Options = new[ ]
            {
              new Option() {ShortName = "c", LongName = "alias-credentials", Required = false, Ordinal = false},
            }
          }
        }
      };
    }
  }
}