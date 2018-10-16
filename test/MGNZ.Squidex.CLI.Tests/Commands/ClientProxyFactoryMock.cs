namespace MGNZ.Squidex.CLI.Tests.Commands
{
  using System;
  using System.Linq.Expressions;

  using MGNZ.Squidex.CLI.Common.Commands;

  using Moq;

  public class ClientProxyFactoryMock
  {
    private Mock<IClientProxyFactory> _mock = null;

    public ClientProxyFactoryMock()
    {
      _mock = new Mock<IClientProxyFactory>();

      WithGetClientProxy = null;
      AndGetClientProxyThrows = null;
      AndGetClientProxyReturns = null;
    }

    public Expression<Func<IClientProxyFactory, object>> WithGetClientProxy { get; set; }
    public Exception AndGetClientProxyThrows { get; set; }
    public object AndGetClientProxyReturns { get; set; }

    public static Expression<Func<IClientProxyFactory, object>> WithGetClientProxyAsNoOp<TProxy>() where TProxy : class => (v) => v.GetClientProxy<TProxy>(It.IsAny<string>());
    public static Expression<Func<IClientProxyFactory, object>> WithGetClientProxyAs<TProxy>(string aliasCredentials) where TProxy : class => (v) => v.GetClientProxy<TProxy>(aliasCredentials);
    public static Exception AndGetClientProxyThrowsAsStandardError { get; private set; } = new ArgumentException();

    public IClientProxyFactory Build()
    {
      // handles : GetClientProxy
      if (WithGetClientProxy != null && AndGetClientProxyReturns != null)
        _mock.Setup(WithGetClientProxy).Returns(AndGetClientProxyReturns);
      if (WithGetClientProxy != null && AndGetClientProxyThrows != null)
        _mock.Setup(WithGetClientProxy).Throws(AndGetClientProxyThrows);
      else
        throw new ArgumentNullException();

      return _mock.Object;
    }
  }
}