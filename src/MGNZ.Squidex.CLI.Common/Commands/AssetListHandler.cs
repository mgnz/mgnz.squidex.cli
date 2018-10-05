namespace MGNZ.Squidex.CLI.Common.Commands
{
  using System.Threading;
  using System.Threading.Tasks;

  using Autofac;

  using MediatR;

  using MGNZ.Squidex.CLI.Common.Routing;

  using Serilog;

  public class AssetListHandler : BaseHandler<AssetListRequest>
  {
    public AssetListHandler(ILogger logger, IContainer container) : base(logger, container) { }

    /// <inheritdoc />
    public override async Task<Unit> Handle(AssetListRequest request, CancellationToken cancellationToken)
    {
      return await base.Handle(request, cancellationToken);
    }
  }

  [Noun("asset"), Verb("list")]
  public class AssetListRequest: BaseRequest
  {
    [Option("c", "alias-credentials")] public string AliasCredentials { get; set; }
  }
}