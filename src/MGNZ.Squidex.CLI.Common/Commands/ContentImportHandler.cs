namespace MGNZ.Squidex.CLI.Common.Commands
{
  using System.Threading;
  using System.Threading.Tasks;

  using Autofac;

  using MediatR;

  using Serilog.Core;

  public class ContentImportHandler : BaseHandler<ContentImportRequest>
  {
    public ContentImportHandler(Logger logger, IContainer container) : base(logger, container) { }
    /// <inheritdoc />
    public override async Task<Unit> Handle(ContentImportRequest request, CancellationToken cancellationToken)
    {
      return await base.Handle(request, cancellationToken);
    }
  }

  public class ContentImportRequest : IRequest<Unit>
  {
  }
}