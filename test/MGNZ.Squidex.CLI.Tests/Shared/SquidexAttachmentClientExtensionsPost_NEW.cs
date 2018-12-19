namespace MGNZ.Squidex.Client
{
  using System;
  using System.IO;
  using System.Linq;
  using System.Threading.Tasks;

  using MGNZ.Squidex.Client.Model;

  using Refit;

  public static class SquidexAttachmentClientExtensionsPost_NEW
  {
    public static async Task<AttachmentContent> Post_NEW(this ISquidexAttachmentClient that, string application, string fileName, string mimeType, Stream stream)
    {
      return await that.PostAsset(application, new[]
      {
        new StreamPart(stream, fileName, mimeType)
      });
    }

    public static async Task<AttachmentContent> UpdateAssetContentById_NEW(this ISquidexAttachmentClient that, string application, string id, string fileName, string mimeType, Stream stream)
    {
      return await that.UpdateAssetContent(application, id, new[]
      {
        new StreamPart(stream, fileName, mimeType)
      });
    }

    public static async Task<AttachmentContent> UpdateAssetContentByNamePost_NEW(this ISquidexAttachmentClient that, string application, string fileName, string mimeType, Stream stream)
    {
      var item = await that.GetByNameOrDefault_NEW(application, fileName);

      return await that.UpdateAssetContentById_NEW(application, item.Id, fileName, mimeType, stream);
    }

    public static async Task DeleteByNamePost_NEW(this ISquidexAttachmentClient that, string application, string fileName)
    {
      var item = await that.GetByNameOrDefault_NEW(application, fileName);

      await that.DeleteAsset(application, item.Id);
    }

    public static async Task<AttachmentContent> GetByNameOrDefault_NEW(this ISquidexAttachmentClient that, string application, string fileName)
    {
      if (Path.HasExtension(fileName)) fileName = Path.GetFileNameWithoutExtension(fileName);

      // todo : pagination
      var data = await that.GetAssets(application, new ListRequest()
      {
        Skip = 0, Top = 200
      });

      var count = data.Total;
      if (count == 0) return default(AttachmentContent);

      var item = data.Items.FirstOrDefault(d => CheckEquality(d, fileName));

      return item;
    }

    public static async Task<bool> AttachmentExistsPost_NEW(this ISquidexAttachmentClient that, string application,
      string fileName = null)
    {
      var item = await that.GetByNameOrDefault_NEW(application, fileName);

      return item != null;
    }

    private static bool CheckEquality(AttachmentContent d, string name)
    {
      var dynamicName = Convert.ToString(d.FileName);

      return name.Equals(dynamicName);
    }
  }
}