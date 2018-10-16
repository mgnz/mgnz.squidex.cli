namespace MGNZ.Squidex.CLI.Tests.Commands
{
  using MGNZ.Squidex.CLI.Common.Commands;
  using MGNZ.Squidex.CLI.Common.Configuration;
  using MGNZ.Squidex.CLI.Tests.Platform;

  public class ClientProxyFactoryFixture
  {
    public ClientProxyFactoryFixture()
    {
      this.OAuthTokenFactory = new OAuthTokenFactoryFixture().Build();
      this.ApplicationConfiguration = new ApplicationConfigurationFixture().Build();
    }

    public IOAuthTokenFactory OAuthTokenFactory { get; set; }
    public ApplicationConfiguration ApplicationConfiguration { get; set; }

    public IClientProxyFactory Build()
    {
      return new ClientProxyFactory(SerilogFixture.UsefullLogger<ClientProxyFactory>(), this.OAuthTokenFactory, this.ApplicationConfiguration);
    }
  }
}