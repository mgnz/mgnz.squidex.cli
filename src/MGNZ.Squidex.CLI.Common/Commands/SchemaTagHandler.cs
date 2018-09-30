namespace MGNZ.Squidex.CLI.Common.Commands
{
  using System.Threading;
  using System.Threading.Tasks;

  using Autofac;

  using MediatR;

  using MGNZ.Squidex.CLI.Common.CLI;

  using Serilog;

  public class SchemaTagHandler : BaseHandler<SchemaTagRequest>
  {
    public SchemaTagHandler(ILogger logger, IContainer container) : base(logger, container)
    {
    }

    /// <inheritdoc />
    public override async Task<Unit> Handle(SchemaTagRequest request, CancellationToken cancellationToken)
    {
      return await base.Handle(request, cancellationToken);
    }
  }

  [Noun("schema"), Verb("tag")]
  public class SchemaTagRequest: BaseRequest
  {
    [Option("n", "name", required: true, ordanalityOrder: 1)] public string Name { get; set; }
    [Option("t", "tags", required: true, ordanalityOrder: 2)] public string Tags { get; set; }
    [Option("c", "alias-credentials")] public string AliasCredentials { get; set; }
  }
}