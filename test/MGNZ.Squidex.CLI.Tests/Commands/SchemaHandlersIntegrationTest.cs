using System.Threading;

namespace MGNZ.Squidex.CLI.Tests.Commands
{
  using System.IO;
  using System.Reflection;
  using System.Threading.Tasks;

  using FluentAssertions;

  using MGNZ.Squidex.Client;
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

    [Fact()]
    public async Task Execute_EndToEnd()
    {
      var consoleWriter = new ConsoleWriter(SerilogFixture.UsefullLogger<ConsoleWriter>());
      var authTokenFactory = new OAuthTokenFactory(SerilogFixture.UsefullLogger<OAuthTokenFactory>(), _applicationConfiguration);
      var clientFactory = new ClientProxyFactory(SerilogFixture.UsefullLogger<ClientProxyFactory>(), authTokenFactory, _applicationConfiguration);
      //var fileHandler = new FileHandlerMock() {ReadFile = FileHandlerMock.WithReadFileAsNoOp(), WriteFile = FileHandlerMock.WithWriteFileAsNoOp()}.Build();
      var fileHandler = new FileHandler(SerilogFixture.UsefullLogger<FileHandler>());

      var schemaImportHandler = new SchemaImportHandler(SerilogFixture.UsefullLogger<SchemaImportHandler>(), clientFactory, consoleWriter, null);
      var schemaExportHandler = new SchemaExportHandler(SerilogFixture.UsefullLogger<SchemaExportHandler>(), clientFactory, consoleWriter, null);
      var schemaDeleteHandler = new SchemaDeleteHandler(SerilogFixture.UsefullLogger<SchemaDeleteHandler>(), clientFactory, consoleWriter, null);
      //var schemaListHandler = new SchemaDeleteHandler(SerilogFixture.UsefullLogger<ContentDeleteHandler>(), clientFactory, null);
      //var schemaTagHandler = new SchemaTagHandler(SerilogFixture.UsefullLogger<ContentDeleteHandler>(), clientFactory, null);

      var checker = clientFactory.GetClientProxy<ISquidexAppSchemaClient>("aut-developer");

      var schemaImportResult = await schemaImportHandler.Handle(new SchemaImportRequest()
      {
        AliasCredentials = "aut-developer",
        Application = "aut",
        Name = "test-schema-1",
        Path = AssetLoader.Schema1Path
      }, CancellationToken.None);

      var importedSchemaExists = await checker.SchemaExists("aut", "test-schema-1");
      importedSchemaExists.Should().BeTrue($"{nameof(SchemaImportRequest)} failed to import file");

      var schemaExportResult = await schemaExportHandler.Handle(new SchemaExportRequest()
      {
        AliasCredentials = "aut-developer",
        Application = "aut",
        Name = "test-schema-1",
        Path = Path.Combine(AssetLoader.ExportPath, $"{nameof(SchemaHandlersIntegrationTest)} {nameof(Execute_EndToEnd)} out.json")
      }, CancellationToken.None);

      var exportedFileExists = File.Exists(Path.Combine(AssetLoader.ExportPath, $"{nameof(SchemaHandlersIntegrationTest)} {nameof(Execute_EndToEnd)} out.json"));
      exportedFileExists.Should().BeTrue($"{nameof(SchemaExportRequest)} failed to export file");

      var existingSchemaExists = await checker.SchemaExists("aut", "test-schema-1");
      existingSchemaExists.Should().BeTrue($"{nameof(SchemaDeleteRequest)} dependent schema does not exist");

      var schemaDeleteResult = await schemaDeleteHandler.Handle(new SchemaDeleteRequest()
      {
        AliasCredentials = "aut-developer",
        Application = "aut",
        Name = "test-schema-1"
      }, CancellationToken.None);

      var schemaIsDeleted = await checker.SchemaExists("aut", "test-schema-1");
      schemaIsDeleted.Should().BeFalse($"{nameof(SchemaDeleteRequest)} dependent schema must not exist");
    }
  }
}