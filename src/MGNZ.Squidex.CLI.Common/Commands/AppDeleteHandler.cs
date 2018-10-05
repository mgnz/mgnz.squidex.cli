namespace MGNZ.Squidex.CLI.Common.Commands
{
  using System.Threading;
  using System.Threading.Tasks;

  using Autofac;

  using MediatR;

  using MGNZ.Squidex.CLI.Common.Routing;

  using Serilog;

  public class AppDeleteHandler : BaseHandler<AppDeleteRequest>
  {
    public AppDeleteHandler(ILogger logger, IContainer container) : base(logger, container) { }

    /// <inheritdoc />
    public override async Task<Unit> Handle(AppDeleteRequest request, CancellationToken cancellationToken)
    {
      return await base.Handle(request, cancellationToken);
    }
  }

  [Noun("app"), Verb("delete")]
  public class AppDeleteRequest: BaseRequest
  {
    [Option("n", "name", required:true, ordanalityOrder:1)] public string Name { get; set; }
    [Option("t", "token", required: true, ordanalityOrder: 2)] public string Token { get; set; }
  }
}