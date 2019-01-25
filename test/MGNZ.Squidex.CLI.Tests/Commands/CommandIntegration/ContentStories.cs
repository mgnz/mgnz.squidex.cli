namespace MGNZ.Squidex.CLI.Tests.Commands.CommandIntegration
{
  using System.Threading;
  using System.Threading.Tasks;

  using MediatR;

  using MGNZ.Squidex.CLI.Common.Commands;

  public class ContentStories
  {
    public static async Task<Unit> ImportContent(ContentImportHandler systemUnderTest, string application, string schema, string path, bool publish = false, string aliasCredentials = "aut-developer")
    {
      return await systemUnderTest.Handle(new ContentImportRequest()
      {
        AliasCredentials = aliasCredentials,
        Application = application,
        Schema = schema,
        Path = path,
        Publish = publish.ToString()
      }, CancellationToken.None);
    }

    public static async Task<Unit> ExportContent(ContentExportHandler systemUnderTest, ContentExportRequest request)
    {
      return await systemUnderTest.Handle(request, CancellationToken.None);
    }

    public static async Task<Unit> ExportContent(ContentExportHandler systemUnderTest, string application, string schema, string path, string all = null, string top = null, string skip = null, string orderBy = null, string queryBy = null, string searchBy = null, string filterBy = null, string aliasCredentials = "aut-developer")
    {
      return await ExportContent(systemUnderTest, new ContentExportRequest()
      {
        AliasCredentials = aliasCredentials,
        Application = application,
        Schema = schema,
        Path = path,
        All = all,
        Top = top,
        Skip = skip,
        OrderBy = orderBy,
        QueryBy = queryBy,
        SearchBy = searchBy,
        FilterBy = filterBy
      });
    }

    public static async Task<Unit> DeleteContent(ContentDeleteHandler systemUnderTest, string application, string schema, string id, string aliasCredentials = "aut-developer")
    {
      return await systemUnderTest.Handle(new ContentDeleteRequest
      {
        AliasCredentials = aliasCredentials,
        Application = application,
        Schema = schema,
        Id = id
      }, CancellationToken.None);
    }

    public static async Task<Unit> PostContent(ContentPostHandler systemUnderTest, string application, string schema, string path, bool publish = false, string aliasCredentials = "aut-developer")
    {
      return await systemUnderTest.Handle(new ContentPostRequest
      {
        AliasCredentials = aliasCredentials,
        Application = application,
        Schema = schema,
        Path = path,
        Publish = publish.ToString()
      }, CancellationToken.None);
    }
  }
}