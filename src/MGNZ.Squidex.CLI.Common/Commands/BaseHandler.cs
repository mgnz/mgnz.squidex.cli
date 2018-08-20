namespace MGNZ.Squidex.CLI.Common.Commands
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;

  using Autofac;

  using MediatR;

  using Serilog.Core;

  public class BaseHandler<TRequest> : IRequestHandler<TRequest>
    where TRequest : IRequest<Unit>
  {
    public BaseHandler(Logger logger, IContainer container)
    {
      Logger = logger;
      Container = container;
    }

    protected Logger Logger { get; }
    protected IContainer Container { get; }

    /// <inheritdoc />
    public virtual Task<Unit> Handle(TRequest request, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }
  }
}