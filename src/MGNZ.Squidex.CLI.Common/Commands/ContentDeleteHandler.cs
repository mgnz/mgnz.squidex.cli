namespace MGNZ.Squidex.CLI.Common.Commands
{
  using System.Threading;
  using System.Threading.Tasks;

  using Autofac;

  using MediatR;

  using Serilog.Core;

  public class ContentDeleteHandler : BaseHandler<ContentDeleteRequest>
  {
    public ContentDeleteHandler(Logger logger, IContainer container) : base(logger, container) { }

    /// <inheritdoc />
    public override async Task<Unit> Handle(ContentDeleteRequest request, CancellationToken cancellationToken)
    {
      return await base.Handle(request, cancellationToken);
    }
  }

  public class ContentDeleteRequest : IRequest<Unit>
  {
  }
}