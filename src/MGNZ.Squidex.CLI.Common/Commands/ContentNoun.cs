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
              new Option() {ShortName = "x", LongName = "schema", Required = true, Ordinal = true},
              new Option() {ShortName = "id", LongName = "id", Required = true, Ordinal = true},
              new Option() {ShortName = "c", LongName = "alias-credentials", Required = false, Ordinal = false},
            }
          }
        },
        {
          "import-content", new Verb
          {
            Names = new[ ] {"import"},
            Options = new[ ]
            {
              new Option() {ShortName = "x", LongName = "schema", Required = true, Ordinal = true},
              new Option() {ShortName = "p", LongName = "path", Required = true, Ordinal = true},
              new Option() {ShortName = "c", LongName = "alias-credentials", Required = false, Ordinal = false},
            }
          }
        },
        {
          "export-content", new Verb
          {
            Names = new[ ] {"export"},
            Options = new[ ]
            {
              new Option() {ShortName = "x", LongName = "schema", Required = true, Ordinal = true},
              new Option() {ShortName = "p", LongName = "path", Required = true, Ordinal = true},
              new Option() {ShortName = "a", LongName = "all", Required = true, Ordinal = false},
              new Option() {ShortName = "t", LongName = "top", Required = false, Ordinal = false},
              new Option() {ShortName = "s", LongName = "skip", Required = false, Ordinal = false},
              new Option() {ShortName = "o", LongName = "order-by", Required = false, Ordinal = false},
              new Option() {ShortName = "q", LongName = "query-by", Required = false, Ordinal = false},
              new Option() {ShortName = "f", LongName = "filter-by", Required = false, Ordinal = false},
              new Option() {ShortName = "c", LongName = "alias-credentials", Required = false, Ordinal = false},
            }
          }
        }
      };
    }
  }
}