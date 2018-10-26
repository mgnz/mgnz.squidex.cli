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

  public class ContentImportHandler : BaseHandler<ContentImportRequest>
  {
    private IFileHandler _fileHandler = null;

    public ContentImportHandler(ILogger logger, IClientProxyFactory clientFactory, IContainer container)
      : base(logger, clientFactory, container)
    {
      this._fileHandler = Container.Resolve<IFileHandler>();
    }
    /// <inheritdoc />
    public override async Task<Unit> Handle(ContentImportRequest request, CancellationToken cancellationToken)
    {
      var proxy = ClientFactory.GetClientProxy<ISquidexContentClient>(request.AliasCredentials);
      dynamic inputFileContent = _fileHandler.ReadFile(request.Path);

      foreach (var itemContent in inputFileContent.items)
      {
        var data = itemContent.data;

        await proxy.Create<dynamic>(request.Application, request.Schema, data);
      }

      return Unit.Value;
    }
  }

  [Noun("content"), Verb("import")]
  public class ContentImportRequest: BaseRequest
  {
    [Option("app", "application", required: true, ordanalityOrder: 1)] public string Application { get; set; }
    [Option("sc", "schema", required: true, ordanalityOrder: 2)] public string Schema { get; set; }
    [Option("p", "path", required: true, ordanalityOrder: 3)] public string Path { get; set; }
    [Option("c", "alias-credentials")] public string AliasCredentials { get; set; }
  }
}