 namespace MGNZ.Squidex.CLI.Tests.Commands
{
  using System.Net.Http;
  using System.Threading.Tasks;

  using MGNZ.Squidex.Client;
  using MGNZ.Squidex.Client.Handlers;
  using MGNZ.Squidex.CLI.Common.Commands;
  using MGNZ.Squidex.CLI.Common.Routing;
  using MGNZ.Squidex.CLI.Tests.Platform;

  using Moq;

  using Refit;

  using Xunit;

  [Collection("Sequential Squidex Integration Tests")]
  [Trait("category", "squidex-cli-integration")]
  public class ContentHandlersIntegrationTest
  {
    //protected ISquidexContentClient _authenticatedContentClient = null;
    //public SquidexCliIntegrationTest()
    //{
    //  _authenticatedContentClient = RestService.For<ISquidexContentClient>(new HttpClient(new SimpleAccessTokenHttpClientHandler(() => )));
    //}

    [Fact]
    public async Task Execute_EndToEnd()
    {
      //var clientFactory = new ClientProxyFactoryFixture().Build();

      //var contentImport = new ContentImportHandler(SerilogFixture.UsefullLogger<ContentImportHandler>(), clientFactory, null);
      //var contentExport = new ContentExportHandler(SerilogFixture.UsefullLogger<ContentExportHandler>(), clientFactory, null);
      //var contentDelete = new ContentDeleteHandler(SerilogFixture.UsefullLogger<ContentDeleteHandler>(), clientFactory, null);
    }
  }
}