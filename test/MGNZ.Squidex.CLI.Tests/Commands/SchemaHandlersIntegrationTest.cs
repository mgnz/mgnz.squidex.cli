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
      var schemaName = GetRandomSchemaName;

      await SchemaClient.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));

      await SchemaStories.ImportSchema(SchemaImportSystemUnderTest, "aut", schemaName, AssetLoader.Schema1Path);
      await SchemaClient.AssertSchemaMustExist("aut", schemaName, delay: TimeSpan.FromSeconds(0.5));

      await SchemaStories.DeleteSchema(SchemaDeleteSystemUnderTest, "aut", schemaName);
      await SchemaClient.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
    }

    [Fact]
    public async Task SchemaExport_Execute_EndToEnd()
    {
      var schemaName = GetRandomSchemaName;
      var exportPath = Path.Combine(AssetLoader.ExportPath, $"{nameof(SchemaHandlersIntegrationTest)} {nameof(SchemaExport_Execute_EndToEnd)}-out.json");

      await SchemaClient.AssertNoSchemasExist("aut");
      await SchemaStories.ImportSchema(SchemaImportSystemUnderTest, "aut", schemaName, AssetLoader.Schema1Path);

      await SchemaStories.ExportSchema(SchemaExportSystemUnderTest, "aut", schemaName, exportPath);
      var exportedFileExists = File.Exists(exportPath);
      exportedFileExists.Should().BeTrue($"{nameof(SchemaExportRequest)} failed to export file");

      // todo validate export file

      await SchemaStories.DeleteSchema(SchemaDeleteSystemUnderTest, "aut", schemaName);
      await SchemaClient.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
    }

    [Fact]
    public async Task SchemaDelete_Execute_EndToEnd()
    {
      var schemaName = GetRandomSchemaName;

      await SchemaClient.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
      await SchemaStories.ImportSchema(SchemaImportSystemUnderTest, "aut", schemaName, AssetLoader.Schema1Path);

      await SchemaStories.DeleteSchema(SchemaDeleteSystemUnderTest, "aut", schemaName);
      await SchemaClient.AssertSchemaMustNotExist("aut", schemaName, delay: TimeSpan.FromSeconds(5));

      await SchemaClient.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
    }

    [Fact(Skip = "tag not implemented")]
    public async Task SchemaListt_Execute_EndToEnd()
    {
    }

    [Fact(Skip = "tag not implemented")]
    public async Task SchemaTag_Execute_EndToEnd()
    {
    }
  }
}