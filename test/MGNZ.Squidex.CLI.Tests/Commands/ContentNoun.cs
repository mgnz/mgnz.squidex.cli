namespace MGNZ.Squidex.CLI.Tests.Commands
{
  using System.Collections.Generic;

  using MGNZ.Squidex.CLI.Common.Commands;
  using MGNZ.Squidex.CLI.Common.Routing;

  public class ContentNoun : Noun
  {
    public ContentNoun()
    {
      Names = new[ ] {"content", "contents"};
      Verbs = new Dictionary<string, Verb>
      {
        {
          "delete", new Verb(this)
          {
            Names = new[ ] {"delete"},
            RequestType = typeof(ContentDeleteRequest),
            Options = new Dictionary<string, Option>()
            {
              { "sc", new Option() {ShortName = "sc", LongName = "schema", Required = true, OrdanalityOrder = 1} },
              { "id", new Option() {ShortName = "id", LongName = "id", Required = true, OrdanalityOrder = 2} },
              { "alias-credentials", new Option() {ShortName = "c", LongName = "alias-credentials", Required = false} }
            }
          }
        },
        {
          "import", new Verb(this)
          {
            Names = new[ ] {"import"},
            RequestType = typeof(ContentImportRequest),
            Options = new Dictionary<string, Option>()
            {
              { "sc", new Option() {ShortName = "sc", LongName = "schema", Required = true, OrdanalityOrder = 1} },
              { "path", new Option() {ShortName = "p", LongName = "path", Required = true, OrdanalityOrder = 2} },
              { "alias-credentials", new Option() {ShortName = "c", LongName = "alias-credentials", Required = false} }
            }
          }
        },
        {
          "export", new Verb(this)
          {
            Names = new[ ] {"export"},
            RequestType = typeof(ContentExportRequest),
            Options = new Dictionary<string, Option>()
            {
              { "sc", new Option() {ShortName = "sc", LongName = "schema", Required = true, OrdanalityOrder = 1} },
              { "path", new Option() {ShortName = "p", LongName = "path", Required = true, OrdanalityOrder = 2} },
              { "all", new Option() {ShortName = "a", LongName = "all", Required = true} },
              { "token", new Option() {ShortName = "t", LongName = "top", Required = false} },
              { "skip", new Option() {ShortName = "s", LongName = "skip", Required = false} },
              { "order-by", new Option() {ShortName = "ob", LongName = "order-by", Required = false} },
              { "query-by", new Option() {ShortName = "qb", LongName = "query-by", Required = false}  },
              { "filter-by", new Option() {ShortName = "fb", LongName = "filter-by", Required = false} },
              { "alias-credentials", new Option() {ShortName = "c", LongName = "alias-credentials", Required = false} }
            }
          }
        }
      };
    }
  }
}