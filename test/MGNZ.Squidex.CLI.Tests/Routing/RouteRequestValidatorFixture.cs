namespace MGNZ.Squidex.CLI.Tests.Routing
{
  using System;
  using System.Linq.Expressions;

  using MGNZ.Squidex.CLI.Common.Commands;
  using MGNZ.Squidex.CLI.Common.Routing;
  using MGNZ.Squidex.CLI.Tests.Platform;

  using Moq;

  internal class RouteRequestValidatorFixture
  {
    private Mock<IValidateRouteRequests> _mock = null;

    public RouteRequestValidatorFixture()
    {
      _mock = new Mock<IValidateRouteRequests>();

      WithValidate = WithValidateAsNoOp;
      AndValidateThrows = null;
    }

    public Expression<Action<IValidateRouteRequests>> WithValidate { get; set; }
    public Exception AndValidateThrows { get; set; }

    public static Expression<Action<IValidateRouteRequests>> WithValidateAsNoOp { get; private set; } = (v) => v.Validate(It.IsAny<string[ ]>(), It.IsAny<BaseRequest>());
    public static Exception AndValidateThrowsAsStandardError { get; private set; } = new ArgumentException();

    public IValidateRouteRequests Mock()
    {
      // handles : Validate
      if (AndValidateThrows == null)
        _mock.Setup(WithValidate);
      else
        _mock.Setup(WithValidate).Throws(AndValidateThrows);

      return _mock.Object;
    }

    public IValidateRouteRequests Build()
    {
      return new RouteRequestValidator(SerilogFixture.UsefullLogger<RouteRequestValidator>());
    }
  }
}