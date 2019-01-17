namespace MGNZ.Squidex.CLI.Common.Commands
{
  using System.IO;
  using System.Threading;
  using System.Threading.Tasks;

  using Autofac;

  using MediatR;
  using MGNZ.Squidex.CLI.Common.Platform;
  using MGNZ.Squidex.CLI.Common.Routing;
  using MGNZ.Squidex.Client;

  using Refit;

  using Serilog;

  public class AssetUpdateContentHandler : BaseHandler<AssetUpdateContentRequest>
  {
    private readonly IConsoleWriter _consoleWriter;

    public AssetUpdateContentHandler(ILogger logger, IClientProxyFactory clientFactory, IConsoleWriter consoleWriter, IContainer container)
      : base(logger, clientFactory, container)
    {
      _consoleWriter = consoleWriter;
    }

    /// <inheritdoc />
    public override async Task<Unit> Handle(AssetUpdateContentRequest contentRequest, CancellationToken cancellationToken)
    {
      var proxy = ClientFactory.GetClientProxy<ISquidexAttachmentClient>(contentRequest.AliasCredentials);
      var stream = await FileEx.ReadFileAsStream(contentRequest.Path); 

      if(string.IsNullOrEmpty(contentRequest.MimeType) || string.IsNullOrWhiteSpace(contentRequest.MimeType))
      {
        contentRequest.MimeType = MimeTypeMap.GetMimeType(Path.GetExtension(contentRequest.Id));
      }

      var response = await proxy.UpdateAsset(contentRequest.Application, contentRequest.Id, new[]
      {
        new StreamPart(stream, contentRequest.Id, contentRequest.MimeType)
      });

      return Unit.Value;
    }
  }

  [Noun("asset"), Verb("update")]
  public class AssetUpdateContentRequest: BaseRequest
  {
    [Option("app", "application", required: true, ordanalityOrder: 1)] public string Application { get; set; }
    [Option("id", "id", required: true, ordanalityOrder: 2)] public string Id { get; set; }
    [Option("p", "path", required: true, ordanalityOrder: 3)] public string Path { get; set; }
    [Option("m", "mimetype", required: false)] public string MimeType { get; set; }
    [Option("c", "alias-credentials")] public string AliasCredentials { get; set; }
  }
}