namespace MGNZ.Squidex.CLI.Tests.Commands
{
  using System;
  using System.IO;
  using System.Threading.Tasks;

  using FluentAssertions;

  using MGNZ.Squidex.Client;
  using MGNZ.Squidex.CLI.Common.Commands;

  using MGNZ.Squidex.Tests.Shared.Code;

  using Xunit;

  [Collection("Sequential Squidex Integration Tests")]
  //[Trait("category", "squidex-cli-integration")]
  [Trait("category", "inprogress")]
  public class AssetHandlersIntegrationTest : BaseHandlerIntegrationTest
  {
    [Fact]
    public async Task AssetImport_Execute_EndToEnd()
    {
      var schemaName = $"{GetRandomSchemaName.Substring(0, 10)}.jpg";

      await AttachmentChecker.AssertNoAttachmentsExist("aut", delay: TimeSpan.FromSeconds(0.5));

      await AssetStories.ImportAsset(AttachmentImportHandler, "aut", schemaName, MGNZ.Squidex.Tests.Shared.Code.AssetLoader.Asset2Path);
      var dto = await AttachmentChecker.GetByNameOrDefault_NEW("aut", schemaName);

      dto.Should().NotBeNull();
      dto.IsImage.Should().BeTrue();

      await AssetStories.DeleteAsset(AttachmentDeleteHandler, "aut", dto.Id);
      await AttachmentChecker.AssertNoAttachmentsExist("aut", delay: TimeSpan.FromSeconds(0.5));
    }

    [Fact]
    public async Task AssetExport_Execute_EndToEnd()
    {
      //var schemaName = GetRandomSchemaName;
      //var exportPath = Path.Combine(AssetLoader.ExportPath, $"{nameof(SchemaHandlersIntegrationTest)} {nameof(AssetExport_Execute_EndToEnd)}-out.json");

      //await SchemaChecker.AssertNoSchemasExist("aut");
      //await SchemaStories.ImportSchema(SchemaImportHandler, "aut", schemaName, AssetLoader.Schema1Path);

      //await SchemaStories.ExportSchema(SchemaExportHandler, "aut", schemaName, exportPath);
      //var exportedFileExists = File.Exists(exportPath);
      //exportedFileExists.Should().BeTrue($"{nameof(SchemaExportRequest)} failed to export file");

      //// todo validate export file

      //await SchemaStories.DeleteSchema(SchemaDeleteHandler, "aut", schemaName);
      //await SchemaChecker.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
    }

    [Fact]
    public async Task AssetDelete_Execute_EndToEnd()
    {
      //var schemaName = GetRandomSchemaName;

      //await SchemaChecker.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
      //await SchemaStories.ImportSchema(SchemaImportHandler, "aut", schemaName, AssetLoader.Schema1Path);

      //await SchemaStories.DeleteSchema(SchemaDeleteHandler, "aut", schemaName);
      //await SchemaChecker.AssertSchemaMustNotExist("aut", schemaName, delay: TimeSpan.FromSeconds(5));

      //await SchemaChecker.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
    }

    [Fact(Skip = "tag not implemented")]
    public async Task AssetList_Execute_EndToEnd()
    {
    }

    [Fact(Skip = "tag not implemented")]
    public async Task AssetTag_Execute_EndToEnd()
    {
    }
  }
}