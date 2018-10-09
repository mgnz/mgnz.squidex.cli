namespace MGNZ.Squidex.CLI.Tests.Commands
{
  using System;
  using System.Linq.Expressions;
  using System.Threading;
  using System.Threading.Tasks;

  using MediatR;

  using MGNZ.Squidex.CLI.Common.Commands;

  using Moq;

  public class BaseHandlerFixture
  {
    private Mock<ContentDeleteHandler> _mock;

    public BaseHandlerFixture()
    {
      _mock = new Mock<ContentDeleteHandler>();

      WithHandle = WithHandleAsNoOp;
      AndHandleThrows = null;
      AndHandleReturns = AndHandleReturnsDefault;
    }

    public Expression<Func<ContentDeleteHandler, Task<Unit>>> WithHandle { get; set; }
    public Exception AndHandleThrows { get; set; }
    public Task<Unit> AndHandleReturns { get; set; }

    public static Expression<Func<ContentDeleteHandler, Task<Unit>>> WithHandleAsNoOp { get; } = v => v.Handle(It.IsAny<ContentDeleteRequest>(), It.IsAny<CancellationToken>());

    public static Exception AndHandleThrowsAsStandardError { get; } = new ArgumentException();
    public static Task<Unit> AndHandleReturnsDefault { get; } = Task.FromResult(new Unit());

    public ContentDeleteHandler Mock()
    {
      // handles : Task<Unit> Handle(ContentDeleteRequest request, CancellationToken cancellationToken)
      if (AndHandleThrows != null)
        _mock.Setup(WithHandle).Throws(AndHandleThrows);
      else if (AndHandleReturns != null)
        _mock.Setup(WithHandle).Returns(AndHandleReturns);

      return _mock.Object;
    }

    public ContentDeleteHandler Build()
    {
      throw new NotImplementedException();
    }
  }
}