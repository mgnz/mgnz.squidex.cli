
namespace MGNZ.Squidex.CLI.Tests.Commands
{
  using System;
  using System.IO;
  using System.Threading.Tasks;

  using FluentAssertions;

  using MGNZ.Squidex.Client.Transport;
  using MGNZ.Squidex.CLI.Common.Commands;
  using MGNZ.Squidex.CLI.Tests.Assets;
  using MGNZ.Squidex.CLI.Tests.Platform;

  using Newtonsoft.Json;

  using Xunit;

  [Collection("Sequential Squidex Integration Tests")]
  [Trait("category", "squidex-cli-integration")]
  public class ContentHandlersIntegrationTest : BaseHandlerIntegrationTest
  {
    [Fact(Skip = "used to setup data in assets only")]
    public async Task get_asset_data()
    {
      var schemaName = GetRandomSchemaName;
      await SchemaChecker.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
      await SchemaStories.ImportSchema(SchemaImportHandler, "aut", schemaName, AssetLoader.Schema1Path);

      var post1response = await ContentChecker.Create<dynamic>("aut", schemaName, AssetLoader.Schema1Data1Post.Value);
      var post2response = await ContentChecker.Create<dynamic>("aut", schemaName, AssetLoader.Schema1Data2Post.Value);

      var queryresponse = await ContentChecker.Query<dynamic>("aut", schemaName, new QueryRequest()
      {
        Skip = 0,
        Top = 100
      });

      var post1responsestring = JsonConvert.SerializeObject(post1response, Formatting.Indented); 
      var post2responsestring = JsonConvert.SerializeObject(post2response, Formatting.Indented);
      var queryresponsestring = JsonConvert.SerializeObject(queryresponse, Formatting.Indented);

      await SchemaStories.DeleteSchema(SchemaDeleteHandler, "aut", schemaName);
    }

    [Fact]
    public async Task ContentImport_Execute_EndToEnd()
    {
      var schemaName = GetRandomSchemaName;
      await SchemaChecker.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
      await SchemaStories.ImportSchema(SchemaImportHandler, "aut", schemaName, AssetLoader.Schema1Path);

      var expectedFirst = AssetLoader.Schema1DataQueryResponse.Value.Items[0];
      var expectedSecond = AssetLoader.Schema1DataQueryResponse.Value.Items[1];

      await ContentStories.ImportContent(ContentImportHandler, "aut", schemaName, AssetLoader.Schema1DataImportPath, publish: true);

      var content = await ContentChecker.Query<dynamic>("aut", schemaName, new QueryRequest()
      {
        Skip = 0,
        Top = 100
      });

      var testCount = content.Total;
      testCount.Should().Be(2);

      var actualFirst = content.Items[0];
      var actualSecond = content.Items[1];

      // todo : verify

      await SchemaStories.DeleteSchema(SchemaDeleteHandler, "aut", schemaName);
    }

    [Fact]
    public async Task ContentExport_Execute_EndToEnd()
    {
      var schemaName = GetRandomSchemaName;
      await SchemaChecker.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
      await SchemaStories.ImportSchema(SchemaImportHandler, "aut", schemaName, AssetLoader.Schema1Path);
      await ContentStories.ImportContent(ContentImportHandler, "aut", schemaName, AssetLoader.Schema1DataImportPath, publish: true);

      var expectedFirst = AssetLoader.Schema1DataExportResponse.Value.Items[0];
      var expectedSecond = AssetLoader.Schema1DataExportResponse.Value.Items[1];

      var exportPath = Path.Combine(AssetLoader.ExportPath, $"{nameof(ContentHandlersIntegrationTest)} {nameof(ContentExport_Execute_EndToEnd)}-out.json");

      // act

      await ContentStories.ExportContent(ContentExportHandler, "aut", schemaName, exportPath, top: "10", skip: "0");
      var exportedFileExists = File.Exists(exportPath);
      exportedFileExists.Should().BeTrue($"{nameof(SchemaExportRequest)} failed to export file");

      // todo : verify export content

      await SchemaStories.DeleteSchema(SchemaDeleteHandler, "aut", schemaName);
    }

    [Fact]
    public async Task ContentDelete_Execute_EndToEnd()
    {
      var schemaName = GetRandomSchemaName;
      await SchemaChecker.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
      await SchemaStories.ImportSchema(SchemaImportHandler, "aut", schemaName, AssetLoader.Schema1Path);
      await ContentStories.ImportContent(ContentImportHandler, "aut", schemaName, AssetLoader.Schema1DataImportPath, publish: true);

      var content = await ContentChecker.Query<dynamic>("aut", schemaName, new QueryRequest()
      {
        Skip = 0,
        Top = 100
      });

      content.Total.Should().Be(2);
      var actualFirst = content.Items[0];
      var actualSecond = content.Items[1];

      // act

      await ContentStories.DeleteContent(ContentDeleteHandler, "aut", schemaName, actualFirst.Id);

      // todo : verify export content

      await ContentChecker.AssertContentMustNotExists("aut", schemaName, actualFirst.Id);
      await ContentChecker.AssertContentMustExists("aut", schemaName, actualSecond.Id);

      // clean up

      await SchemaStories.DeleteSchema(SchemaDeleteHandler, "aut", schemaName);
    }

    [Fact]
    public async Task ContentPost_Execute_EndToEnd()
    {

    }
  }
}