namespace MGNZ.Squidex.CLI.Tests.CLI
{
  using System;
  using System.Collections.Generic;
  using System.Reflection;

  using FluentAssertions;

  using MGNZ.Squidex.CLI.Common.CLI;
  using MGNZ.Squidex.CLI.Common.Commands;
  using MGNZ.Squidex.CLI.Tests.Platform;

  using Xunit;

  public class RouteMetadataBuilderUnitTests
  {
    public static List<object[ ]> ReflectNouns_HappyPath_Data => new List<object[ ]>
    {
      // Dictionary<string, Tuple<NounAttribute, Type>>
      new object[ ]
      {
        new Dictionary<string, Noun>
        {
          { "noun1", new AppNoun() }
        },
        typeof(BaseVerbReference1).Assembly
      }
    };

    [Theory]
    [MemberData(nameof(ReflectNouns_HappyPath_Data))]
    public void ReflectNouns_HappyPath(Dictionary<string, Tuple<NounAttribute, Type>> expectedResponse, Assembly inputAssembly)
    {
      var routeAttributeReflector = new RouteAttributeReflector(SerilogFixture.UsefullLogger<RouteAttributeReflector>());
      var sut = new RouteMetadataBuilder(SerilogFixture.UsefullLogger<RouteMetadataBuilder>(), routeAttributeReflector);
      var actualResponse = sut.GetNounMetadata();

      actualResponse.Keys.Should().Contain(expectedResponse.Keys);

      foreach (var expected in expectedResponse)
      {
        var actual = actualResponse[expected.Key];

        actual.Item1.Should().BeEquivalentTo(expected.Value.Item1);
      }
    }
  }
}