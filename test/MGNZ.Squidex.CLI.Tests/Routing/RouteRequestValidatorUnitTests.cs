namespace MGNZ.Squidex.CLI.Tests.Routing
{
  using System.Collections.Generic;

  using Xunit;

  [Trait("category", "unit")]
  public class RouteRequestValidatorUnitTests
  {
    public static List<object[ ]> Validate_HappyPath_Data => new List<object[ ]>
    {
      new object[ ]
      {

      }
    };

    [Theory]
    [MemberData(nameof(Validate_HappyPath_Data))]
    public void Validate_HappyPath()
    {

    }
  }
}