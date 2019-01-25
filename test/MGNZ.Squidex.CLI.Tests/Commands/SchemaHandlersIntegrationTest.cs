namespace MGNZ.Squidex.CLI.Tests.Commands
{
  using System;
  using System.IO;
  using System.Threading.Tasks;

  using FluentAssertions;

  using MGNZ.Squidex.CLI.Common.Commands;
  using MGNZ.Squidex.Tests.Shared.Code;

  using Xunit;

  [Collection("Sequential Squidex Integration Tests")]
  [Trait("category", "squidex-cli-integration")]
  public class SchemaHandlersIntegrationTest : BaseHandlerIntegrationTest
  {
    [Fact]
    public async Task SchemaImport_Execute_EndToEnd()
    {
      var name1 = GetRandomSchemaName;

      await SchemaClient.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));

      await SchemaStories.ImportSchema(SchemaImportSystemUnderTest, "aut", name1, AssetLoader.Schema1Path);
      await SchemaClient.AssertSchemaMustExist("aut", name1, delay: TimeSpan.FromSeconds(0.5));

      await SchemaClient.DeleteSchema("aut", name1);
      await SchemaClient.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
    }

    [Fact]
    public async Task SchemaExport_Execute_EndToEnd()
    {
      var name1 = GetRandomSchemaName;
      var exportPath1 = Path.Combine(AssetLoader.ExportPath, $"{nameof(SchemaHandlersIntegrationTest)} {nameof(SchemaExport_Execute_EndToEnd)}-out.json");

      await SchemaClient.AssertNoSchemasExist("aut");
      await SchemaClient.CreateSchema("aut", AssetLoader.Schema1(name1));
      await SchemaClient.PublishSchema("aut", name1);

      await SchemaStories.ExportSchema(SchemaExportSystemUnderTest, "aut", name1, exportPath1);
      var exportedFileExists = File.Exists(exportPath1);
      exportedFileExists.Should().BeTrue($"{nameof(SchemaExportRequest)} failed to export file");

      // todo validate export file

      await SchemaClient.DeleteSchema("aut", name1);
      await SchemaClient.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
    }

    [Fact]
    public async Task SchemaDelete_Execute_EndToEnd()
    {
      var name1 = GetRandomSchemaName;

      await SchemaClient.AssertNoSchemasExist("aut");
      await SchemaClient.CreateSchema("aut", AssetLoader.Schema1(name1));
      await SchemaClient.PublishSchema("aut", name1);

      await SchemaStories.DeleteSchema(SchemaDeleteSystemUnderTest, "aut", name1);
      await SchemaClient.AssertSchemaMustNotExist("aut", name1, delay: TimeSpan.FromSeconds(5));

      await SchemaClient.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
    }

    [Fact(Skip = "tag not implemented")]
    public Task SchemaList_Execute_EndToEnd()
    {
      throw new NotImplementedException();
    }

    [Fact(Skip = "tag not implemented")]
    public Task SchemaTag_Execute_EndToEnd()
    {
      throw new NotImplementedException();
    }
  }
}