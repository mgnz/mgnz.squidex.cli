
namespace MGNZ.Squidex.CLI.Tests.Commands.CommandIntegration
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
  using MGNZ.Squidex.Client;
  using Newtonsoft.Json;

  [Collection("Sequential Squidex Integration Tests")]
  [Trait("category", "squidex-cli-integration")]
  public class ContentCommandIntegrationTest : BaseCommandIntegrationTest
  {
    //[Fact(Skip = "used to setup data in assets only")]
    //public async Task get_asset_data()
    //{
    //  var schemaName = GetRandomSchemaName;
    //  await SchemaClient.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
    //  await SchemaStories.ImportSchema(SchemaImportSystemUnderTest, "aut", schemaName, AssetLoader.Schema1Path);

    //  var post1response = await ContentClient.CreateContent("aut", schemaName, AssetLoader.Schema1Data1Post.Value);
    //  var post2response = await ContentClient.CreateContent("aut", schemaName, AssetLoader.Schema1Data2Post.Value);

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
      var actuals = await ContentClient.QueryContent("aut", name1, new QueryRequest()
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
      var export = Path.Combine(AssetLoader.ExportPath, $"{nameof(ContentCommandIntegrationTest)} {nameof(ContentExport_Execute_EndToEnd)}-out.json");

      await SchemaClient.CreateSchema("aut", AssetLoader.Schema1(name1));
      await SchemaClient.PublishSchema("aut", name1);

      dynamic create1response = await ContentClient.CreateContent("aut", name1, AssetLoader.Schema1Data1Post);
      dynamic create2response = await ContentClient.CreateContent("aut", name1, AssetLoader.Schema1Data2Post);
      string create1id = Convert.ToString(create1response.id);
      string create2id = Convert.ToString(create2response.id);
      await ContentClient.PublishContent("aut", name1, create1id);
      await ContentClient.PublishContent("aut", name1, create2id);

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

      dynamic create1response = await ContentClient.CreateContent("aut", name1, AssetLoader.Schema1Data1Post);
      dynamic create2response = await ContentClient.CreateContent("aut", name1, AssetLoader.Schema1Data2Post);
      string create1id = Convert.ToString(create1response.id);
      string create2id = Convert.ToString(create2response.id);
      await ContentClient.PublishContent("aut", name1, create1id);
      await ContentClient.PublishContent("aut", name1, create2id);

      // act

      await ContentStories.DeleteContent(ContentDeleteSystemUnderTest, "aut", name1, create1id);

      // todo : verify export content

      await ContentClient.AssertContentMustNotExists("aut", name1, create1id, delay: TimeSpan.FromSeconds(0.5));
      await ContentClient.AssertContentMustExists("aut", name1, create2id, delay: TimeSpan.FromSeconds(0.5));

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

      var content = await ContentClient.QueryContent("aut", name1, new QueryRequest()
      {
        Skip = 0,
        Top = 100
      });

      int contentTotal = Convert.ToInt32(content.Total);
      contentTotal.Should().Be(2);
      var actualFirst = content.Items[0];
      var actualSecond = content.Items[1];

      // todo : verify export content

      await SchemaClient.DeleteSchema("aut", name1);
      await SchemaClient.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
    }
  }

    static class foo
    {
      public static async Task<QueryResponse<dynamic>> QueryContent(this ISquidexContentClient that, string app, string schema, QueryRequest request)
      {
        dynamic raw = await that.QueryContent(app, schema, request.Top, request.Skip, request.OrderBy, request.Search, request.Filter);
        var deserialized = JsonConvert.DeserializeObject<QueryResponse<dynamic>>(raw.ToString());

        return deserialized;
      }
  }
}