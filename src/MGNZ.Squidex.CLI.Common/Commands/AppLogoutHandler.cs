namespace MGNZ.Squidex.CLI.Common.Commands
{
  using System.Threading;
  using System.Threading.Tasks;

  using Autofac;

  using MediatR;

  using Serilog.Core;

  public class AppLogoutHandler : BaseHandler<AppLogoutRequest>
  {
    public AppLogoutHandler(Logger logger, IContainer container) : base(logger, container) { }

    /// <inheritdoc />
    public override async Task<Unit> Handle(AppLogoutRequest request, CancellationToken cancellationToken)
    {
      return await base.Handle(request, cancellationToken);
    }
  }

  public class AppLogoutRequest : IRequest<Unit>
  {
  }
}