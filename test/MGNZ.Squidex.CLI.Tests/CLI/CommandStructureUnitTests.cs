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
              Operation = new Operation() { Arguments = new DeleteAppArguments() {}}
            }
          },
          {
            "list-app", new Verb
            {
              Names = new[ ] {"list"},
              Operation = new Operation() { Arguments = new ListAppArguments() {}}
            }
          },
          {
            "login-app", new Verb
            {
              Names = new[ ] {"login"},
              Operation = new Operation() { Arguments = new LoginAppArguments() {}}
            }
          },
          {
            "logout-app", new Verb
            {
              Names = new[ ] {"logout"},
              Operation = new Operation() { Arguments = new LogoutAppArguments() {}}
            }
          },
          {
            "new-app", new Verb
            {
              Names = new[ ] {"new"},
              Operation = new Operation() { Arguments = new NewAppArguments() {}}
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
              Operation = new Operation() { Arguments = new DeleteAssetArguments() {}}
            }
          },
          {
            "list-asset", new Verb
            {
              Names = new[ ] {"list"},
              Operation = new Operation() { Arguments = new ListAssetArguments() {}}
            }
          },
          {
            "import-asset", new Verb
            {
              Names = new[ ] {"import"},
              Operation = new Operation() { Arguments = new ImportAssetArguments() {}}
            }
          },
          {
            "export-asset", new Verb
            {
              Names = new[ ] {"export"},
              Operation = new Operation() { Arguments = new ExportAssetArguments() {}}
            }
          },
          {
            "tag-asset", new Verb
            {
              Names = new[ ] {"tag"},
              Operation = new Operation() { Arguments = new TagAssetArguments() {}}
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
              Operation = new Operation() { Arguments = new DeleteContentArguments() {}}
            }
          },
          {
            "import-content", new Verb
            {
              Names = new[ ] {"import"},
              Operation = new Operation() { Arguments = new ImportContentArguments() {}}
            }
          },
          {
            "export-content", new Verb
            {
              Names = new[ ] {"export"},
              Operation = new Operation() { Arguments = new ExportContentArguments() {}}
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
            "delete-schema", new Verb
            {
              Names = new[ ] {"delete"},
              Operation = new Operation() { Arguments = new DeleteSchemaArguments() {}}
            }
          },
          {
            "list-schema", new Verb
            {
              Names = new[ ] {"list"},
              Operation = new Operation() { Arguments = new ListSchemaArguments() {}}
            }
          },
          {
            "export-schema", new Verb
            {
              Names = new[ ] {"export"},
              Operation = new Operation() { Arguments = new ExportSchemaArguments() {}}
            }
          },
          {
            "import-schema", new Verb
            {
              Names = new[ ] {"import"},
              Operation = new Operation() { Arguments = new ImportSchemaArguments() {}}
            }
          }
        }
      };
    }
  }
}