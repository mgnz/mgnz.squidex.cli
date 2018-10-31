namespace MGNZ.Squidex.CLI.Tests.Platform
{
  using System;
  using System.Linq.Expressions;

  using Moq;

  public class Method<TImplementation>
    where TImplementation : class
  {
    public Expression<Action<TImplementation>> Call { get; set; }
    public Exception Throws { get; set; }

    public void Apply(ref Mock<TImplementation> mock)
    {
      var implementation = mock.Setup(Call);

      if (Throws != null)
        implementation.Throws(Throws);
    }
  }
}