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
          "delete", new Verb(this)
          {
            Names = new[ ] {"delete"},
            RequestType = typeof(AppDeleteRequest),
            Options = new Dictionary<string, Option>()
            {
              { "name", new Option {ShortName = "n", LongName = "name", Required = true, OrdanalityOrder = 1} },
              { "token", new Option {ShortName = "t", LongName = "token", Required = true, OrdanalityOrder = 2} }
            }
          }
        },

        {
          "list", new Verb(this)
          {
            Names = new[ ] {"list"},
            RequestType = typeof(AppListRequest),
            Options = new Dictionary<string, Option>()
            {
              { "token", new Option {ShortName = "t", LongName = "token", Required = true, OrdanalityOrder = 1} }
            }
          }
        },
        {
          "login", new Verb(this)
          {
            Names = new[ ] {"login"},
            RequestType = typeof(AppLoginRequest),
            Options = new Dictionary<string, Option>()
            {
              { "url", new Option {ShortName = "u", LongName = "uri", Required = true, OrdanalityOrder = 1} },
              { "name", new Option {ShortName = "n", LongName = "name", Required = true, OrdanalityOrder = 2} },
              { "client-id", new Option {ShortName = "cid", LongName = "client-id", Required = false} },
              { "client-secret", new Option {ShortName = "cs", LongName = "client-secret", Required = false} },
              { "token", new Option {ShortName = "t", LongName = "token", Required = false} },
              { "alias-credentials", new Option {ShortName = "c", LongName = "alias-credentials", Required = false} },
              { "alias-credentials-as", new Option {ShortName = "a", LongName = "alias-credentials-as", Required = false} }
            }
          }
        },
        {
          "logout", new Verb(this)
          {
            Names = new[ ] {"logout"},
            RequestType = typeof(AppLogoutRequest),
            Options = new Dictionary<string, Option>()
            {
              { "name", new Option {ShortName = "n", LongName = "name", Required = true, OrdanalityOrder = 1} },
              { "alias-credentials", new Option {ShortName = "c", LongName = "alias-credentials", Required = false} }
            }
          }
        },
        {
          "new", new Verb(this)
          {
            Names = new[ ] {"new"},

            RequestType = typeof(AppNewRequest),
            Options = new Dictionary<string, Option>()
            {
              { "name", new Option {ShortName = "n", LongName = "name", Required = true, OrdanalityOrder = 1} },
              { "token", new Option {ShortName = "t", LongName = "token", Required = true, OrdanalityOrder = 2} }
            }
          }
        }
      };
    }
  }
}