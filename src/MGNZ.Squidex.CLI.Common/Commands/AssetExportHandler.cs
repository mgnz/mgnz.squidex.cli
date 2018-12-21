namespace MGNZ.Squidex.CLI.Common.Commands
{
  using System.Threading;
  using System.Threading.Tasks;

  using Autofac;

  using MediatR;

  using MGNZ.Squidex.CLI.Common.Platform;
  using MGNZ.Squidex.CLI.Common.Routing;

  using Serilog;

  public class AssetExportHandler : BaseHandler<AssetExportRequest>
  {
    private readonly IConsoleWriter _consoleWriter;
    public AssetExportHandler(ILogger logger, IClientProxyFactory clientFactory, IConsoleWriter consoleWriter, IContainer container)
      : base(logger, clientFactory, container)
    {
      _consoleWriter = consoleWriter;
    }

    /// <inheritdoc />
    public override async Task<Unit> Handle(AssetExportRequest request, CancellationToken cancellationToken)
    {
      return await base.Handle(request, cancellationToken);
    }
  }

  [Noun("asset"), Verb("export")]
  public class AssetExportRequest: BaseRequest
  {
    [Option("app", "application", required: true, ordanalityOrder: 1)] public string Application { get; set; }
    [Option("n", "name", required: true, ordanalityOrder: 1)] public string Name { get; set; }
    [Option("p", "path", required: true, ordanalityOrder: 2)] public string Path { get; set; }
    [Option("c", "alias-credentials")] public string AliasCredentials { get; set; }
  }
}