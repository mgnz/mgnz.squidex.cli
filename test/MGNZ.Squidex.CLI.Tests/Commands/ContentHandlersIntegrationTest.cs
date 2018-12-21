
namespace MGNZ.Squidex.CLI.Tests.Commands
{
  using System;
  using System.IO;
  using System.Threading.Tasks;

  using FluentAssertions;

  using MGNZ.Squidex.Client.Tests.Shared.Code;
  using MGNZ.Squidex.Client.Transport;
  using MGNZ.Squidex.CLI.Common.Commands;
  using MGNZ.Squidex.Tests.Shared.Code;

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
      await SchemaClient.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
      await SchemaStories.ImportSchema(SchemaImportSystemUnderTest, "aut", schemaName, AssetLoader.Schema1Path);

      var post1response = await ContentClient.Create<dynamic>("aut", schemaName, AssetLoader.Schema1Data1Post.Value);
      var post2response = await ContentClient.Create<dynamic>("aut", schemaName, AssetLoader.Schema1Data2Post.Value);

      var queryresponse = await ContentClient.Query<dynamic>("aut", schemaName, new QueryRequest()
      {
        Skip = 0,
        Top = 100
      });

      var post1responsestring = JsonConvert.SerializeObject(post1response, Formatting.Indented); 
      var post2responsestring = JsonConvert.SerializeObject(post2response, Formatting.Indented);
      var queryresponsestring = JsonConvert.SerializeObject(queryresponse, Formatting.Indented);

      await SchemaStories.DeleteSchema(SchemaDeleteSystemUnderTest, "aut", schemaName);
    }

    [Fact]
    public async Task ContentImport_Execute_EndToEnd()
    {
      var schemaName = GetRandomSchemaName;
      await SchemaClient.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
      await SchemaStories.ImportSchema(SchemaImportSystemUnderTest, "aut", schemaName, AssetLoader.Schema1Path);

      var expectedFirst = AssetLoader.Schema1DataQueryResponse.Items[0];
      var expectedSecond = AssetLoader.Schema1DataQueryResponse.Items[1];

      await ContentStories.ImportContent(ContentImportSystemUnderTest, "aut", schemaName, AssetLoader.Schema1DataImportPath, publish: true);

      var content = await ContentClient.Query<dynamic>("aut", schemaName, new QueryRequest()
      {
        Skip = 0,
        Top = 100
      });

      var testCount = content.Total;
      testCount.Should().Be(2);

      var actualFirst = content.Items[0];
      var actualSecond = content.Items[1];

      // todo : verify

      await SchemaStories.DeleteSchema(SchemaDeleteSystemUnderTest, "aut", schemaName);
    }

    [Fact]
    public async Task ContentExport_Execute_EndToEnd()
    {
      var schemaName = GetRandomSchemaName;
      await SchemaClient.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
      await SchemaStories.ImportSchema(SchemaImportSystemUnderTest, "aut", schemaName, AssetLoader.Schema1Path);
      await ContentStories.ImportContent(ContentImportSystemUnderTest, "aut", schemaName, AssetLoader.Schema1DataImportPath, publish: true);

      var expectedFirst = AssetLoader.Schema1DataExportResponse.Items[0];
      var expectedSecond = AssetLoader.Schema1DataExportResponse.Items[1];

      var exportPath = Path.Combine(AssetLoader.ExportPath, $"{nameof(ContentHandlersIntegrationTest)} {nameof(ContentExport_Execute_EndToEnd)}-out.json");

      // act

      await ContentStories.ExportContent(ContentExportSystemUnderTest, "aut", schemaName, exportPath, top: "10", skip: "0");
      var exportedFileExists = File.Exists(exportPath);
      exportedFileExists.Should().BeTrue($"{nameof(SchemaExportRequest)} failed to export file");

      // todo : verify export content

      await SchemaStories.DeleteSchema(SchemaDeleteSystemUnderTest, "aut", schemaName);
    }

    [Fact]
    public async Task ContentDelete_Execute_EndToEnd()
    {
      var schemaName = GetRandomSchemaName;
      await SchemaClient.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
      await SchemaStories.ImportSchema(SchemaImportSystemUnderTest, "aut", schemaName, AssetLoader.Schema1Path);
      await ContentStories.ImportContent(ContentImportSystemUnderTest, "aut", schemaName, AssetLoader.Schema1DataImportPath, publish: true);

      var content = await ContentClient.Query<dynamic>("aut", schemaName, new QueryRequest()
      {
        Skip = 0,
        Top = 100
      });

      content.Total.Should().Be(2);
      var actualFirst = content.Items[0];
      var actualSecond = content.Items[1];

      // act

      await ContentStories.DeleteContent(ContentDeleteSystemUnderTest, "aut", schemaName, actualFirst.Id);

      // todo : verify export content

      await ContentClient.AssertContentMustNotExists("aut", schemaName, actualFirst.Id);
      await ContentClient.AssertContentMustExists("aut", schemaName, actualSecond.Id);

      // clean up

      await SchemaStories.DeleteSchema(SchemaDeleteSystemUnderTest, "aut", schemaName);
    }

    [Fact]
    public async Task ContentPost_Execute_EndToEnd()
    {
      var schemaName = GetRandomSchemaName;
      await SchemaClient.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
      await SchemaStories.ImportSchema(SchemaImportSystemUnderTest, "aut", schemaName, AssetLoader.Schema1Path);

      var expectedFirst = AssetLoader.Schema1DataExportResponse.Items[0];
      var expectedSecond = AssetLoader.Schema1DataExportResponse.Items[1];

      // act

      await ContentStories.PostContent(ContentPostSystemUnderTest, "aut", schemaName, AssetLoader.Schema1Data1PostPath, publish: true);
      await ContentStories.PostContent(ContentPostSystemUnderTest, "aut", schemaName, AssetLoader.Schema1Data2PostPath, publish: true);

      var content = await ContentClient.Query<dynamic>("aut", schemaName, new QueryRequest()
      {
        Skip = 0,
        Top = 100
      });

      content.Total.Should().Be(2);
      var actualFirst = content.Items[0];
      var actualSecond = content.Items[1];

      // todo : verify export content

      await SchemaStories.DeleteSchema(SchemaDeleteSystemUnderTest, "aut", schemaName);
    }
  }
}