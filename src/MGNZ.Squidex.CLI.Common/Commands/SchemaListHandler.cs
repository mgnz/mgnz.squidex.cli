namespace MGNZ.Squidex.CLI.Common.Commands
{
  using System.Threading;
  using System.Threading.Tasks;

  using Autofac;

  using MediatR;

  using MGNZ.Squidex.Client;
  using MGNZ.Squidex.CLI.Common.Routing;

  using Serilog;

  public class SchemaListHandler : BaseHandler<SchemaListRequest>
  {
    public SchemaListHandler(ILogger logger, IClientProxyFactory clientFactory, IContainer container)
      : base(logger, clientFactory, container)
    {

    }

    /// <inheritdoc />
    public override async Task<Unit> Handle(SchemaListRequest request, CancellationToken cancellationToken)
    {
      var proxy = ClientFactory.GetClientProxy<ISquidexAppSchemaClient>(request.AliasCredentials);

      return await base.Handle(request, cancellationToken);
    }
  }

  [Noun("schema"), Verb("list")]
  public class SchemaListRequest: BaseRequest
  {
    [Option("app", "application", required: true, ordanalityOrder: 1)] public string Application { get; set; }
    [Option("c", "alias-credentials")] public string AliasCredentials { get; set; }
  }
}