
namespace MGNZ.Squidex.CLI.Tests.Commands
{
  using System;
  using System.IO;
  using System.Threading.Tasks;

  using FluentAssertions;

  using MGNZ.Squidex.Client.Model;
  using MGNZ.Squidex.Client.Tests.Shared.Code;
  using MGNZ.Squidex.Client.Transport;
  using MGNZ.Squidex.CLI.Common.Commands;
  using MGNZ.Squidex.Tests.Shared.Code;

  using Xunit;

  [Collection("Sequential Squidex Integration Tests")]
  [Trait("category", "squidex-cli-integration")]
  public class ContentHandlersIntegrationTest : BaseHandlerIntegrationTest
  {
    //[Fact(Skip = "used to setup data in assets only")]
    //public async Task get_asset_data()
    //{
    //  var schemaName = GetRandomSchemaName;
    //  await SchemaClient.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
    //  await SchemaStories.ImportSchema(SchemaImportSystemUnderTest, "aut", schemaName, AssetLoader.Schema1Path);

    //  var post1response = await ContentClient.Create<dynamic>("aut", schemaName, AssetLoader.Schema1Data1Post.Value);
    //  var post2response = await ContentClient.Create<dynamic>("aut", schemaName, AssetLoader.Schema1Data2Post.Value);

    //  var queryresponse = await ContentClient.Query<dynamic>("aut", schemaName, new QueryRequest()
    //  {
    //    Skip = 0,
    //    Top = 100
    //  });

    //  var post1responsestring = JsonConvert.SerializeObject(post1response, Formatting.Indented);
    //  var post2responsestring = JsonConvert.SerializeObject(post2response, Formatting.Indented);
    //  var queryresponsestring = JsonConvert.SerializeObject(queryresponse, Formatting.Indented);

    //  await SchemaStories.DeleteSchema(SchemaDeleteSystemUnderTest, "aut", schemaName);
    //}

    [Fact]
    public async Task ContentImport_Execute_EndToEnd()
    {
      var name1 = GetRandomSchemaName;
      await SchemaClient.CreateSchema("aut", AssetLoader.Schema1(name1));
      await SchemaClient.PublishSchema("aut", name1);

      var expected1 = AssetLoader.Schema1DataQueryResponse.Items[0];
      var expected2 = AssetLoader.Schema1DataQueryResponse.Items[1];

      await ContentStories.ImportContent(ContentImportSystemUnderTest, "aut", name1, AssetLoader.Schema1DataImportPath, publish: true);
      var actuals = await ContentClient.Query<dynamic>("aut", name1, new QueryRequest()
      {
        Skip = 0,
        Top = 100
      });

      var actualsTotal = actuals.Total;
      actualsTotal.Should().Be(2);

      var actual1 = actuals.Items[0];
      var actual2 = actuals.Items[1];

      // todo : verify

      await SchemaClient.DeleteSchema("aut", name1);
      await SchemaClient.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
    }

    [Fact]
    public async Task ContentExport_Execute_EndToEnd()
    {
      var name1 = GetRandomSchemaName;
      var export = Path.Combine(AssetLoader.ExportPath, $"{nameof(ContentHandlersIntegrationTest)} {nameof(ContentExport_Execute_EndToEnd)}-out.json");

      await SchemaClient.CreateSchema("aut", AssetLoader.Schema1(name1));
      await SchemaClient.PublishSchema("aut", name1);

      ItemContent<dynamic> create1 = await ContentClient.Create<dynamic>("aut", name1, AssetLoader.Schema1Data1Post);
      ItemContent<dynamic> create2 = await ContentClient.Create<dynamic>("aut", name1, AssetLoader.Schema1Data2Post);
      await ContentClient.Publish("aut", name1, create1.Id);
      await ContentClient.Publish("aut", name1, create2.Id);

      var expected1 = AssetLoader.Schema1DataExportResponse.Items[0];
      var expected2 = AssetLoader.Schema1DataExportResponse.Items[1];

      // act

      await ContentStories.ExportContent(ContentExportSystemUnderTest, "aut", name1, export, top: "10", skip: "0");
      var exportExists = File.Exists(export);
      exportExists.Should().BeTrue($"{nameof(SchemaExportRequest)} failed to export file");

      // todo : verify export content

      await SchemaClient.DeleteSchema("aut", name1);
      await SchemaClient.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
    }

    [Fact]
    public async Task ContentDelete_Execute_EndToEnd()
    {
      var name1 = GetRandomSchemaName;
      await SchemaClient.CreateSchema("aut", AssetLoader.Schema1(name1));
      await SchemaClient.PublishSchema("aut", name1);

      ItemContent<dynamic> create1 = await ContentClient.Create<dynamic>("aut", name1, AssetLoader.Schema1Data1Post);
      ItemContent<dynamic> create2 = await ContentClient.Create<dynamic>("aut", name1, AssetLoader.Schema1Data2Post);
      await ContentClient.Publish("aut", name1, create1.Id);
      await ContentClient.Publish("aut", name1, create2.Id);

      // act

      await ContentStories.DeleteContent(ContentDeleteSystemUnderTest, "aut", name1, create1.Id);

      // todo : verify export content

      await ContentClient.AssertContentMustNotExists("aut", name1, create1.Id);
      await ContentClient.AssertContentMustExists("aut", name1, create2.Id);

      // clean up

      await SchemaClient.DeleteSchema("aut", name1);
      await SchemaClient.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
    }

    [Fact]
    public async Task ContentPost_Execute_EndToEnd()
    {
      var name1 = GetRandomSchemaName;
      await SchemaClient.CreateSchema("aut", AssetLoader.Schema1(name1));
      await SchemaClient.PublishSchema("aut", name1);

      var expectedFirst = AssetLoader.Schema1DataExportResponse.Items[0];
      var expectedSecond = AssetLoader.Schema1DataExportResponse.Items[1];

      // act

      await ContentStories.PostContent(ContentPostSystemUnderTest, "aut", name1, AssetLoader.Schema1Data1PostPath, publish: true);
      await ContentStories.PostContent(ContentPostSystemUnderTest, "aut", name1, AssetLoader.Schema1Data2PostPath, publish: true);

      var content = await ContentClient.Query<dynamic>("aut", name1, new QueryRequest()
      {
        Skip = 0,
        Top = 100
      });

      content.Total.Should().Be(2);
      var actualFirst = content.Items[0];
      var actualSecond = content.Items[1];

      // todo : verify export content

      await SchemaClient.DeleteSchema("aut", name1);
      await SchemaClient.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
    }
  }
}