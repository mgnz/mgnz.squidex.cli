namespace MGNZ.Squidex.CLI.Tests.Platform
{
  using System;
  using System.Linq.Expressions;

  using Moq;
  using Moq.Language.Flow;

  public class Function<TImplementation, TReturns>
    where TImplementation : class
  {
    public Expression<Func<TImplementation, TReturns>> Call { get; set; }

    public TReturns Returns { get; set; }
    public Exception Throws { get; set; }

    public void Apply(ref Mock<TImplementation> mock)
    {
      ISetup<TImplementation, TReturns> implementation = null;

      if (Call == null) return;

      implementation = mock.Setup(Call);
      if (Returns != null)
        implementation.Returns(Returns);
      else if (Throws != null)
        implementation.Throws(Throws);
    }
  }
}