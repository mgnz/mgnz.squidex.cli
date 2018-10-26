namespace MGNZ.Squidex.CLI.Common.Commands
{
  using System;
  using System.Linq;
  using System.Threading;
  using System.Threading.Tasks;

  using Autofac;

  using MediatR;

  using MGNZ.Squidex.Client;
  using MGNZ.Squidex.Client.Transport;
  using MGNZ.Squidex.CLI.Common.Platform;
  using MGNZ.Squidex.CLI.Common.Routing;

  using Serilog;

  public class ContentExportHandler : BaseHandler<ContentExportRequest>
  {
    private IFileHandler _fileHandler = null;

    public ContentExportHandler(ILogger logger, IClientProxyFactory clientFactory, IContainer container)
      : base(logger, clientFactory, container)
    {
      this._fileHandler = Container.Resolve<IFileHandler>();
    }

    /// <inheritdoc />
    public override async Task<Unit> Handle(ContentExportRequest request, CancellationToken cancellationToken)
    {
      var proxy = ClientFactory.GetClientProxy<ISquidexContentClient>(request.AliasCredentials);

      var outputFileContent = await proxy.Query<dynamic>(request.Application, request.Schema, new QueryRequest()
      {
        Search = request.SearchBy,
        Filter = request.FilterBy,
        OrderBy = request.OrderBy,
        Skip = Convert.ToInt32(request.Skip),
        Top = Convert.ToInt32(request.Top)
      });

      await _fileHandler.WriteFile(request.Path, outputFileContent);

      return Unit.Value;
    }
  }

  [Noun("content"), Verb("export")]
  public class ContentExportRequest: BaseRequest
  {
    [Option("a", "application", required: true, ordanalityOrder: 1)] public string Application { get; set; }
    [Option("sc", "schema", required: true, ordanalityOrder: 2)] public string Schema { get; set; }
    [Option("p", "path", required: true, ordanalityOrder: 3)] public string Path { get; set; }
    [Option("a", "all")] public string All { get; set; }
    [Option("t", "top")] public string Top { get; set; }
    [Option("s", "skip")] public string Skip { get; set; }
    [Option("ob", "order-by")] public string OrderBy { get; set; }
    [Option("qb", "query-by")] public string QueryBy { get; set; }
    [Option("sb", "search-by")] public string SearchBy { get; set; }
    [Option("fb", "filter-by")] public string FilterBy { get; set; }
    [Option("c", "alias-credentials")] public string AliasCredentials { get; set; }
  }
}