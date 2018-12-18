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

      await SchemaChecker.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));

      await SchemaStories.ImportSchema(SchemaImportHandler, "aut", schemaName, AssetLoader.Schema1Path);
      await SchemaChecker.AssertSchemaMustExist("aut", schemaName, delay: TimeSpan.FromSeconds(0.5));

      await SchemaStories.DeleteSchema(SchemaDeleteHandler, "aut", schemaName);
      await SchemaChecker.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
    }

    [Fact]
    public async Task SchemaExport_Execute_EndToEnd()
    {
      var schemaName = GetRandomSchemaName;
      var exportPath = Path.Combine(AssetLoader.ExportPath, $"{nameof(SchemaHandlersIntegrationTest)} {nameof(SchemaExport_Execute_EndToEnd)}-out.json");

      await SchemaChecker.AssertNoSchemasExist("aut");
      await SchemaStories.ImportSchema(SchemaImportHandler, "aut", schemaName, AssetLoader.Schema1Path);

      await SchemaStories.ExportSchema(SchemaExportHandler, "aut", schemaName, exportPath);
      var exportedFileExists = File.Exists(exportPath);
      exportedFileExists.Should().BeTrue($"{nameof(SchemaExportRequest)} failed to export file");

      // todo validate export file

      await SchemaStories.DeleteSchema(SchemaDeleteHandler, "aut", schemaName);
      await SchemaChecker.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
    }

    [Fact]
    public async Task SchemaDelete_Execute_EndToEnd()
    {
      var schemaName = GetRandomSchemaName;

      await SchemaChecker.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
      await SchemaStories.ImportSchema(SchemaImportHandler, "aut", schemaName, AssetLoader.Schema1Path);

      await SchemaStories.DeleteSchema(SchemaDeleteHandler, "aut", schemaName);
      await SchemaChecker.AssertSchemaMustNotExist("aut", schemaName, delay: TimeSpan.FromSeconds(5));

      await SchemaChecker.AssertNoSchemasExist("aut", delay: TimeSpan.FromSeconds(0.5));
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