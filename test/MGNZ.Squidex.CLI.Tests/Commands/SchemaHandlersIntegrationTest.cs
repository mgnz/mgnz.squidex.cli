using System.Threading;

namespace MGNZ.Squidex.CLI.Tests.Commands
{
  using System.IO;
  using System.Reflection;
  using System.Threading.Tasks;

  using MGNZ.Squidex.CLI.Common.Commands;
  using MGNZ.Squidex.CLI.Common.Configuration;
  using MGNZ.Squidex.CLI.Common.Platform;
  using MGNZ.Squidex.CLI.Tests.Platform;
  using MGNZ.Squidex.CLI.Tests.Plumbing;

  using Microsoft.Extensions.Configuration;

  using Xunit;

  [Collection("Sequential Squidex Integration Tests")]
  [Trait("category", "squidex-cli-integration")]
  public class SchemaHandlersIntegrationTest
  {
    readonly ApplicationConfiguration _applicationConfiguration = null;

    public SchemaHandlersIntegrationTest()
    {
      _applicationConfiguration = new ApplicationConfiguration();
      var configurationRoot = TestHelper.GetConfigurationRoot(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
      configurationRoot.Bind(_applicationConfiguration);
    }

    //[Fact(Skip = "under development")]
    [Fact()]
    public async Task Execute_EndToEnd()
    {
      var authTokenFactory = new OAuthTokenFactory(SerilogFixture.UsefullLogger<OAuthTokenFactory>(), _applicationConfiguration);
      var clientFactory = new ClientProxyFactory(SerilogFixture.UsefullLogger<ClientProxyFactory>(), authTokenFactory, _applicationConfiguration);
      //var fileHandler = new FileHandlerMock() {ReadFile = FileHandlerMock.WithReadFileAsNoOp(), WriteFile = FileHandlerMock.WithWriteFileAsNoOp()}.Build();
      var fileHandler = new FileHandler(SerilogFixture.UsefullLogger<FileHandler>());

      var import = new SchemaImportHandler(SerilogFixture.UsefullLogger<SchemaImportHandler>(), clientFactory, fileHandler, null);
      //var contentExport = new ContentExportHandler(SerilogFixture.UsefullLogger<ContentExportHandler>(), clientFactory, fileHandler, null);
      //var contentDelete = new ContentDeleteHandler(SerilogFixture.UsefullLogger<ContentDeleteHandler>(), clientFactory, null);

      var result = await import.Handle(new SchemaImportRequest()
      {
        AliasCredentials = "aut-developer",
        Application = "aut",
        Name = "test-schema-1",
        Path = AssetLoader.Schema1Path
      }, CancellationToken.None);
    }
  }
}