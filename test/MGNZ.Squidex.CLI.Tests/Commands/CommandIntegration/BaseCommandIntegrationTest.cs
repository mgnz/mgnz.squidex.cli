namespace MGNZ.Squidex.CLI.Tests.Commands.CommandIntegration
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
  using System.Threading.Tasks;
  using System.Collections.Generic;
  using MGNZ.Squidex.Client.Model;
  using System.Linq;
  using System;

  public class BaseCommandIntegrationTest : IDisposable
  {
    protected ISquidexAppSchemaClient SchemaClient { get; set; } = null;
    protected SchemaImportHandler SchemaImportSystemUnderTest { get; set; } = null;
    protected SchemaExportHandler SchemaExportSystemUnderTest { get; set; } = null;
    protected SchemaDeleteHandler SchemaDeleteSystemUnderTest { get; set; } = null;
    protected SchemaListHandler SchemaListSystemUnderTest { get; set; } = null;

    protected ISquidexContentClient ContentClient { get; } = null;
    protected ContentImportHandler ContentImportSystemUnderTest { get; } = null;
    protected ContentExportHandler ContentExportSystemUnderTest { get; } = null;
    protected ContentDeleteHandler ContentDeleteSystemUnderTest { get; } = null;
    protected ContentPostHandler ContentPostSystemUnderTest { get; } = null;

    protected ISquidexAttachmentClient AttachmentClient { get; } = null;
    protected AssetImportHandler AttachmentImportSystemUnderTest { get; } = null;
    protected AssetExportHandler AttachmentExportSystemUnderTest { get; } = null;
    protected AssetDeleteHandler AttachmentDeleteSystemUnderTest { get; } = null;
    protected AssetListHandler AttachmentListSystemUnderTest { get; } = null;
    protected AssetTagHandler AttachmentTagSystemUnderTest { get; } = null;
    protected AssetUpdateContentHandler AssetUpdateContentSystemUnderTest { get; } = null;


    protected string GetRandomSchemaName =>  new Faker().Random.AlphaNumeric(40).ToLower();
    
    public BaseCommandIntegrationTest()
    {
      var _applicationConfiguration = new ApplicationConfiguration();
      var configurationRoot = TestHelper.GetConfigurationRoot(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
      configurationRoot.Bind(_applicationConfiguration);

      var consoleWriter = new ConsoleWriter(SerilogFixture.UsefullLogger<ConsoleWriter>());
      var authTokenFactory = new OAuthTokenFactory(SerilogFixture.UsefullLogger<OAuthTokenFactory>(), _applicationConfiguration);
      var clientFactory = new ClientProxyFactory(SerilogFixture.UsefullLogger<ClientProxyFactory>(), authTokenFactory, _applicationConfiguration);
      //var fileHandler = new FileHandlerMock() {ReadFile = FileHandlerMock.WithReadFileAsNoOp(), WriteFile = FileHandlerMock.WithWriteFileAsNoOp()}.Build();
      var fileHandler = new FileHandler(SerilogFixture.UsefullLogger<FileHandler>());

      SchemaClient = clientFactory.GetClientProxy<ISquidexAppSchemaClient>("aut-developer");
      SchemaImportSystemUnderTest = new SchemaImportHandler(SerilogFixture.UsefullLogger<SchemaImportHandler>(), clientFactory, consoleWriter, null);
      SchemaExportSystemUnderTest = new SchemaExportHandler(SerilogFixture.UsefullLogger<SchemaExportHandler>(), clientFactory, consoleWriter, null);
      SchemaDeleteSystemUnderTest = new SchemaDeleteHandler(SerilogFixture.UsefullLogger<SchemaDeleteHandler>(), clientFactory, consoleWriter, null);
      SchemaListSystemUnderTest = new SchemaListHandler(SerilogFixture.UsefullLogger<SchemaListHandler>(), clientFactory, consoleWriter, null);
      //var schemaTagHandler = new SchemaTagHandler(SerilogFixture.UsefullLogger<ContentDeleteSystemUnderTest>(), clientFactory, null);

      ContentClient = clientFactory.GetClientProxy<ISquidexContentClient>("aut-developer");
      ContentImportSystemUnderTest = new ContentImportHandler(SerilogFixture.UsefullLogger<ContentImportHandler>(), clientFactory, consoleWriter, null);
      ContentExportSystemUnderTest = new ContentExportHandler(SerilogFixture.UsefullLogger<ContentExportHandler>(), clientFactory, consoleWriter, null);
      ContentDeleteSystemUnderTest = new ContentDeleteHandler(SerilogFixture.UsefullLogger<ContentDeleteHandler>(), clientFactory, consoleWriter, null);
      ContentPostSystemUnderTest = new ContentPostHandler(SerilogFixture.UsefullLogger<ContentImportHandler>(), clientFactory, consoleWriter, null);

      AttachmentClient = clientFactory.GetClientProxy<ISquidexAttachmentClient>("aut-developer");
      AttachmentImportSystemUnderTest = new AssetImportHandler(SerilogFixture.UsefullLogger<AssetImportHandler>(), clientFactory, consoleWriter, null);
      AttachmentExportSystemUnderTest = new AssetExportHandler(SerilogFixture.UsefullLogger<AssetExportHandler>(), clientFactory, consoleWriter, null);
      AttachmentDeleteSystemUnderTest = new AssetDeleteHandler(SerilogFixture.UsefullLogger<AssetDeleteHandler>(), clientFactory, consoleWriter, null);
      AttachmentListSystemUnderTest = new AssetListHandler(SerilogFixture.UsefullLogger<AssetListHandler>(), clientFactory, consoleWriter, null);
      AttachmentTagSystemUnderTest = new AssetTagHandler(SerilogFixture.UsefullLogger<AssetTagHandler>(), clientFactory, consoleWriter, null);
      AssetUpdateContentSystemUnderTest = new AssetUpdateContentHandler(SerilogFixture.UsefullLogger<AssetUpdateContentHandler>(), clientFactory, consoleWriter, null);
    }

    public void Dispose()
    {
      Task.Run(async () => await PurgeSchema()).Wait();
      Task.Run(async () => await PurgeAttachments()).Wait();
    }

    private async Task PurgeSchema()
    {
      var data = await SchemaClient.GetAllSchemas("aut");
      var count = Convert.ToInt32(data.Count);

      if (count == 0) return;

      var allnames = ((IEnumerable<dynamic>) data).Select(d => Convert.ToString(d.name));
      foreach (var name in allnames)
        await SchemaClient.DeleteSchema("aut", name);
    }

    private async Task PurgeAttachments()
    {
      var data = await AttachmentClient.GetAllAssets("aut", new ListRequest()
      {
        Skip = 0, Top = 200
      });
      var count = data.Total;

      if (count == 0) return;

      foreach (var attachment in data.Items)
        await AttachmentClient.DeleteAsset("aut", attachment.Id);
    }
  }
}