namespace MGNZ.Squidex.CLI.Common.Commands
{
  using System.Threading;
  using System.Threading.Tasks;

  using Autofac;

  using MediatR;

  using Serilog.Core;

  public class AppNewHandler : BaseHandler<AppNewRequest>
  {
    public AppNewHandler(Logger logger, IContainer container) : base(logger, container) { }

    /// <inheritdoc />
    public override async Task<Unit> Handle(AppNewRequest request, CancellationToken cancellationToken)
    {
      return await base.Handle(request, cancellationToken);
    }
  }

  public class AppNewRequest : IRequest<Unit>
  {
  }
}