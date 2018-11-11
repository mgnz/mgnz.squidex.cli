namespace MGNZ.Squidex.CLI.Tests.Commands
{
  using System.Threading;
  using System.Threading.Tasks;

  using MediatR;

  using MGNZ.Squidex.CLI.Common.Commands;

  public static class SchemaStories
  {
    public static async Task<Unit> DeleteSchema(SchemaDeleteHandler schemaDeleteHandler, string application, string name, string aliasCredentials = "aut-developer")
    {
      return await schemaDeleteHandler.Handle(new SchemaDeleteRequest()
      {
        AliasCredentials = aliasCredentials,
        Application = application,
        Name = name,
      }, CancellationToken.None);
    }

    public static async Task<Unit> ExportSchema(SchemaExportHandler schemaExportHandler, string application, string name, string path, string aliasCredentials = "aut-developer")
    {
      return await schemaExportHandler.Handle(new SchemaExportRequest()
      {
        AliasCredentials = aliasCredentials,
        Application = application,
        Name = name,
        Path = path,
      }, CancellationToken.None);
    }

    public static async Task<Unit> ImportSchema(SchemaImportHandler schemaImportHandler, string application, string name, string path, string aliasCredentials = "aut-developer")
    {
      return await schemaImportHandler.Handle(new SchemaImportRequest()
      {
        AliasCredentials = aliasCredentials,
        Application = application,
        Name = name,
        Path = path
      }, CancellationToken.None);
    }
  }
}