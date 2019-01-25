namespace MGNZ.Squidex.CLI.Tests.Commands.CommandRouting
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;

  using Bogus;

  using MGNZ.Squidex.CLI.Common.Commands;
  using MGNZ.Squidex.CLI.Common.Routing;
  using MGNZ.Squidex.CLI.Tests.Platform;

  public class BaseCommandRoutingUnitTest
  {
    public static Lazy<Dictionary<string, Noun>> RoutingMetadata => new Lazy<Dictionary<string, Noun>>(LoadRoutingMetadata);
    private static Dictionary<string, Noun> LoadRoutingMetadata()
    {
      var inputAssembly = typeof(BaseRequest).Assembly;
      var routeAttributeReflector = new RouteAttributeReflector(SerilogFixture.UsefullLogger<RouteAttributeReflector>());
      var sut = new RouteMetadataBuilder(SerilogFixture.UsefullLogger<RouteMetadataBuilder>(), routeAttributeReflector);
      var actualResponse = sut.GetMetadata(inputAssembly);

      return actualResponse;
    }

    protected static List<object[]> BuildSchemaImportData(string nounKey, string verbKey = null)
    {
      var faker = new Faker();

      IEnumerable<Verb> verbs = null;
      if (string.IsNullOrEmpty(verbKey) || string.IsNullOrWhiteSpace(verbKey))
        verbs = RoutingMetadata.Value[nounKey].Verbs.Select(kvp => kvp.Value);
      else
        verbs = new Verb[] { RoutingMetadata.Value[nounKey].Verbs[verbKey] };

      var set = new List<object[ ]>();
      foreach (var verb in verbs)
        for (var i = 0; i < 100; i++)
          set.Add(BuildVerb(nounKey, verb, faker));

      return set;
    }

    private static object[] BuildVerb(string nounKey, Verb verb, Faker faker)
    {
      var outputExpectedRequest = (BaseRequest)Activator.CreateInstance(verb.RequestType);

      var commandLineBuilder = new StringBuilder();
      commandLineBuilder.Append($"{nounKey} {verb.GetDefaultName} ");

      foreach (var option in verb.Options.Values) SetValue(ref commandLineBuilder, outputExpectedRequest, option, faker);

      var inputCommandLine = commandLineBuilder.ToString().Trim();

      return new object[]
      {
        outputExpectedRequest,
        inputCommandLine
      };
    }

    private static void SetValue(ref StringBuilder commandLineBuilder, BaseRequest instance, Option option, Faker faker)
    {
      if (option.IsOrdinal || faker.Random.Bool())
      {
        var value = GetValue(option, faker);

        option.Value = value;
        option.PropertyInfo.SetValue(instance, value);

        if (option.IsOrdinal)
        {
          commandLineBuilder.Append($"{value} ");
        }
        else
        {
          var operation = faker.Random.ArrayElement(new[] { option.GetShortNameFormatted, option.GetLongNameFormatted });
          commandLineBuilder.Append($"{operation} {value} ");
        }
      }
    }

    private static string GetValue(Option parametised, Faker faker)
    {
      switch (parametised.PropertyInfo.Name)
      {
        default:
          return $"in_{parametised.PropertyInfo.Name}";
          //return faker.Random.AlphaNumeric(faker.Random.Number(5, 50));
      }
    }
  }
}