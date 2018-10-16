namespace MGNZ.Squidex.CLI.Tests.Commands
{
  using MGNZ.Squidex.CLI.Common.Commands;
  using MGNZ.Squidex.CLI.Common.Configuration;
  using MGNZ.Squidex.CLI.Tests.Platform;

  public class OAuthTokenFactoryFixture
  {
    public OAuthTokenFactoryFixture()
    {
      this.ApplicationConfiguration = new ApplicationConfigurationFixture().Build();
    }

    public ApplicationConfiguration ApplicationConfiguration { get; set; }

    public IOAuthTokenFactory Build()
    {
      return new OAuthTokenFactory(SerilogFixture.UsefullLogger<OAuthTokenFactory>(), this.ApplicationConfiguration);
    }
  }
}