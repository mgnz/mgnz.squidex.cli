namespace MGNZ.Squidex.CLI.Tests.CLI
{
  using System.Collections.Generic;

  using MGNZ.Squidex.CLI.Common.Commands;

  using MGNZ.Squidex.CLI.Common.CLI;

  using Xunit;

  public class CommandStructureUnitTests
  {
    [Fact]
    public void StructurePlayPen()
    {
      var app = new Noun
      {
        Names = new[ ] {"app", "apps"},
        Verbs = new Dictionary<string, Verb>
        {
          {
            "delete-app",
            new Verb
            {
              Names = new[ ] {"delete"},
              Operation = new Operation() { Arguments = new DeleteAppArguments() {}},
              Options = new []
              {
                new Option() { ShortName = "n", LongName = "name", Required = true, Ordinal = true },
                new Option() { ShortName = "t", LongName = "token", Required = true, Ordinal = true },
              }
            }
          },

          {
            "list-app", new Verb
            {
              Names = new[ ] {"list"},
              Operation = new Operation() { Arguments = new ListAppArguments() {}},
              Options = new []
              {
                new Option() { ShortName = "t", LongName = "token", Required = true, Ordinal = true },
              }
            }
          },
          {
            "login-app", new Verb
            {
              Names = new[ ] {"login"},
              Operation = new Operation() { Arguments = new LoginAppArguments() {}},
              Options = new []
              {
                new Option() { ShortName = "n", LongName = "name", Required = true, Ordinal = true },
                new Option() { ShortName = "cid", LongName = "client-id", Required = false, Ordinal = false },
                new Option() { ShortName = "cs", LongName = "client-secret", Required = false, Ordinal = false },
                new Option() { ShortName = "t", LongName = "token", Required = false, Ordinal = false },
                new Option() { ShortName = "c", LongName = "alias-credentials", Required = false, Ordinal = false },
                new Option() { ShortName = "a", LongName = "alias-credentials-as", Required = false, Ordinal = false },
              }
            }
          },
          {
            "logout-app", new Verb
            {
              Names = new[ ] {"logout"},
              Operation = new Operation() { Arguments = new LogoutAppArguments() {}},
              Options = new []
              {
                new Option() { ShortName = "n", LongName = "name", Required = true, Ordinal = true },
                new Option() { ShortName = "c", LongName = "alias-credentials", Required = false, Ordinal = false },
              }
            }
          },
          {
            "new-app", new Verb
            {
              Names = new[ ] {"new"},
              Operation = new Operation() { Arguments = new NewAppArguments() {}},
              Options = new []
              {
                new Option() { ShortName = "n", LongName = "name", Required = true, Ordinal = true },
                new Option() { ShortName = "t", LongName = "token", Required = true, Ordinal = true },
              }
            }
          }
        }
      };
      var asset = new Noun
      {
        Names = new[ ] {"asset", "assets"},
        Verbs = new Dictionary<string, Verb>
        {
          {
            "delete-asset", new Verb
            {
              Names = new[ ] {"delete"},
              Operation = new Operation() { Arguments = new DeleteAssetArguments() {}},
              Options = new []
              {
                new Option() { ShortName = "n", LongName = "name", Required = true, Ordinal = true },
                new Option() { ShortName = "c", LongName = "alias-credentials", Required = false, Ordinal = false },
              }
            }
          },
          {
            "list-asset", new Verb
            {
              Names = new[ ] {"list"},
              Operation = new Operation() { Arguments = new ListAssetArguments() {}},
              Options = new []
              {
                new Option() { ShortName = "c", LongName = "alias-credentials", Required = false, Ordinal = false },
              }
            }
          },
          {
            "import-asset", new Verb
            {
              Names = new[ ] {"import"},
              Operation = new Operation() { Arguments = new ImportAssetArguments() {}},
              Options = new []
              {
                new Option() { ShortName = "p", LongName = "path", Required = true, Ordinal = true },
                new Option() { ShortName = "n", LongName = "name", Required = true, Ordinal = true },
                new Option() { ShortName = "t", LongName = "tags", Required = true, Ordinal = true },
                new Option() { ShortName = "c", LongName = "alias-credentials", Required = false, Ordinal = false },
              }
            }
          },
          {
            "export-asset", new Verb
            {
              Names = new[ ] {"export"},
              Operation = new Operation() { Arguments = new ExportAssetArguments() {}},
              Options = new []
              {
                new Option() { ShortName = "n", LongName = "name", Required = true, Ordinal = false },
                new Option() { ShortName = "p", LongName = "path", Required = true, Ordinal = true },
                new Option() { ShortName = "c", LongName = "alias-credentials", Required = false, Ordinal = false },
              }
            }
          },
          {
            "tag-asset", new Verb
            {
              Names = new[ ] {"tag"},
              Operation = new Operation() { Arguments = new TagAssetArguments() {}},
              Options = new []
              {
                new Option() { ShortName = "n", LongName = "name", Required = true, Ordinal = true },
                new Option() { ShortName = "t", LongName = "tags", Required = true, Ordinal = true },
                new Option() { ShortName = "c", LongName = "alias-credentials", Required = false, Ordinal = false },
              }
            }
          }
        }
      };
      var content = new Noun
      {
        Names = new[ ] {"content", "contents"},
        Verbs = new Dictionary<string, Verb>
        {
          {
            "delete-content", new Verb
            {
              Names = new[ ] {"delete"},
              Operation = new Operation() { Arguments = new DeleteContentArguments() {}},
              Options = new []
              {
                new Option() { ShortName = "x", LongName = "schema", Required = true, Ordinal = true },
                new Option() { ShortName = "id", LongName = "id", Required = true, Ordinal = true },
                new Option() { ShortName = "c", LongName = "alias-credentials", Required = false, Ordinal = false },
              }
            }
          },
          {
            "import-content", new Verb
            {
              Names = new[ ] {"import"},
              Operation = new Operation() { Arguments = new ImportContentArguments() {}},
              Options = new []
              {
                new Option() { ShortName = "x", LongName = "schema", Required = true, Ordinal = true },
                new Option() { ShortName = "p", LongName = "path", Required = true, Ordinal = true },
                new Option() { ShortName = "c", LongName = "alias-credentials", Required = false, Ordinal = false },
              }
            }
          },
          {
            "export-content", new Verb
            {
              Names = new[ ] {"export"},
              Operation = new Operation() { Arguments = new ExportContentArguments() {}},
              Options = new []
              {
                new Option() { ShortName = "x", LongName = "schema", Required = true, Ordinal = true },
                new Option() { ShortName = "p", LongName = "path", Required = true, Ordinal = true },
                new Option() { ShortName = "a", LongName = "all", Required = true, Ordinal = false },
                new Option() { ShortName = "t", LongName = "top", Required = false, Ordinal = false },
                new Option() { ShortName = "s", LongName = "skip", Required = false, Ordinal = false },
                new Option() { ShortName = "o", LongName = "order-by", Required = false, Ordinal = false },
                new Option() { ShortName = "q", LongName = "query-by", Required = false, Ordinal = false },
                new Option() { ShortName = "f", LongName = "filter-by", Required = false, Ordinal = false },
                new Option() { ShortName = "c", LongName = "alias-credentials", Required = false, Ordinal = false },
              }
            }
          }
        }
      };
      var schema = new Noun
      {
        Names = new[ ] {"schema", "schemas ", "schemata"},
        Verbs = new Dictionary<string, Verb>
        {
          {
            "tag-schema", new Verb
            {
              Names = new[ ] {"category", "categorise", "tag"},
              Operation = new Operation() { Arguments = new ImportSchemaArguments() {}},
              Options = new []
              {
                new Option() { ShortName = "n", LongName = "name", Required = true, Ordinal = true },
                new Option() { ShortName = "t", LongName = "tag", Required = true, Ordinal = true },
                new Option() { ShortName = "c", LongName = "alias-credentials", Required = false, Ordinal = false },
              }
            }
          },
          {
            "delete-schema", new Verb
            {
              Names = new[ ] {"delete"},
              Operation = new Operation() { Arguments = new DeleteSchemaArguments() {}},
              Options = new []
              {
                new Option() { ShortName = "n", LongName = "name", Required = true, Ordinal = true },
                new Option() { ShortName = "c", LongName = "alias-credentials", Required = false, Ordinal = false },
              }
            }
          },
          {
            "export-schema", new Verb
            {
              Names = new[ ] {"export"},
              Operation = new Operation() { Arguments = new ExportSchemaArguments() {}},
              Options = new []
              {
                new Option() { ShortName = "n", LongName = "name", Required = true, Ordinal = true },
                new Option() { ShortName = "c", LongName = "alias-credentials", Required = false, Ordinal = false },
              }
            }
          },
          {
            "import-schema", new Verb
            {
              Names = new[ ] {"import"},
              Operation = new Operation() { Arguments = new ImportSchemaArguments() {}},
              Options = new []
              {
                new Option() { ShortName = "n", LongName = "name", Required = true, Ordinal = true },
                new Option() { ShortName = "p", LongName = "path", Required = true, Ordinal = true },
                new Option() { ShortName = "c", LongName = "alias-credentials", Required = false, Ordinal = false },
              }
            }
          },
          {
            "list-schema", new Verb
            {
              Names = new[ ] {"list"},
              Operation = new Operation() { Arguments = new ListSchemaArguments() {}},
              Options = new []
              {
                new Option() { ShortName = "c", LongName = "alias-credentials", Required = false, Ordinal = false },
              }
            }
          }
        }
      };
    }
  }
}