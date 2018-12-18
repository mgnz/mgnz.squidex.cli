namespace MGNZ.Squidex.CLI.Tests.Commands
{
  using System.Threading;
  using System.Threading.Tasks;

  using MediatR;

  using MGNZ.Squidex.CLI.Common.Commands;

  public static class AssetStories
  {
    public static async Task<Unit> DeleteAsset(AssetDeleteHandler assetDeleteHandler, string application, string id, string aliasCredentials = "aut-developer")
    {
      return await assetDeleteHandler.Handle(new AssetDeleteRequest()
      {
        AliasCredentials = aliasCredentials,
        Application = application,
        Id = id,
      }, CancellationToken.None);
    }

    public static async Task<Unit> ExportAsset(AssetExportHandler assetExportHandler, string application, string name, string path, string aliasCredentials = "aut-developer")
    {
      return await assetExportHandler.Handle(new AssetExportRequest()
      {
        AliasCredentials = aliasCredentials,
        Application = application,
        Name = name,
        Path = path,
      }, CancellationToken.None);
    }

    public static async Task<Unit> ImportAsset(AssetImportHandler assetImportHandler, string application, string name, string path, string mimeType = "", string aliasCredentials = "aut-developer")
    {
      return await assetImportHandler.Handle(new AssetImportRequest()
      {
        AliasCredentials = aliasCredentials,
        Application = application,
        Name = name,
        MimeType = mimeType,
        Path = path
      }, CancellationToken.None);
    }
  }
}