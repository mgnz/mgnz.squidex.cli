namespace MGNZ.Squidex.CLI.Common.Commands
{
  using System.Threading;
  using System.Threading.Tasks;

  using Autofac;

  using MediatR;

  using MGNZ.Squidex.Client;
  using MGNZ.Squidex.Client.Model;
  using MGNZ.Squidex.CLI.Common.Platform;
  using MGNZ.Squidex.CLI.Common.Routing;

  using Serilog;

  public class AssetListHandler : BaseHandler<AssetListRequest>
  {
    private readonly IConsoleWriter _consoleWriter;
    public AssetListHandler(ILogger logger, IClientProxyFactory clientFactory, IConsoleWriter consoleWriter, IContainer container)
      : base(logger, clientFactory, container)
    {
      _consoleWriter = consoleWriter;
    }

    /// <inheritdoc />
    public override async Task<Unit> Handle(AssetListRequest request, CancellationToken cancellationToken)
    {
      var proxy = ClientFactory.GetClientProxy<ISquidexAttachmentClient>(request.AliasCredentials);

      var dto = await proxy.GetAssets(request.Application, new ListRequest()
      {
        Skip = 0, Top = 200
      });

      // todo : output to console

      return Unit.Value;
    }
  }

  [Noun("asset"), Verb("list")]
  public class AssetListRequest: BaseRequest
  {
    [Option("app", "application", required: true, ordanalityOrder: 1)] public string Application { get; set; }
    [Option("c", "alias-credentials")] public string AliasCredentials { get; set; }
  }
}