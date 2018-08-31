namespace MGNZ.Squidex.CLI.Common.Commands
{
  using System.Threading;
  using System.Threading.Tasks;

  using Autofac;

  using MediatR;

  using MGNZ.Squidex.CLI.Common.CLI;

  using Serilog;

  public class AppLogoutHandler : BaseHandler<AppLogoutRequest>
  {
    public AppLogoutHandler(ILogger logger, IContainer container) : base(logger, container) { }

    /// <inheritdoc />
    public override async Task<Unit> Handle(AppLogoutRequest request, CancellationToken cancellationToken)
    {
      return await base.Handle(request, cancellationToken);
    }
  }

  public class AppLogoutRequest : IRequest
  {
    [Option("n", "name")] public string Name { get; set; }
    [Option("c", "alias-credentials")] public string AliasCredentials { get; set; }
  }
}