namespace MGNZ.Squidex.CLI.Tests.Commands
{
  using System.IO;
  using System.Reflection;

  using Bogus;

  using MGNZ.Squidex.Client;
  using MGNZ.Squidex.CLI.Common.Commands;
  using MGNZ.Squidex.CLI.Common.Configuration;
  using MGNZ.Squidex.CLI.Common.Platform;
  using MGNZ.Squidex.CLI.Tests.Platform;
  using MGNZ.Squidex.CLI.Tests.Plumbing;

  using Microsoft.Extensions.Configuration;

  public class BaseHandlerIntegrationTest
  {
    protected ISquidexAppSchemaClient SchemaChecker { get; set; } = null;
    protected SchemaImportHandler SchemaImportHandler { get; set; } = null;
    protected SchemaExportHandler SchemaExportHandler { get; set; } = null;
    protected SchemaDeleteHandler SchemaDeleteHandler { get; set; } = null;
    protected SchemaListHandler SchemaListHandler { get; set; } = null;

    protected ISquidexContentClient ContentChecker { get; } = null;
    protected ContentImportHandler ContentImportHandler { get; } = null;
    protected ContentExportHandler ContentExportHandler { get; } = null;
    protected ContentDeleteHandler ContentDeleteHandler { get; } = null;
    protected ContentPostHandler ContentPostHandler { get; } = null;

    protected ISquidexAttachmentClient AttachmentChecker { get; } = null;
    protected AssetImportHandler AttachmentImportHandler { get; } = null;
    protected AssetExportHandler AttachmentExportHandler { get; } = null;
    protected AssetDeleteHandler AttachmentDeleteHandler { get; } = null;
    protected AssetListHandler AttachmentListHandler { get; } = null;
    protected AssetTagHandler AttachmentTagHandler { get; } = null;


    protected string GetRandomSchemaName =>  new Faker().Random.AlphaNumeric(40).ToLower();
    
    public BaseHandlerIntegrationTest()
    {
      var _applicationConfiguration = new ApplicationConfiguration();
      var configurationRoot = TestHelper.GetConfigurationRoot(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
      configurationRoot.Bind(_applicationConfiguration);

      var consoleWriter = new ConsoleWriter(SerilogFixture.UsefullLogger<ConsoleWriter>());
      var authTokenFactory = new OAuthTokenFactory(SerilogFixture.UsefullLogger<OAuthTokenFactory>(), _applicationConfiguration);
      var clientFactory = new ClientProxyFactory(SerilogFixture.UsefullLogger<ClientProxyFactory>(), authTokenFactory, _applicationConfiguration);
      //var fileHandler = new FileHandlerMock() {ReadFile = FileHandlerMock.WithReadFileAsNoOp(), WriteFile = FileHandlerMock.WithWriteFileAsNoOp()}.Build();
      var fileHandler = new FileHandler(SerilogFixture.UsefullLogger<FileHandler>());

      SchemaChecker = clientFactory.GetClientProxy<ISquidexAppSchemaClient>("aut-developer");
      SchemaImportHandler = new SchemaImportHandler(SerilogFixture.UsefullLogger<SchemaImportHandler>(), clientFactory, consoleWriter, null);
      SchemaExportHandler = new SchemaExportHandler(SerilogFixture.UsefullLogger<SchemaExportHandler>(), clientFactory, consoleWriter, null);
      SchemaDeleteHandler = new SchemaDeleteHandler(SerilogFixture.UsefullLogger<SchemaDeleteHandler>(), clientFactory, consoleWriter, null);
      SchemaListHandler = new SchemaListHandler(SerilogFixture.UsefullLogger<SchemaListHandler>(), clientFactory, consoleWriter, null);
      //var schemaTagHandler = new SchemaTagHandler(SerilogFixture.UsefullLogger<ContentDeleteHandler>(), clientFactory, null);

      ContentChecker = clientFactory.GetClientProxy<ISquidexContentClient>("aut-developer");
      ContentImportHandler = new ContentImportHandler(SerilogFixture.UsefullLogger<ContentImportHandler>(), clientFactory, consoleWriter, null);
      ContentExportHandler = new ContentExportHandler(SerilogFixture.UsefullLogger<ContentExportHandler>(), clientFactory, consoleWriter, null);
      ContentDeleteHandler = new ContentDeleteHandler(SerilogFixture.UsefullLogger<ContentDeleteHandler>(), clientFactory, consoleWriter, null);
      ContentPostHandler = new ContentPostHandler(SerilogFixture.UsefullLogger<ContentImportHandler>(), clientFactory, consoleWriter, null);

      AttachmentChecker = clientFactory.GetClientProxy<ISquidexAttachmentClient>("aut-developer");
      AttachmentImportHandler = new AssetImportHandler(SerilogFixture.UsefullLogger<AssetImportHandler>(), clientFactory, consoleWriter, null);
      AttachmentExportHandler = new AssetExportHandler(SerilogFixture.UsefullLogger<AssetExportHandler>(), clientFactory, consoleWriter, null);
      AttachmentDeleteHandler = new AssetDeleteHandler(SerilogFixture.UsefullLogger<AssetDeleteHandler>(), clientFactory, consoleWriter, null);
      AttachmentListHandler = new AssetListHandler(SerilogFixture.UsefullLogger<AssetListHandler>(), clientFactory, consoleWriter, null);
      AttachmentTagHandler = new AssetTagHandler(SerilogFixture.UsefullLogger<AssetTagHandler>(), clientFactory, consoleWriter, null);
    }
  }
}