namespace MGNZ.Squidex.CLI.Tests.Commands
{
  using System.Collections.Generic;

  using MGNZ.Squidex.CLI.Common.Commands;
  using MGNZ.Squidex.CLI.Common.Routing;

  public class AssetNoun : Noun
  {
    public AssetNoun()
    {
      Names = new[ ] {"asset", "assets"};
      Verbs = new Dictionary<string, Verb>
      {
        {
          "delete", new Verb(this)
          {
            Names = new[ ] {"delete"},
            RequestType = typeof(AssetDeleteRequest),
            Options = new Dictionary<string, Option>()
            {
              { "name", new Option() {ShortName = "n", LongName = "name", Required = true, OrdanalityOrder = 1} },
              { "alias-credentials", new Option() {ShortName = "c", LongName = "alias-credentials", Required = false} }
            }
          }
        },
        {
          "list", new Verb(this)
          {
            Names = new[ ] {"list"},
            RequestType = typeof(AssetListRequest),
            Options = new Dictionary<string, Option>()
            {
              { "alias-credentials", new Option() {ShortName = "c", LongName = "alias-credentials", Required = false} }
            }
          }
        },
        {
          "import", new Verb(this)
          {
            Names = new[ ] {"import"},
            RequestType = typeof(AssetImportRequest),
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
          "export", new Verb(this)
          {
            Names = new[ ] {"export"},
            RequestType = typeof(AssetExportRequest),
            Options = new Dictionary<string, Option>()
            {
              { "name", new Option() {ShortName = "n", LongName = "name", Required = true, OrdanalityOrder = 1} },
              { "path", new Option() {ShortName = "p", LongName = "path", Required = true, OrdanalityOrder = 2} },
              { "alias-credentials", new Option() {ShortName = "c", LongName = "alias-credentials", Required = false} }
            }
          }
        },
        {
          "tag", new Verb(this)
          {
            Names = new[ ] {"tag"},
            RequestType = typeof(AssetTagRequest),
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