namespace MGNZ.Squidex.CLI.Common.Commands
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;

  using Autofac;

  using MediatR;

  using MGNZ.Squidex.Client;
  using MGNZ.Squidex.CLI.Common.Platform;
  using MGNZ.Squidex.CLI.Common.Routing;

  using Serilog;

  public class AssetDeleteHandler : BaseHandler<AssetDeleteRequest>
  {
    private readonly IConsoleWriter _consoleWriter;
    public AssetDeleteHandler(ILogger logger, IClientProxyFactory clientFactory, IConsoleWriter consoleWriter, IContainer container)
      : base(logger, clientFactory, container)
    {
      _consoleWriter = consoleWriter;
    }

    /// <inheritdoc />
    public override async Task<Unit> Handle(AssetDeleteRequest request, CancellationToken cancellationToken)
    {
      var proxy = ClientFactory.GetClientProxy<ISquidexAttachmentClient>(request.AliasCredentials);

      try
      {
        await proxy.DeleteAsset(request.Application, request.Id);
      }
      catch (Exception e)
      {
        // todo : log
      }

      return Unit.Value;
    }
  }

  [Noun("asset"), Verb("delete")]
  public class AssetDeleteRequest: BaseRequest
  {
    [Option("app", "application", required: true, ordanalityOrder: 1)] public string Application { get; set; }
    [Option("id", "id", required: true, ordanalityOrder: 1)] public string Id { get; set; }
    [Option("c", "alias-credentials")] public string AliasCredentials { get; set; }
  }
}