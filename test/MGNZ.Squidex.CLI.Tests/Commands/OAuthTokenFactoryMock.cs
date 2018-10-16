namespace MGNZ.Squidex.CLI.Tests.Commands
{
  using System;
  using System.Linq.Expressions;
  using System.Threading.Tasks;

  using MGNZ.Squidex.CLI.Common.Commands;

  using Moq;

  public class OAuthTokenFactoryMock
  {
    private Mock<IOAuthTokenFactory> _mock = null;

    public OAuthTokenFactoryMock()
    {
      _mock = new Mock<IOAuthTokenFactory>();

      WithGetOrRefreshOAuthToken = null;
      AndGetOrRefreshOAuthTokenThrows = null;
      AndGetOrRefreshOAuthTokenReturns = null;
    }

    public Expression<Func<IOAuthTokenFactory, Task<string>>> WithGetOrRefreshOAuthToken { get; set; }
    public Exception AndGetOrRefreshOAuthTokenThrows { get; set; }
    public Task<string> AndGetOrRefreshOAuthTokenReturns { get; set; }

    public static Expression<Func<IOAuthTokenFactory, Task<string>>> WithGetOrRefreshOAuthTokenAsNoOp() => (v) => v.GetOrRefreshOAuthToken(It.IsAny<string>());
    public static Expression<Func<IOAuthTokenFactory, Task<string>>> WithGetOrRefreshOAuthTokenAs(string aliasCredentials) => (v) => v.GetOrRefreshOAuthToken(aliasCredentials);
    public static Exception AndGetOrRefreshOAuthTokenThrowsAsStandardError { get; private set; } = new ArgumentException();

    public IOAuthTokenFactory Build()
    {
      // handles : GetOrRefreshOAuthToken
      if (WithGetOrRefreshOAuthToken != null && AndGetOrRefreshOAuthTokenReturns != null)
        _mock.Setup(WithGetOrRefreshOAuthToken).Returns(AndGetOrRefreshOAuthTokenReturns);
      if (WithGetOrRefreshOAuthToken != null && AndGetOrRefreshOAuthTokenThrows != null)
        _mock.Setup(WithGetOrRefreshOAuthToken).Throws(AndGetOrRefreshOAuthTokenThrows);
      else
        throw new ArgumentNullException();

      return _mock.Object;
    }
  }
}