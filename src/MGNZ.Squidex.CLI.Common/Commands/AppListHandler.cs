namespace MGNZ.Squidex.CLI.Common.Commands
{
  using System.Threading;
  using System.Threading.Tasks;

  using Autofac;

  using MediatR;

  using MGNZ.Squidex.CLI.Common.Routing;

  using Serilog;

  public class AppListHandler : BaseHandler<AppListRequest>
  {
    public AppListHandler(ILogger logger, IClientProxyFactory clientFactory, IContainer container) : base(logger, clientFactory, container) { }

    /// <inheritdoc />
    public override async Task<Unit> Handle(AppListRequest request, CancellationToken cancellationToken)
    {
      return await base.Handle(request, cancellationToken);
    }
  }

  [Noun("app"), Verb("list")]
  public class AppListRequest: BaseRequest
  {
    [Option("t", "token", required: true, ordanalityOrder: 1)] public string Token { get; set; }
  }
}