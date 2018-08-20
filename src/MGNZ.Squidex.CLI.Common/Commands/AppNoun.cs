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
              new Option {ShortName = "n", LongName = "name", Required = true, Ordinal = true},
              new Option {ShortName = "t", LongName = "token", Required = true, Ordinal = true}
            }
          }
        },

        {
          "list-app", new Verb
          {
            Names = new[ ] {"list"},
            Options = new[ ]
            {
              new Option {ShortName = "t", LongName = "token", Required = true, Ordinal = true}
            }
          }
        },
        {
          "login-app", new Verb
          {
            Names = new[ ] {"login"},
            Options = new[ ]
            {
              new Option {ShortName = "u", LongName = "uri", Required = true, Ordinal = true},
              new Option {ShortName = "n", LongName = "app-name", Required = true, Ordinal = true},
              new Option {ShortName = "cid", LongName = "client-id", Required = false, Ordinal = false},
              new Option {ShortName = "cs", LongName = "client-secret", Required = false, Ordinal = false},
              new Option {ShortName = "t", LongName = "token", Required = false, Ordinal = false},
              new Option {ShortName = "c", LongName = "alias-credentials", Required = false, Ordinal = false},
              new Option {ShortName = "a", LongName = "alias-credentials-as", Required = false, Ordinal = false}
            }
          }
        },
        {
          "logout-app", new Verb
          {
            Names = new[ ] {"logout"},
            Options = new[ ]
            {
              new Option {ShortName = "n", LongName = "name", Required = true, Ordinal = true},
              new Option {ShortName = "c", LongName = "alias-credentials", Required = false, Ordinal = false}
            }
          }
        },
        {
          "new-app", new Verb
          {
            Names = new[ ] {"new"},
            Options = new[ ]
            {
              new Option {ShortName = "n", LongName = "name", Required = true, Ordinal = true},
              new Option {ShortName = "t", LongName = "token", Required = true, Ordinal = true}
            }
          }
        }
      };
    }
  }
}