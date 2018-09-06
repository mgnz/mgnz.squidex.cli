namespace MGNZ.Squidex.CLI.Common.Commands
{
  using System.Threading;
  using System.Threading.Tasks;

  using Autofac;

  using MediatR;

  using MGNZ.Squidex.CLI.Common.CLI;

  using Serilog;

  public class SchemaImportHandler : BaseHandler<SchemaImportRequest>
  {
    public SchemaImportHandler(ILogger logger, IContainer container) : base(logger, container) { }

    /// <inheritdoc />
    public override async Task<Unit> Handle(SchemaImportRequest request, CancellationToken cancellationToken)
    {
      return await base.Handle(request, cancellationToken);
    }
  }

  [Noun("schema"), Verb("import")]
  public class SchemaImportRequest : IRequest
  {
    [Option("n", "name", required: true, ordanalityOrder: 1)] public string Name { get; set; }
    [Option("p", "path", required: true, ordanalityOrder: 2)] public string Path { get; set; }
    [Option("c", "alias-credentials")] public string AliasCredentials { get; set; }
  }
}