namespace MGNZ.Squidex.CLI.Common.Commands
{
  using System.Collections.Generic;

  using MGNZ.Squidex.CLI.Common.CLI;

  public class ContentNoun : Noun
  {
    public ContentNoun()
    {
      Names = new[ ] {"content", "contents"};
      Verbs = new Dictionary<string, Verb>
      {
        {
          "delete-content", new Verb
          {
            Names = new[ ] {"delete"},
            Options = new[ ]
            {
              new Option() {ShortName = "x", LongName = "schema", Required = true, OrdanalityOrder = 1},
              new Option() {ShortName = "id", LongName = "id", Required = true, OrdanalityOrder = 2},
              new Option() {ShortName = "c", LongName = "alias-credentials", Required = false},
            }
          }
        },
        {
          "import-content", new Verb
          {
            Names = new[ ] {"import"},
            Options = new[ ]
            {
              new Option() {ShortName = "x", LongName = "schema", Required = true, OrdanalityOrder = 1},
              new Option() {ShortName = "p", LongName = "path", Required = true, OrdanalityOrder = 2},
              new Option() {ShortName = "c", LongName = "alias-credentials", Required = false},
            }
          }
        },
        {
          "export-content", new Verb
          {
            Names = new[ ] {"export"},
            Options = new[ ]
            {
              new Option() {ShortName = "x", LongName = "schema", Required = true, OrdanalityOrder = 1},
              new Option() {ShortName = "p", LongName = "path", Required = true, OrdanalityOrder = 2},
              new Option() {ShortName = "a", LongName = "all", Required = true},
              new Option() {ShortName = "t", LongName = "top", Required = false},
              new Option() {ShortName = "s", LongName = "skip", Required = false},
              new Option() {ShortName = "o", LongName = "order-by", Required = false},
              new Option() {ShortName = "q", LongName = "query-by", Required = false},
              new Option() {ShortName = "f", LongName = "filter-by", Required = false},
              new Option() {ShortName = "c", LongName = "alias-credentials", Required = false},
            }
          }
        }
      };
    }
  }
}