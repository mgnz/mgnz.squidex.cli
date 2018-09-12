namespace MGNZ.Squidex.CLI.Tests.CLI
{
  using System;
  using System.Collections.Generic;
  using System.Reflection;

  using FluentAssertions;

  using MGNZ.Squidex.CLI.Common.CLI;
  using MGNZ.Squidex.CLI.Tests.Platform;

  using Xunit;

  public class RouteAttributeReflectorUnitTests
  {
    public static List<object[]> ReflectNouns_HappyPath_Data => new List<object[]>
    {
      // Dictionary<string, Tuple<NounAttribute, Type>>
      new object[ ]
      {
        new Dictionary<string, Tuple<NounAttribute, Type>> ()
        {
          {"noun1", new Tuple<NounAttribute, Type>(new NounAttribute("noun1") {}, typeof(ReferenceA))},
          {"noun2", new Tuple<NounAttribute, Type>(new NounAttribute("noun2") {}, typeof(BaseNounReference1))}
        },
        typeof(BaseVerbReference1).Assembly
      }
    };

    [Theory]
    [MemberData(nameof(ReflectNouns_HappyPath_Data))]
    public void ReflectNouns_HappyPath(Dictionary<string, Tuple<NounAttribute, Type>> expectedResponse, Assembly inputAssembly)
    {
      var sut = new RouteAttributeReflector(SerilogFixture.UsefullLogger<RouteAttributeReflector>());
      var actualResponse = sut.ReflectNouns(inputAssembly);

      actualResponse.Keys.Should().Contain(expectedResponse.Keys);

      foreach (var expected in expectedResponse)
      {
        var actual = actualResponse[expected.Key];

        actual.Item1.Should().BeEquivalentTo(expected.Value.Item1);
      }
    }

    public static List<object[]> ReflectVerbs_HappyPath_Data => new List<object[]>
    {
      // Dictionary<string, Tuple<VerbAttribute, Type>>
      new object[ ]
      {
        new Dictionary<string, Tuple<VerbAttribute, Type>> ()
        {
          {"verb1", new Tuple<VerbAttribute, Type>(new VerbAttribute("verb1") {}, typeof(ReferenceA))},
          {"verb2", new Tuple<VerbAttribute, Type>(new VerbAttribute("verb2") {}, typeof(BaseVerbReference1))}
        },
        typeof(BaseVerbReference1).Assembly
      }
    };

    [Theory]
    [MemberData(nameof(ReflectVerbs_HappyPath_Data))]
    public void ReflectVerbs_HappyPath(Dictionary<string, Tuple<VerbAttribute, Type>> expectedResponse, Assembly inputAssembly)
    {
      var sut = new RouteAttributeReflector(SerilogFixture.UsefullLogger<RouteAttributeReflector>());
      var actualResponse = sut.ReflectVerbs(inputAssembly);

      actualResponse.Keys.Should().Contain(expectedResponse.Keys);

      foreach (var expected in expectedResponse)
      {
        var actual = actualResponse[expected.Key];

        actual.Item1.Should().BeEquivalentTo(expected.Value.Item1);
      }
    }

    public static List<object[]> ReflectOptions_HappyPath_Data => new List<object[]>
    {
      // Dictionary<string, Tuple<OptionAttribute, PropertyInfo>>
      new object[ ]
      {
        new Dictionary<string, Tuple<OptionAttribute, PropertyInfo>> ()
        {
          {"option-a", new Tuple<OptionAttribute, PropertyInfo>(new OptionAttribute("a", "option-a", required: true, ordanalityOrder: 1) {}, null)}, 
          {"option-b", new Tuple<OptionAttribute, PropertyInfo>(new OptionAttribute("b", "option-b") {}, null)}
        },
        typeof(BaseVerbReference1)
      },
      new object[ ]
      {
        new Dictionary<string, Tuple<OptionAttribute, PropertyInfo>> ()
        {
          {"option-a", new Tuple<OptionAttribute, PropertyInfo>(new OptionAttribute("a", "option-a", required: true, ordanalityOrder: 1) {}, null)},
          {"option-b", new Tuple<OptionAttribute, PropertyInfo>(new OptionAttribute("b", "option-b") {}, null)}
        },
        typeof(ReferenceA)
      }
    };

    [Theory]
    [MemberData(nameof(ReflectOptions_HappyPath_Data))]
    public void ReflectOptions_HappyPath(Dictionary<string, Tuple<OptionAttribute, PropertyInfo>> expectedResponse, Type inputType)
    {
      var sut = new RouteAttributeReflector(SerilogFixture.UsefullLogger<RouteAttributeReflector>());
      var actualResponse = sut.ReflectOptions(inputType);

      actualResponse.Keys.Should().Contain(expectedResponse.Keys);

      foreach (var expected in expectedResponse)
      {
        var actual = actualResponse[expected.Key];

        actual.Item1.Should().BeEquivalentTo(expected.Value.Item1);
      }
    }
  }
}
