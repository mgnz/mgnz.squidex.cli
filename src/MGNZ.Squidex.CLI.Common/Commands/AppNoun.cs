namespace MGNZ.Squidex.CLI.Common.Commands
{
  using System.Collections.Generic;

  using MGNZ.Squidex.CLI.Common.CLI;

  public class AppNoun : Noun
  {
    public AppNoun()
    {
      Names = new[ ] {"app", "apps"};
      Verbs = new Dictionary<string, Verb>
      {
        {
          "delete-app", new Verb
          {
            Names = new[ ] {"delete"},
            Options = new[ ]
            {
              new Option {ShortName = "n", LongName = "name", Required = true, OrdanalityOrder = 1},
              new Option {ShortName = "t", LongName = "token", Required = true, OrdanalityOrder = 2}
            }
          }
        },

        {
          "list-app", new Verb
          {
            Names = new[ ] {"list"},
            Options = new[ ]
            {
              new Option {ShortName = "t", LongName = "token", Required = true, OrdanalityOrder = 1}
            }
          }
        },
        {
          "login-app", new Verb
          {
            Names = new[ ] {"login"},
            Options = new[ ]
            {
              new Option {ShortName = "u", LongName = "uri", Required = true, OrdanalityOrder = 1},
              new Option {ShortName = "n", LongName = "app-name", Required = true, OrdanalityOrder = 2},
              new Option {ShortName = "cid", LongName = "client-id", Required = false},
              new Option {ShortName = "cs", LongName = "client-secret", Required = false},
              new Option {ShortName = "t", LongName = "token", Required = false},
              new Option {ShortName = "c", LongName = "alias-credentials", Required = false},
              new Option {ShortName = "a", LongName = "alias-credentials-as", Required = false}
            }
          }
        },
        {
          "logout-app", new Verb
          {
            Names = new[ ] {"logout"},
            Options = new[ ]
            {
              new Option {ShortName = "n", LongName = "name", Required = true, OrdanalityOrder = 1},
              new Option {ShortName = "c", LongName = "alias-credentials", Required = false}
            }
          }
        },
        {
          "new-app", new Verb
          {
            Names = new[ ] {"new"},
            Options = new[ ]
            {
              new Option {ShortName = "n", LongName = "name", Required = true, OrdanalityOrder = 1},
              new Option {ShortName = "t", LongName = "token", Required = true, OrdanalityOrder = 2}
            }
          }
        }
      };
    }
  }
}