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

  public class SchemaDeleteHandler : BaseHandler<SchemaDeleteRequest>
  {
    private readonly IConsoleWriter _consoleWriter;

    public SchemaDeleteHandler(ILogger logger, IClientProxyFactory clientFactory, IConsoleWriter consoleWriter, IContainer container)
      : base(logger, clientFactory, container)
    {
      _consoleWriter = consoleWriter;
    }

    /// <inheritdoc />
    public override async Task<Unit> Handle(SchemaDeleteRequest request, CancellationToken cancellationToken)
    {
      var proxy = ClientFactory.GetClientProxy<ISquidexAppSchemaClient>(request.AliasCredentials);

      if (await SchemaExists(request.Application, request.Name))
      {
        await proxy.DeleteSchema(request.Application, request.Name);
      }
      else
      {
        // todo : log and throw not found
      }

      return Unit.Value;
    }
  }

  [Noun("schema"), Verb("delete")]
  public class SchemaDeleteRequest: BaseRequest
  {
    [Option("app", "application", required: true, ordanalityOrder: 1)] public string Application { get; set; }
    [Option("n", "name", required: true, ordanalityOrder: 2)] public string Name { get; set; }
    [Option("c", "alias-credentials")] public string AliasCredentials { get; set; }
  }
}