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

  using Newtonsoft.Json;

  using Serilog;

  public class ContentImportHandler : BaseHandler<ContentImportRequest>
  {
    private IConsoleWriter _consoleWriter = null;

    public ContentImportHandler(ILogger logger, IClientProxyFactory clientFactory, IConsoleWriter consoleWriter, IContainer container)
      : base(logger, clientFactory, container)
    {
      this._consoleWriter = consoleWriter;
    }
    /// <inheritdoc />
    public override async Task<Unit> Handle(ContentImportRequest request, CancellationToken cancellationToken)
    {
      var proxy = ClientFactory.GetClientProxy<ISquidexContentClient>(request.AliasCredentials);
      var json = await FileEx.ReadAllTextAsync(request.Path);
      var inputFileContent = JsonConvert.DeserializeObject<QueryResponse<dynamic>>(json);
      
      foreach (dynamic itemContent in inputFileContent.Items)
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