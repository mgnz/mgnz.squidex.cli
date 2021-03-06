namespace MGNZ.Squidex.CLI.Tests.Routing
{
  using System.Collections.Generic;

  using MGNZ.Squidex.CLI.Common.Commands;
  using MGNZ.Squidex.CLI.Common.Routing;

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
            RequestType = typeof(SchemaTagRequest),
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
            RequestType = typeof(SchemaDeleteRequest),
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
            RequestType = typeof(SchemaExportRequest),
            Options = new Dictionary<string, Option>()
            {
              { "name", new Option() {ShortName = "n", LongName = "name", Required = true, OrdanalityOrder = 1} },
              { "path", new Option() {ShortName = "p", LongName = "path", Required = true, OrdanalityOrder = 2} },
              { "alias-credentials", new Option() {ShortName = "c", LongName = "alias-credentials", Required = false} }
            }
          }
        },
        {
          "import", new Verb(this)
          {
            Names = new[ ] {"import"},
            RequestType = typeof(SchemaImportRequest),
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
            RequestType = typeof(SchemaListRequest),
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