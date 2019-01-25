namespace MGNZ.Squidex.CLI.Tests.Commands.CommandIntegration
{
  using System;
  using System.Linq;
  using System.Threading.Tasks;

  using FluentAssertions;

  using MGNZ.Squidex.Client;
  using MGNZ.Squidex.CLI.Common.Platform;
  using MGNZ.Squidex.Tests.Shared.Code;

  using Xunit;
  using System.IO;

  [Collection("Sequential Squidex Integration Tests")]
  [Trait("category", "squidex-cli-integration")]
  public class AssetCommandIntegrationTest : BaseCommandIntegrationTest
  {
    [Fact]
    public async Task AssetImport_Execute_EndToEnd()
    {
      var name1 = $"{GetRandomSchemaName.Substring(0, 10)}.jpg";

      await AttachmentClient.AssertNoAttachmentsExist("aut", delay: TimeSpan.FromSeconds(0.5));

      await AssetStories.ImportAsset(AttachmentImportSystemUnderTest, "aut", name1, AssetLoader.Asset2Path);
      var item1 = await AttachmentClient.GetAssetByNameOrDefault("aut", name1);

      item1.Should().NotBeNull();
      item1.IsImage.Should().BeTrue();

      await AttachmentClient.DeleteAsset("aut", item1.Id);
      await AttachmentClient.AssertNoAttachmentsExist("aut", delay: TimeSpan.FromSeconds(0.5));
    }

    [Fact]
    public async Task AssetExport_Execute_EndToEnd()
    {
      var schemaName = GetRandomSchemaName;
      var exportPath = Path.Combine(AssetLoader.ExportPath, $"{nameof(SchemaCommandIntegrationTest)} {nameof(AssetExport_Execute_EndToEnd)}-out.json");

      await SchemaClient.AssertNoSchemasExist("aut");
      await SchemaStories.ImportSchema(SchemaImportSystemUnderTest, "aut", schemaName, AssetLoader.Schema1Path);

      await SchemaStories.ExportSchema(SchemaExportSystemUnderTest, "aut", schemaName, exportPath);
      var exportedFileExists = File.Exists(exportPath);
      exportedFileExists.Should().BeTrue($"{nameof(AssetCommandIntegrationTest)} failed to export file");

      // todo validate export file

      await SchemaStories.DeleteSchema(SchemaDeleteSystemUnderTest, "aut", schemaName);
      await SchemaClient.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
    }

    [Fact]
    public async Task AssetDelete_Execute_EndToEnd()
    {
      var name1 = $"{GetRandomSchemaName.Substring(0, 10)}.jpg";

      await AttachmentClient.AssertNoAttachmentsExist("aut", delay: TimeSpan.FromSeconds(0.5));
      var posted1 = await AttachmentClient.CreateAsset("aut", name1,  MimeTypeMap.GetMimeType("jpg"), AssetLoader.Asset2);

      await AssetStories.DeleteAsset(AttachmentDeleteSystemUnderTest, "aut", posted1.Id);

      await AttachmentClient.AssertNoAttachmentsExist("aut", delay: TimeSpan.FromSeconds(0.5));
    }

    [Fact]
    public async Task AssetList_Execute_EndToEnd()
    {
      var name1 = $"{GetRandomSchemaName.Substring(0, 10)}.jpg";
      var name2 = $"{GetRandomSchemaName.Substring(0, 10)}.jpg";

      await AttachmentClient.AssertNoAttachmentsExist("aut", delay: TimeSpan.FromSeconds(0.5));

      var posted1 = await AttachmentClient.CreateAsset("aut", name1,  MimeTypeMap.GetMimeType("jpg"), AssetLoader.Asset2);
      var posted2 =  await AttachmentClient.CreateAsset("aut", name2,  MimeTypeMap.GetMimeType("jpg"), AssetLoader.Asset3);

      await AssetStories.ListAsset(AttachmentListSystemUnderTest, "aut");

      // todo : assert expected output to console service

      await AttachmentClient.DeleteAsset("aut", posted1.Id);
      await AttachmentClient.DeleteAsset("aut", posted2.Id);
      await AttachmentClient.AssertNoAttachmentsExist("aut", delay: TimeSpan.FromSeconds(0.5));
    }

    [Fact]
    public async Task AssetTag_Execute_EndToEnd()
    {
      var name1 = $"{GetRandomSchemaName.Substring(0, 10)}.jpg";

      await AttachmentClient.AssertNoAttachmentsExist("aut", delay: TimeSpan.FromSeconds(0.5));
      var posted1 = await AttachmentClient.CreateAsset("aut", name1,  MimeTypeMap.GetMimeType("jpg"), AssetLoader.Asset2);

      var updatedTags = posted1.Tags.Append("tag-a").Append("tag-b").ToArray();
      await AssetStories.TagAsset(AttachmentTagSystemUnderTest, "aut", posted1.Id, updatedTags);
      var updated1 = await AttachmentClient.GetAssetByNameOrDefault("aut", name1);

      updated1.Should().NotBeNull();
      updated1.Tags.Should().Contain(updatedTags);

      await AttachmentClient.DeleteAsset("aut", posted1.Id);
      await AttachmentClient.AssertNoAttachmentsExist("aut", delay: TimeSpan.FromSeconds(0.5));
    }

    [Fact]
    public async Task AssetUpdate_Execute_EndToEnd()
    {
      var name1 = $"{GetRandomSchemaName.Substring(0, 10)}.jpg";

      await AttachmentClient.AssertNoAttachmentsExist("aut", delay: TimeSpan.FromSeconds(0.5));

      var posted1 = await AttachmentClient.CreateAsset("aut", name1,  MimeTypeMap.GetMimeType("jpg"), AssetLoader.Asset2);

      await AssetStories.UpdateAsset(AssetUpdateContentSystemUnderTest, "aut", posted1.Id, AssetLoader.Asset3Path);
      var updated1 = await AttachmentClient.GetAssetByNameOrDefault("aut", name1);

      updated1.Should().NotBeNull();
      updated1.Id.Should().Be(posted1.Id);
      updated1.Tags.Should().Contain(posted1.Tags);
      updated1.FileName.Should().BeEquivalentTo(posted1.FileName);
      updated1.FileSize.Should().NotBe(posted1.FileSize);

      await AttachmentClient.DeleteAsset("aut", posted1.Id);
      await AttachmentClient.AssertNoAttachmentsExist("aut", delay: TimeSpan.FromSeconds(0.5));
    }
  }
}