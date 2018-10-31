namespace MGNZ.Squidex.CLI.Common.Commands
{
  using System.Threading;
  using System.Threading.Tasks;

  using Autofac;

  using MediatR;

  using MGNZ.Squidex.Client;
  using MGNZ.Squidex.CLI.Common.Routing;

  using Serilog;

  public class SchemaDeleteHandler : BaseHandler<SchemaDeleteRequest>
  {
    public SchemaDeleteHandler(ILogger logger, IClientProxyFactory clientFactory, IContainer container)
      : base(logger, clientFactory, container)
    {

    }

    /// <inheritdoc />
    public override async Task<Unit> Handle(SchemaDeleteRequest request, CancellationToken cancellationToken)
    {
      var proxy = ClientFactory.GetClientProxy<ISquidexAppSchemaClient>(request.AliasCredentials);

      if (await SchemaExists(request.Application, request.Name))
      {
        var deleteResult = await proxy.DeleteSchema(request.Application, request.Name);
        ThrowSchemaDeleteFailure(deleteResult);
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