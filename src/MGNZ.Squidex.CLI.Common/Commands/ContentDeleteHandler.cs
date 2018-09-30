namespace MGNZ.Squidex.CLI.Common.Commands
{
  using System.Threading;
  using System.Threading.Tasks;

  using Autofac;

  using MediatR;

  using MGNZ.Squidex.CLI.Common.CLI;

  using Serilog;

  public class ContentDeleteHandler : BaseHandler<ContentDeleteRequest>
  {
    public ContentDeleteHandler(ILogger logger, IContainer container) : base(logger, container) { }

    /// <inheritdoc />
    public override async Task<Unit> Handle(ContentDeleteRequest request, CancellationToken cancellationToken)
    {
      return await base.Handle(request, cancellationToken);
    }
  }

  [Noun("content"), Verb("delete")]
  public class ContentDeleteRequest: BaseRequest
  {
    [Option("sc", "schema", required: true, ordanalityOrder: 1)] public string Schema { get; set; }
    [Option("id", "id", required: true, ordanalityOrder: 2)] public string Id { get; set; }
    [Option("c", "alias-credentials")] public string AliasCredentials { get; set; }
  }
}