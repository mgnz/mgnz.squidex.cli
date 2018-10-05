namespace MGNZ.Squidex.CLI.Common.Commands
{
  using System.Threading;
  using System.Threading.Tasks;

  using Autofac;

  using MediatR;

  using MGNZ.Squidex.CLI.Common.Routing;

  using Serilog;

  public class SchemaDeleteHandler : BaseHandler<SchemaDeleteRequest>
  {
    public SchemaDeleteHandler(ILogger logger, IContainer container) : base(logger, container) { }

    /// <inheritdoc />
    public override async Task<Unit> Handle(SchemaDeleteRequest request, CancellationToken cancellationToken)
    {
      return await base.Handle(request, cancellationToken);
    }
  }

  [Noun("schema"), Verb("delete")]
  public class SchemaDeleteRequest: BaseRequest
  {
    [Option("n", "name", required: true, ordanalityOrder: 1)] public string Name { get; set; }
    [Option("c", "alias-credentials")] public string AliasCredentials { get; set; }
  }
}