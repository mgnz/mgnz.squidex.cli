namespace MGNZ.Squidex.CLI.Tests.Commands
{
  using System.Collections.Generic;

  using MGNZ.Squidex.CLI.Common.Configuration;

  public class ApplicationConfigurationFixture
  {
    public ApplicationConfigurationFixture()
    {
      this.AliasCredentials = new Dictionary<string, Credentials>();
    }

    public Dictionary<string, Credentials> AliasCredentials { get; set; }

    public ApplicationConfiguration Build()
    {
      return new ApplicationConfiguration() { AliasCredentials = this.AliasCredentials };
    }
  }
}