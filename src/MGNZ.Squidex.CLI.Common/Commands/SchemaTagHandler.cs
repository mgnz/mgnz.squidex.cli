namespace MGNZ.Squidex.CLI.Common.Commands
{
  using System.Threading;
  using System.Threading.Tasks;

  using Autofac;

  using MediatR;

  using MGNZ.Squidex.Client;
  using MGNZ.Squidex.CLI.Common.Routing;

  using Serilog;

  public class SchemaTagHandler : BaseHandler<SchemaTagRequest>
  {
    public SchemaTagHandler(ILogger logger, IClientProxyFactory clientFactory, IContainer container)
      : base(logger, clientFactory, container)
    {

    }

    /// <inheritdoc />
    public override async Task<Unit> Handle(SchemaTagRequest request, CancellationToken cancellationToken)
    {
      var proxy = ClientFactory.GetClientProxy<ISquidexAppSchemaClient>(request.AliasCredentials);

      return await base.Handle(request, cancellationToken);
    }
  }

  [Noun("schema"), Verb("tag")]
  public class SchemaTagRequest: BaseRequest
  {
    [Option("app", "application", required: true, ordanalityOrder: 1)] public string Application { get; set; }
    [Option("n", "name", required: true, ordanalityOrder: 2)] public string Name { get; set; }
    [Option("t", "tags", required: true, ordanalityOrder: 3)] public string Tags { get; set; }
    [Option("c", "alias-credentials")] public string AliasCredentials { get; set; }
  }
}