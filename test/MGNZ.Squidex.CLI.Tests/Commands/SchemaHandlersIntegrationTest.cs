namespace MGNZ.Squidex.CLI.Tests.Commands
{
  using System;
  using System.IO;
  using System.Reflection;
  using System.Threading;
  using System.Threading.Tasks;

  using FluentAssertions;

  using MediatR;

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
    private readonly SchemaImportHandler _schemaImportHandler = null;
    private readonly SchemaExportHandler _schemaExportHandler = null;
    private readonly SchemaDeleteHandler _schemaDeleteHandler = null;
    private readonly SchemaListHandler _schemaListHandler = null;

    private readonly ISquidexAppSchemaClient _checker = null;

    public SchemaHandlersIntegrationTest()
    {
      var _applicationConfiguration = new ApplicationConfiguration();
      var configurationRoot = TestHelper.GetConfigurationRoot(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
      configurationRoot.Bind(_applicationConfiguration);

      var consoleWriter = new ConsoleWriter(SerilogFixture.UsefullLogger<ConsoleWriter>());
      var authTokenFactory = new OAuthTokenFactory(SerilogFixture.UsefullLogger<OAuthTokenFactory>(), _applicationConfiguration);
      var clientFactory = new ClientProxyFactory(SerilogFixture.UsefullLogger<ClientProxyFactory>(), authTokenFactory, _applicationConfiguration);
      //var fileHandler = new FileHandlerMock() {ReadFile = FileHandlerMock.WithReadFileAsNoOp(), WriteFile = FileHandlerMock.WithWriteFileAsNoOp()}.Build();
      var fileHandler = new FileHandler(SerilogFixture.UsefullLogger<FileHandler>());

      _schemaImportHandler = new SchemaImportHandler(SerilogFixture.UsefullLogger<SchemaImportHandler>(), clientFactory, consoleWriter, null);
      _schemaExportHandler = new SchemaExportHandler(SerilogFixture.UsefullLogger<SchemaExportHandler>(), clientFactory, consoleWriter, null);
      _schemaDeleteHandler = new SchemaDeleteHandler(SerilogFixture.UsefullLogger<SchemaDeleteHandler>(), clientFactory, consoleWriter, null);
      _schemaListHandler = new SchemaListHandler(SerilogFixture.UsefullLogger<SchemaListHandler>(), clientFactory, consoleWriter, null);
      //var schemaTagHandler = new SchemaTagHandler(SerilogFixture.UsefullLogger<ContentDeleteHandler>(), clientFactory, null);

      _checker = clientFactory.GetClientProxy<ISquidexAppSchemaClient>("aut-developer");
    }

    [Fact]
    public async Task SchemaImport_Execute_EndToEnd()
    {
      await _checker.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));

      await ImportSchemaStory(_schemaImportHandler);
      await _checker.AssertSchemaMustExist("aut", "test-schema-1", delay: TimeSpan.FromSeconds(0.5));

      await DeleteSchemaStory(_schemaDeleteHandler);
      await _checker.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
    }

    [Fact]
    public async Task SchemaExport_Execute_EndToEnd()
    {
      await _checker.AssertNoSchemasExist("aut");
      await ImportSchemaStory(_schemaImportHandler);

      await ExportSchemaStory(_schemaExportHandler);
      var exportedFileExists = File.Exists(Path.Combine(AssetLoader.ExportPath, $"{nameof(SchemaHandlersIntegrationTest)} {nameof(SchemaExport_Execute_EndToEnd)}-out.json"));
      exportedFileExists.Should().BeTrue($"{nameof(SchemaExportRequest)} failed to export file");

      // todo validate export file

      await DeleteSchemaStory(_schemaDeleteHandler);
      await _checker.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
    }

    [Fact]
    public async Task SchemaDelete_Execute_EndToEnd()
    {
      await _checker.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
      await ImportSchemaStory(_schemaImportHandler);

      await DeleteSchemaStory(_schemaDeleteHandler);
      await _checker.AssertSchemaMustNotExist("aut", "test-schema-1", delay: TimeSpan.FromSeconds(5));

      await _checker.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
    }

    [Fact(Skip = "tag not implemented")]
    public async Task SchemaListt_Execute_EndToEnd()
    {
    }

    [Fact(Skip = "tag not implemented")]
    public async Task SchemaTag_Execute_EndToEnd()
    {
    }

    private async Task<Unit> DeleteSchemaStory(SchemaDeleteHandler schemaDeleteHandler)
    {
      return await schemaDeleteHandler.Handle(new SchemaDeleteRequest()
      {
        AliasCredentials = "aut-developer",
        Application = "aut",
        Name = "test-schema-1"
      }, CancellationToken.None);
    }

    private async Task<Unit> ExportSchemaStory(SchemaExportHandler schemaExportHandler)
    {
      return await schemaExportHandler.Handle(new SchemaExportRequest()
      {
        AliasCredentials = "aut-developer",
        Application = "aut",
        Name = "test-schema-1",
        Path = Path.Combine(AssetLoader.ExportPath, $"{nameof(SchemaHandlersIntegrationTest)}-out.json")
      }, CancellationToken.None);
    }

    private async Task<Unit> ImportSchemaStory(SchemaImportHandler schemaImportHandler)
    {
      return await schemaImportHandler.Handle(new SchemaImportRequest()
      {
        AliasCredentials = "aut-developer",
        Application = "aut",
        Name = "test-schema-1",
        Path = AssetLoader.Schema1Path
      }, CancellationToken.None);
    }
  }
}