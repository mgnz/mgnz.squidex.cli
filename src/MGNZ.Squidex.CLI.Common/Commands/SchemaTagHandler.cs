namespace MGNZ.Squidex.CLI.Common.Commands
{
  using System.Threading;
  using System.Threading.Tasks;

  using Autofac;

  using MediatR;

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

  public class SchemaTagRequest : IRequest<Unit>
  {
  }
}