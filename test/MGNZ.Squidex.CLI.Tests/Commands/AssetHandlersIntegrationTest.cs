namespace MGNZ.Squidex.CLI.Tests.Commands
{
  using System.Net.Http;

  using MGNZ.Squidex.Client;
  using MGNZ.Squidex.Client.Handlers;
  using MGNZ.Squidex.CLI.Common.Routing;

  using Refit;

  using Xunit;

  [Collection("Sequential Squidex Integration Tests")]
  [Trait("category", "squidex-cli-integration")]
  public class AssetHandlersIntegrationTest
  {
    //protected ISquidexContentClient _authenticatedContentClient = null;
    //public SquidexCliIntegrationTest()
    //{
    //  _authenticatedContentClient = RestService.For<ISquidexContentClient>(new HttpClient(new SimpleAccessTokenHttpClientHandler(() => )));
    //}

    //[Fact]
    //public async Task Execute_EndToEnd()
    //{
    //  var clientFactory = new ClientProxyFactoryMock() { WithGetClientProxy = ClientProxyFactoryMock.WithGetClientProxyAsNoOp<AssetImportHandler>(), AndGetClientProxyReturns = } .Build();
    //  var import = new AssetImportHandler(SerilogFixture.UsefullLogger<ClientProxyFactory>(), clientFactory, null);
    //}
  }
}