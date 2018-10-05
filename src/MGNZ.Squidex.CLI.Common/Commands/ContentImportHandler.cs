namespace MGNZ.Squidex.CLI.Common.Commands
{
  using System.Threading;
  using System.Threading.Tasks;

  using Autofac;

  using MediatR;

  using MGNZ.Squidex.CLI.Common.Routing;

  using Serilog;

  public class ContentImportHandler : BaseHandler<ContentImportRequest>
  {
    public ContentImportHandler(ILogger logger, IContainer container) : base(logger, container) { }
    /// <inheritdoc />
    public override async Task<Unit> Handle(ContentImportRequest request, CancellationToken cancellationToken)
    {
      return await base.Handle(request, cancellationToken);
    }
  }

  [Noun("content"), Verb("import")]
  public class ContentImportRequest: BaseRequest
  {
    [Option("sc", "schema", required: true, ordanalityOrder: 1)] public string Schema { get; set; }
    [Option("p", "path", required: true, ordanalityOrder: 2)] public string Path { get; set; }
    [Option("c", "alias-credentials")] public string AliasCredentials { get; set; }
  }
}