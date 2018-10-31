 namespace MGNZ.Squidex.CLI.Tests.Commands
{
  using System.IO;
  using System.Reflection;
  using System.Threading.Tasks;

  using MGNZ.Squidex.Client;
  using MGNZ.Squidex.Client.Handlers;
  using MGNZ.Squidex.CLI.Common.Commands;
  using MGNZ.Squidex.CLI.Common.Configuration;
  using MGNZ.Squidex.CLI.Tests.Platform;
  using MGNZ.Squidex.CLI.Tests.Plumbing;

  using Microsoft.Extensions.Configuration;

  using Xunit;

  [Collection("Sequential Squidex Integration Tests")]
  [Trait("category", "squidex-cli-integration")]
  public class ContentHandlersIntegrationTest
  {
    readonly ApplicationConfiguration _applicationConfiguration = null;

    public ContentHandlersIntegrationTest()
    {
      var configurationRoot = TestHelper.GetConfigurationRoot(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
      configurationRoot.Bind(_applicationConfiguration);
    }

    [Fact(Skip = "under development")]
    //[Fact()]
    public async Task Execute_EndToEnd()
    {
      var authTokenFactory = new OAuthTokenFactory(SerilogFixture.UsefullLogger<OAuthTokenFactory>(), _applicationConfiguration);
      var clientFactory = new ClientProxyFactoryFixture() { ApplicationConfiguration = _applicationConfiguration, OAuthTokenFactory = authTokenFactory } .Build();

      var fileHandler = new FileHandlerMock() { ReadFile = FileHandlerMock.WithReadFileAsNoOp(), WriteFile = FileHandlerMock.WithWriteFileAsNoOp() } .Build();

      var contentImport = new ContentImportHandler(SerilogFixture.UsefullLogger<ContentImportHandler>(), clientFactory, fileHandler, null);
      var contentExport = new ContentExportHandler(SerilogFixture.UsefullLogger<ContentExportHandler>(), clientFactory, fileHandler, null);
      var contentDelete = new ContentDeleteHandler(SerilogFixture.UsefullLogger<ContentDeleteHandler>(), clientFactory, null);

      
    }
  }
}