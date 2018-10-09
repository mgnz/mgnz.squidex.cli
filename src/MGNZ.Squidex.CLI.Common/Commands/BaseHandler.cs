namespace MGNZ.Squidex.CLI.Common.Commands
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;

  using Autofac;

  using MediatR;

  using Serilog;

  public class BaseHandler<TRequest>: IRequestHandler<TRequest>
    where TRequest: IRequest
  {
    public BaseHandler(ILogger logger, IClientProxyFactory clientFactory, IContainer container)
    {
      Logger = logger;
      ClientFactory = clientFactory;
      Container = container;
    }

    protected ILogger Logger { get; private set; }
    public IClientProxyFactory ClientFactory { get; private set; }
    protected IContainer Container { get; private set; }

    /// <inheritdoc />
    public virtual Task<Unit> Handle(TRequest request, CancellationToken cancellationToken)
    {
      return Task.FromResult(new Unit());
    }
  }
}