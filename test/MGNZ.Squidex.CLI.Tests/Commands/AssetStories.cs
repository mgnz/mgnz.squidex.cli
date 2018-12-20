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

    public static async Task<Unit> ListAsset(AssetListHandler assetImportHandler, string application, string aliasCredentials = "aut-developer")
    {
      return await assetImportHandler.Handle(new AssetListRequest()
      {
        AliasCredentials = aliasCredentials,
        Application = application
      }, CancellationToken.None);
    }

    public static async Task<Unit> TagAsset(AssetTagHandler handler, string application, string id, string[] tags, string aliasCredentials = "aut-developer")
    {
      return await handler.Handle(new AssetTagRequest()
      {
        AliasCredentials = aliasCredentials,
        Application = application,
        Id = id,
        Tags = string.Join(',', tags)
      }, CancellationToken.None);
    }

    public static async Task<Unit> UpdateAsset(AssetUpdateContentHandler handler, string application, string id, string path, string aliasCredentials = "aut-developer")
    {
      return await handler.Handle(new AssetUpdateContentRequest()
      {
        AliasCredentials = aliasCredentials,
        Application = application,
        Id = id,
        Path = path
      }, CancellationToken.None);
    }
  }
}