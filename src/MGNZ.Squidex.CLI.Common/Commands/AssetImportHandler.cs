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
  using Newtonsoft.Json;
  using Serilog;

  public class AssetImportHandler : BaseHandler<AssetImportRequest>
  {
    private readonly IConsoleWriter _consoleWriter;

    public AssetImportHandler(ILogger logger, IClientProxyFactory clientFactory, IConsoleWriter consoleWriter, IContainer container)
      : base(logger, clientFactory, container)
    {
      _consoleWriter = consoleWriter;
    }

    /// <inheritdoc />
    public override async Task<Unit> Handle(AssetImportRequest request, CancellationToken cancellationToken)
    {
      var proxy = ClientFactory.GetClientProxy<ISquidexAttachmentClient>(request.AliasCredentials);
      var fileStream = await FileEx.ReadFileAsStream(request.Path); 

      if(string.IsNullOrEmpty(request.MimeType) || string.IsNullOrWhiteSpace(request.MimeType))
      {
        request.MimeType = MimeTypeMap.GetMimeType(Path.GetExtension(request.Name));
      }

      var response = await proxy.CreateAsset(request.Application, request.Name, request.MimeType, fileStream);

      return Unit.Value;
    }
  }

  [Noun("asset"), Verb("import")]
  public class AssetImportRequest: BaseRequest
  {
    [Option("app", "application", required: true, ordanalityOrder: 1)] public string Application { get; set; }
    [Option("n", "name", required: true, ordanalityOrder: 2)] public string Name { get; set; }
    [Option("p", "path", required: true, ordanalityOrder: 3)] public string Path { get; set; }
    [Option("t", "tags", required: false)] public string Tags { get; set; }
    [Option("m", "mimetype", required: false)] public string MimeType { get; set; }
    [Option("c", "alias-credentials")] public string AliasCredentials { get; set; }
  }
}