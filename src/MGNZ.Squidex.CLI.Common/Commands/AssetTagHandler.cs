namespace MGNZ.Squidex.CLI.Common.Commands
{
  using System.Threading;
  using System.Threading.Tasks;

  using Autofac;

  using MediatR;

  using Serilog;

  public class AssetTagHandler : BaseHandler<AssetTagRequest>
  {
    public AssetTagHandler(ILogger logger, IContainer container) : base(logger, container) { }

    /// <inheritdoc />
    public override async Task<Unit> Handle(AssetTagRequest request, CancellationToken cancellationToken)
    {
      return await base.Handle(request, cancellationToken);
    }
  }

  public class AssetTagRequest : IRequest<Unit>
  {
  }
}