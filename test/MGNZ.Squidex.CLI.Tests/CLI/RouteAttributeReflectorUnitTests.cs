namespace MGNZ.Squidex.CLI.Tests.CLI
{
  using System;
  using System.Collections.Generic;
  using System.Reflection;

  using FluentAssertions;

  using MediatR;

  using MGNZ.Squidex.CLI.Common.CLI;
  using MGNZ.Squidex.CLI.Common.Commands;
  using MGNZ.Squidex.CLI.Tests.Platform;

  using Xunit;

  public class RouteAttributeReflectorUnitTests
  {
    public static List<object[]> ReflectNouns_HappyPath_Data => new List<object[]>
    {
      // Dictionary<string, Tuple<NounAttribute, Type>>
      new object[ ]
      {
      }
    };

    [Theory(Skip = "not-implemented")]
    [MemberData(nameof(ReflectNouns_HappyPath_Data))]
    public void ReflectNouns_HappyPath(Dictionary<string, Tuple<NounAttribute, Type>> expectedResponse, Assembly inputAssembly)
    {

    }

    public static List<object[]> ReflectVerbs_HappyPath_Data => new List<object[]>
    {
      // Dictionary<string, Tuple<VerbAttribute, Type>>
      new object[ ]
      {
      }
    };

    [Theory(Skip = "not-implemented")]
    [MemberData(nameof(ReflectVerbs_HappyPath_Data))]
    public void ReflectVerbs_HappyPath(Dictionary<string, Tuple<VerbAttribute, Type>> expectedResponse, Assembly inputAssembly)
    {

    }

    public static List<object[]> ReflectOptions_HappyPath_Data => new List<object[]>
    {
      // Dictionary<string, Tuple<VerbAttribute, Type>>
      new object[ ]
      {
      }
    };

    [Theory(Skip = "not-implemented")]
    [MemberData(nameof(ReflectOptions_HappyPath_Data))]
    public void ReflectOptions_HappyPath(Dictionary<string, Tuple<OptionAttribute, Type>> expectedResponse, Type inputType)
    {

    }
  }
}
