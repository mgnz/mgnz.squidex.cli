namespace MGNZ.Squidex.CLI.Common.Commands
{
  using System.Threading;
  using System.Threading.Tasks;

  using Autofac;

  using MediatR;

  using MGNZ.Squidex.Client;
  using MGNZ.Squidex.CLI.Common.Platform;
  using MGNZ.Squidex.CLI.Common.Routing;

  using Serilog;

  public class ContentDeleteHandler : BaseHandler<ContentDeleteRequest>
  {
    private IConsoleWriter _consoleWriter = null;

    public ContentDeleteHandler(ILogger logger, IClientProxyFactory clientFactory, IConsoleWriter consoleWriter, IContainer container)
      : base(logger, clientFactory, container)
    {
      this._consoleWriter = consoleWriter;
    }

    /// <inheritdoc />
    public override async Task<Unit> Handle(ContentDeleteRequest request, CancellationToken cancellationToken)
    {
      var proxy = ClientFactory.GetClientProxy<ISquidexContentClient>(request.AliasCredentials);

      await proxy.Delete(request.Application, request.Schema, request.Id);

      return Unit.Value;
    }
  }

  [Noun("content"), Verb("delete")]
  public class ContentDeleteRequest: BaseRequest
  {
    [Option("app", "application", required: true, ordanalityOrder: 1)] public string Application { get; set; }
    [Option("sc", "schema", required: true, ordanalityOrder: 2)] public string Schema { get; set; }
    [Option("id", "id", required: true, ordanalityOrder: 2)] public string Id { get; set; }
    [Option("c", "alias-credentials")] public string AliasCredentials { get; set; }
  }
}