namespace MGNZ.Squidex.CLI.Common.Commands
{
  using System.Threading;
  using System.Threading.Tasks;

  using Autofac;

  using MediatR;

  using MGNZ.Squidex.CLI.Common.CLI;

  using Serilog;

  public class ContentExportHandler : BaseHandler<ContentExportRequest>
  {
    public ContentExportHandler(ILogger logger, IContainer container) : base(logger, container) { }

    /// <inheritdoc />
    public override async Task<Unit> Handle(ContentExportRequest request, CancellationToken cancellationToken)
    {
      return await base.Handle(request, cancellationToken);
    }
  }

  [Noun("content"), Verb("export")]
  public class ContentExportRequest: BaseRequest
  {
    [Option("sc", "schema", required: true, ordanalityOrder: 1)] public string Schema { get; set; }
    [Option("p", "path", required: true, ordanalityOrder: 2)] public string Path { get; set; }
    [Option("a", "all")] public string All { get; set; }
    [Option("t", "top")] public string Top { get; set; }
    [Option("s", "skip")] public string Skip { get; set; }
    [Option("ob", "order-by")] public string OrderBy { get; set; }
    [Option("qb", "query-by")] public string QueryBy { get; set; }
    [Option("fb", "filter-by")] public string FilterBy { get; set; }
    [Option("c", "alias-credentials")] public string AliasCredentials { get; set; }
  }
}