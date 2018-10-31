namespace MGNZ.Squidex.CLI.Common.Commands
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;

  using Autofac;

  using MediatR;

  using MGNZ.Squidex.Client;
  using MGNZ.Squidex.Client.Transport;
  using MGNZ.Squidex.CLI.Common.Platform;
  using MGNZ.Squidex.CLI.Common.Routing;

  using Serilog;

  public class SchemaExportHandler : BaseHandler<SchemaExportRequest>
  {
    private readonly IFileHandler _fileHandler;

    public SchemaExportHandler(ILogger logger, IClientProxyFactory clientFactory, IFileHandler fileHandler, IContainer container)
      : base(logger, clientFactory, container)
    {
      _fileHandler = fileHandler;
    }

    /// <inheritdoc />
    public override async Task<Unit> Handle(SchemaExportRequest request, CancellationToken cancellationToken)
    {
      var proxy = ClientFactory.GetClientProxy<ISquidexAppSchemaClient>(request.AliasCredentials);

      if (await SchemaExists(request.Application, request.Name))
      {
        var outputFileContent = await proxy.GetSchema(request.Application, request.Name);
        ThrowSchemaGetFailure(outputFileContent);

        await _fileHandler.WriteFile(request.Path, outputFileContent);
      }

      return Unit.Value;
    }
  }

  [Noun("schema"), Verb("export")]
  public class SchemaExportRequest: BaseRequest
  {
    [Option("app", "application", required: true, ordanalityOrder: 1)] public string Application { get; set; }
    [Option("n", "name", required: true, ordanalityOrder: 2)] public string Name { get; set; }
    [Option("p", "path", required: true, ordanalityOrder: 3)] public string Path { get; set; }
    [Option("c", "alias-credentials")] public string AliasCredentials { get; set; }
  }
}