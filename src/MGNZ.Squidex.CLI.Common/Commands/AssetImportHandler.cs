namespace MGNZ.Squidex.CLI.Common.Commands
{
  using System.Threading;
  using System.Threading.Tasks;

  using Autofac;

  using MediatR;

  using MGNZ.Squidex.CLI.Common.Routing;

  using Serilog;

  public class AssetImportHandler : BaseHandler<AssetImportRequest>
  {
    public AssetImportHandler(ILogger logger, IClientProxyFactory clientFactory, IContainer container) : base(logger, clientFactory, container) { }

    /// <inheritdoc />
    public override async Task<Unit> Handle(AssetImportRequest request, CancellationToken cancellationToken)
    {
      return await base.Handle(request, cancellationToken);
    }
  }

  [Noun("asset"), Verb("import")]
  public class AssetImportRequest: BaseRequest
  {
    [Option("n", "name", required: true, ordanalityOrder: 1)] public string Name { get; set; }
    [Option("p", "path", required: true, ordanalityOrder: 2)] public string Path { get; set; }
    [Option("t", "tags", required: true, ordanalityOrder: 3)] public string Tags { get; set; }
    [Option("c", "alias-credentials")] public string AliasCredentials { get; set; }
  }
}