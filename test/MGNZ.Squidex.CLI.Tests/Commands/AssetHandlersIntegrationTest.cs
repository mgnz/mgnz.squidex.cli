namespace MGNZ.Squidex.CLI.Tests.Commands
{
  using System;
  using System.Linq;
  using System.Threading.Tasks;

  using FluentAssertions;

  using MGNZ.Squidex.Client;
  using MGNZ.Squidex.CLI.Common.Platform;
  using MGNZ.Squidex.Tests.Shared.Code;

  using Xunit;

  [Collection("Sequential Squidex Integration Tests")]
  [Trait("category", "squidex-cli-integration")]
  public class AssetHandlersIntegrationTest : BaseHandlerIntegrationTest
  {
    [Fact]
    public async Task AssetImport_Execute_EndToEnd()
    {
      var name1 = $"{GetRandomSchemaName.Substring(0, 10)}.jpg";

      await AttachmentChecker.AssertNoAttachmentsExist("aut", delay: TimeSpan.FromSeconds(0.5));

      await AssetStories.ImportAsset(AttachmentImportHandler, "aut", name1, AssetLoader.Asset2Path);
      var item1 = await AttachmentChecker.GetByNameOrDefault_NEW("aut", name1);

      item1.Should().NotBeNull();
      item1.IsImage.Should().BeTrue();

      await AssetStories.DeleteAsset(AttachmentDeleteHandler, "aut", item1.Id);
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
      var name1 = $"{GetRandomSchemaName.Substring(0, 10)}.jpg";

      await AttachmentChecker.AssertNoAttachmentsExist("aut", delay: TimeSpan.FromSeconds(0.5));

      var posted1 = await AttachmentChecker.Post("aut", name1,  MimeTypeMap.GetMimeType("jpg"), AssetLoader.Asset2);

      await AssetStories.DeleteAsset(AttachmentDeleteHandler, "aut", posted1.Id);
      await AttachmentChecker.AssertNoAttachmentsExist("aut", delay: TimeSpan.FromSeconds(0.5));
    }

    [Fact]
    public async Task AssetList_Execute_EndToEnd()
    {
      var name1 = $"{GetRandomSchemaName.Substring(0, 10)}.jpg";
      var name2 = $"{GetRandomSchemaName.Substring(0, 10)}.jpg";

      await AttachmentChecker.AssertNoAttachmentsExist("aut", delay: TimeSpan.FromSeconds(0.5));

      var posted1 = await AttachmentChecker.Post("aut", name1,  MimeTypeMap.GetMimeType("jpg"), AssetLoader.Asset2);
      var posted2 =  await AttachmentChecker.Post("aut", name2,  MimeTypeMap.GetMimeType("jpg"), AssetLoader.Asset3);

      await AssetStories.ListAsset(AttachmentListHandler, "aut");

      // todo : assert expected output to console service

      await AssetStories.DeleteAsset(AttachmentDeleteHandler, "aut", posted1.Id);
      await AssetStories.DeleteAsset(AttachmentDeleteHandler, "aut", posted2.Id);
      await AttachmentChecker.AssertNoAttachmentsExist("aut", delay: TimeSpan.FromSeconds(0.5));
    }

    [Fact]
    public async Task AssetTag_Execute_EndToEnd()
    {
      var name1 = $"{GetRandomSchemaName.Substring(0, 10)}.jpg";

      await AttachmentChecker.AssertNoAttachmentsExist("aut", delay: TimeSpan.FromSeconds(0.5));

      var posted1 = await AttachmentChecker.Post("aut", name1,  MimeTypeMap.GetMimeType("jpg"), AssetLoader.Asset2);

      var updatedTags = posted1.Tags.Append("tag-a").Append("tag-b").ToArray();
      await AssetStories.TagAsset(AttachmentTagHandler, "aut", posted1.Id, updatedTags);
      var updated1 = await AttachmentChecker.GetByNameOrDefault_NEW("aut", name1);

      updated1.Should().NotBeNull();
      updated1.Tags.Should().Contain(updatedTags);

      await AssetStories.DeleteAsset(AttachmentDeleteHandler, "aut", posted1.Id);
      await AttachmentChecker.AssertNoAttachmentsExist("aut", delay: TimeSpan.FromSeconds(0.5));
    }

    [Fact]
    public async Task AssetUpdate_Execute_EndToEnd()
    {
      var name1 = $"{GetRandomSchemaName.Substring(0, 10)}.jpg";

      await AttachmentChecker.AssertNoAttachmentsExist("aut", delay: TimeSpan.FromSeconds(0.5));

      var posted1 = await AttachmentChecker.Post("aut", name1,  MimeTypeMap.GetMimeType("jpg"), AssetLoader.Asset2);

      await AssetStories.UpdateAsset(AssetUpdateContentHandler, "aut", posted1.Id, AssetLoader.Asset3Path);
      var updated1 = await AttachmentChecker.GetByNameOrDefault_NEW("aut", name1);

      updated1.Should().NotBeNull();
      updated1.Id.Should().Be(posted1.Id);
      updated1.Tags.Should().Contain(posted1.Tags);
      updated1.FileName.Should().BeEquivalentTo(posted1.FileName);
      updated1.FileSize.Should().NotBe(posted1.FileSize);

      await AssetStories.DeleteAsset(AttachmentDeleteHandler, "aut", posted1.Id);
      await AttachmentChecker.AssertNoAttachmentsExist("aut", delay: TimeSpan.FromSeconds(0.5));
    }
  }
}