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

  public class SchemaImportHandler : BaseHandler<SchemaImportRequest>
  {
    private readonly IFileHandler _fileHandler;

    public SchemaImportHandler(ILogger logger, IClientProxyFactory clientFactory, IFileHandler fileHandler, IContainer container) 
      : base(logger, clientFactory, container)
    {
      _fileHandler = fileHandler;
    }

    /// <inheritdoc />
    public override async Task<Unit> Handle(SchemaImportRequest request, CancellationToken cancellationToken)
    {
      var proxy = ClientFactory.GetClientProxy<ISquidexAppSchemaClient>(request.AliasCredentials);
      dynamic inputFileContent = _fileHandler.ReadFile(request.Path);

      if (await SchemaExists(request.Application, request.Name))
      {
        var deleteResult = await proxy.DeleteSchema(request.Application, request.Name);
        ThrowSchemaDeleteFailure(deleteResult);
      }

      var createResult = await proxy.CreateSchema(request.Application, inputFileContent);
      ThrowSchemaCreateFailure(createResult);

      var publishResult = await proxy.PublishSchema(request.Application, request.Name);
      ThrowSchemaPublishCreateFailure(publishResult);

      return Unit.Value;
    }
  }

  [Noun("schema"), Verb("import")]
  public class SchemaImportRequest: BaseRequest
  {
    [Option("app", "application", required: true, ordanalityOrder: 1)] public string Application { get; set; }
    [Option("n", "name", required: true, ordanalityOrder: 2)] public string Name { get; set; }
    [Option("p", "path", required: true, ordanalityOrder: 3)] public string Path { get; set; }
    [Option("c", "alias-credentials")] public string AliasCredentials { get; set; }
  }
}