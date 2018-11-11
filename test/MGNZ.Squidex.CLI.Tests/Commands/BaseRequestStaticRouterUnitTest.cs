namespace MGNZ.Squidex.CLI.Tests.Commands
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;

  using Bogus;

  using FluentAssertions;

  using MGNZ.Squidex.CLI.Common.Commands;
  using MGNZ.Squidex.CLI.Common.Routing;
  using MGNZ.Squidex.CLI.Tests.Platform;
  using MGNZ.Squidex.CLI.Tests.Routing;

  using Xunit;

  public class BaseRequestStaticRouterUnitTest
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

    protected static IEnumerable<object[]> BuildSchemaImportData(string nounKey, string verbKey = null)
    {
      var faker = new Faker();

      IEnumerable<Verb> verbs = null;
      if (string.IsNullOrEmpty(verbKey) || string.IsNullOrWhiteSpace(verbKey))
        verbs = RoutingMetadata.Value[nounKey].Verbs.Select(kvp => kvp.Value);
      else
        verbs = new Verb[] { RoutingMetadata.Value[nounKey].Verbs[verbKey] };

      foreach (var verb in verbs)
        for (var i = 0; i < 100; i++)
          yield return BuildVerb(nounKey, verb, faker);
    }

    private static object[] BuildVerb(string nounKey, Verb verb, Faker faker)
    {
      var outputExpectedRequest = (BaseRequest)Activator.CreateInstance(verb.RequestType);

      foreach (var option in verb.GetOrdinalOptions) SetValue(outputExpectedRequest, option, faker);
      foreach (var option in faker.Random.ListItems(verb.GetParametrizedOptions)) SetValue(outputExpectedRequest, option, faker);

      var inputCommandLine = GetCommandLine(nounKey, verb, faker);

      return new object[]
      {
        outputExpectedRequest,
        inputCommandLine
      };
    }

    private static string GetCommandLine(string nounKey, Verb verb, Faker faker)
    {
      var stringBuilder = new StringBuilder();

      stringBuilder.Append($"{nounKey} {verb.GetDefaultName} ");

      foreach (var option in verb.GetOrdinalOptions)
        stringBuilder.Append($"{option.Value} ");

      foreach (var option in verb.GetParametrizedOptions.SkipWhile(o => o.HasValue == false))
        stringBuilder.Append(MakeCommandLineElement(option, faker));

      return stringBuilder.ToString().Trim();
    }

    private static string MakeCommandLineElement(Option option, Faker faker)
    {
      var operation = faker.Random.ArrayElement(new[] { option.GetShortNameFormatted, option.GetLongNameFormatted });

      // todo : mess with the CLI a bit introducing some random spaces

      return $"{operation} {option.Value} ";
    }

    private static void SetValue(BaseRequest instance, Option option, Faker faker)
    {
      var value = GetValue(option, faker);

      option.Value = value;
      option.PropertyInfo.SetValue(instance, value);
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