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

  [Trait("category", "unit")]
  public class RouteMetadataBuilderUnitTests
  {
    public static List<object[ ]> GetMetadata_HappyPath_Data => new List<object[ ]>
    {
      // Dictionary<string, Noun>
      new object[ ]
      {
        new Dictionary<string, Noun>
        {
          {
            "noun1", new Noun()
            {
              Verbs = new Dictionary<string, Verb>()
              {
                { "verb1", new Verb() { Options = new Dictionary<string, Option>()
                    {
                        { "-a|--option-a", new Option() { ShortName = "a", LongName = "option-a", Required = true, OrdanalityOrder = 1 } },
                        { "-b|--option-b", new Option() { ShortName = "b", LongName = "option-b", OrdanalityOrder = 0 } }
                    }
                  }
                }
              }
            }
          },
          {
            "noun2", new Noun()
            {
              Verbs = new Dictionary<string, Verb>()
              {
                { "verb2", new Verb() { Options = new Dictionary<string, Option>()
                    {
                      { "-a|--option-a", new Option() { ShortName = "a", LongName = "option-a", Required = true, OrdanalityOrder = 1 } },
                      { "-b|--option-b", new Option() { ShortName = "b", LongName = "option-b", OrdanalityOrder = 0  } }
                    }
                  }
                },
                { "verb3", new Verb() { Options = new Dictionary<string, Option>()
                    {
                      { "-a|--option-a", new Option() { ShortName = "a", LongName = "option-a", Required = true, OrdanalityOrder = 1 } },
                      { "-b|--option-b", new Option() { ShortName = "b", LongName = "option-b", OrdanalityOrder = 0  } }
                    }
                  }
                }
              }
            }
          }
        },
        typeof(BaseVerbReference1).Assembly
      }
    };

    [Theory()]
    [MemberData(nameof(GetMetadata_HappyPath_Data))]
    public void ReflectNouns_HappyPath(Dictionary<string, Noun> expectedResponse, Assembly inputAssembly)
    {
      var routeAttributeReflector = new RouteAttributeReflector(SerilogFixture.UsefullLogger<RouteAttributeReflector>());
      var sut = new RouteMetadataBuilder(SerilogFixture.UsefullLogger<RouteMetadataBuilder>(), routeAttributeReflector);
      var actualResponse = sut.GetMetadata(inputAssembly);

      actualResponse.Keys.Should().Contain(expectedResponse.Keys);

      foreach (var expectedKvp in expectedResponse)
      {
        var actualNoun = actualResponse[expectedKvp.Key];

        actualNoun.Verbs.Should().NotBeNull();
        actualNoun.Verbs.Keys.Should().Contain(expectedKvp.Value.Verbs.Keys);

        foreach (var expectedVerbKvp in expectedKvp.Value.Verbs)
        {
          var actualVerb = actualNoun.Verbs[expectedVerbKvp.Key];

          actualVerb.Options.Should().NotBeNull();
          actualVerb.Options.Keys.Should().Contain(expectedVerbKvp.Value.Options.Keys);

          foreach (var expectedOptionKvp in expectedVerbKvp.Value.Options)
          {
            var actualOption = actualVerb.Options[expectedOptionKvp.Key];

            expectedOptionKvp.Value.Should().BeEquivalentTo(actualOption);
          }
        }
      }
    }
  }
}