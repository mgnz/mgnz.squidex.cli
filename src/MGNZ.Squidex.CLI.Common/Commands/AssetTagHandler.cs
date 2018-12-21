namespace MGNZ.Squidex.CLI.Common.Commands
{
  using System.Threading;
  using System.Threading.Tasks;

  using Autofac;

  using MediatR;

  using MGNZ.Squidex.Client;
  using MGNZ.Squidex.Client.Transport;
  using MGNZ.Squidex.CLI.Common.Platform;
  using MGNZ.Squidex.CLI.Common.Routing;

  using Serilog;

  public class AssetTagHandler : BaseHandler<AssetTagRequest>
  {
    private readonly IConsoleWriter _consoleWriter;
    public AssetTagHandler(ILogger logger, IClientProxyFactory clientFactory, IConsoleWriter consoleWriter, IContainer container)
      : base(logger, clientFactory, container)
    {
      _consoleWriter = consoleWriter;
    }

    /// <inheritdoc />
    public override async Task<Unit> Handle(AssetTagRequest request, CancellationToken cancellationToken)
    {
      var proxy = ClientFactory.GetClientProxy<ISquidexAttachmentClient>(request.AliasCredentials);

      var tagsArray = request.Tags.Split(new char[] {','});

      await proxy.UpdateAssetTags(request.Application, request.Id, new UpdateAssetDto()
      {
        Tags = tagsArray
      });

      return Unit.Value;
    }
  }

  [Noun("asset"), Verb("tag")]
  public class AssetTagRequest: BaseRequest
  {
    [Option("app", "application", required: true, ordanalityOrder: 1)] public string Application { get; set; }
    [Option("id", "id", required: true, ordanalityOrder: 1)] public string Id { get; set; }
    [Option("t", "tags", required: true, ordanalityOrder: 2)] public string Tags { get; set; }
    [Option("c", "alias-credentials")] public string AliasCredentials { get; set; }
  }
}