namespace MGNZ.Squidex.CLI.Common.Routing
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  using Serilog;
  using Serilog.Context;

  public class RouteOptionsParser : IParseRouteOptions
  {
    private readonly ILogger _logger;

    public RouteOptionsParser(ILogger logger)
    {
      _logger = logger;
    }

    public void ParseAndPopulateOptions(ref Noun noun, string[ ] args)
    {
      using (LogContext.PushProperty("method", nameof(ParseAndPopulateOptions)))
      using (LogContext.PushProperty("args", new { noun, args }))
      {
        var verbKeyValuePair = noun.Verbs.Single();
        var verb = verbKeyValuePair.Value;

        var candidateArguments = args.Skip(2).ToArray();

        _logger.Information("found {@numberOfCandidateArguments}; specifically {@candidateArguments}", candidateArguments.Length, candidateArguments);

        var ordinals = verb.GetOrdinalOptions;
        var parameterized = verb.GetParametrizedOptions;

        for (var i = 0; i < candidateArguments.Length; i++)
        {
          var argument = candidateArguments.ElementAtOrDefault(i);
          // todo nullcheck return

          // check ordinal fields
          var ordinal = ordinals.ElementAtOrDefault(i);
          if (ordinal != null)
          {
            var ordinalLikelyValue = string.Empty;

            // has the ordinal been named
            if (ordinal.IsNamed(argument))
            {
              ordinalLikelyValue = candidateArguments.ElementAtOrDefault(i + 1);
              i++;
            }
            else
            {
              ordinalLikelyValue = argument;
            }

            ordinal.Value = ordinalLikelyValue;

            // todo log found

            continue;
          }

          // now parametrized
          var optionNamed = verb.GetOptionNamed(argument);
          var optionLikelyValue = candidateArguments.ElementAtOrDefault(i + 1);
          if (optionNamed != null)
          {
            if (optionLikelyValue != null)
            {
              optionNamed.Value = optionLikelyValue;
              i++;
            }

            continue;
          }
          else
          {
            var outOfRangeException = new ArgumentOutOfRangeException(argument, optionLikelyValue, $"'{noun.GetDefaultName} {verb.GetDefaultName}' Doesn't understand an argument of '{argument}'");
            _logger.Error(outOfRangeException, "'{@noun.GetFullNameFormatted} {@verb.GetFullNameFormatted}' Doesn't understand an argument of '{@argument}'", noun.GetDefaultName, verb.GetDefaultName, verb.GetDefaultName, argument);

            throw outOfRangeException;
          }
        }
      }
    }
  }

  public interface IParseRouteOptions
  {
    /// <summary>
    /// Gets a Noun with a Verb that has been processed; and populates the necessary Options form the remaining Command Line
    /// </summary>
    /// <param name="noun"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    void ParseAndPopulateOptions(ref Noun noun, string[] args);
  }
}