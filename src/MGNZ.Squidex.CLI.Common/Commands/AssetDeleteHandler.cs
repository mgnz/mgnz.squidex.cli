namespace MGNZ.Squidex.CLI.Common.Commands
{
  using System.Threading;
  using System.Threading.Tasks;

  using Autofac;

  using MediatR;

  using Serilog;

  public class AssetDeleteHandler : BaseHandler<AssetDeleteRequest>
  {
    public AssetDeleteHandler(ILogger logger, IContainer container) : base(logger, container) { }

    /// <inheritdoc />
    public override async Task<Unit> Handle(AssetDeleteRequest request, CancellationToken cancellationToken)
    {
      return await base.Handle(request, cancellationToken);
    }
  }

  public class AssetDeleteRequest : IRequest<Unit>
  {
  }
}