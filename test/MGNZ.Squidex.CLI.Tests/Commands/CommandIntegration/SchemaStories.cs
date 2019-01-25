namespace MGNZ.Squidex.CLI.Tests.Commands.CommandIntegration
{
  using System.Threading;
  using System.Threading.Tasks;

  using MediatR;

  using MGNZ.Squidex.CLI.Common.Commands;

  public static class SchemaStories
  {
    public static async Task<Unit> DeleteSchema(SchemaDeleteHandler systemUnderTest, string application, string name, string aliasCredentials = "aut-developer")
    {
      return await systemUnderTest.Handle(new SchemaDeleteRequest()
      {
        AliasCredentials = aliasCredentials,
        Application = application,
        Name = name,
      }, CancellationToken.None);
    }

    public static async Task<Unit> ExportSchema(SchemaExportHandler systemUnderTest, string application, string name, string path, string aliasCredentials = "aut-developer")
    {
      return await systemUnderTest.Handle(new SchemaExportRequest()
      {
        AliasCredentials = aliasCredentials,
        Application = application,
        Name = name,
        Path = path,
      }, CancellationToken.None);
    }

    public static async Task<Unit> ImportSchema(SchemaImportHandler systemUnderTest, string application, string name, string path, string aliasCredentials = "aut-developer")
    {
      return await systemUnderTest.Handle(new SchemaImportRequest()
      {
        AliasCredentials = aliasCredentials,
        Application = application,
        Name = name,
        Path = path
      }, CancellationToken.None);
    }
  }
}