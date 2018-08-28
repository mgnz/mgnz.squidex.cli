namespace MGNZ.Squidex.CLI.Common.CLI
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  using Serilog;
  using Serilog.Context;

  public class CommandLineParameterMapper : ICommandLineParameterMapper
  {
    private readonly ILogger _logger;

    public CommandLineParameterMapper(ILogger logger)
    {
      _logger = logger;
    }

    public Dictionary<string, Option> MapParameters(Noun noun, Verb verb, string args)
    {
      return MapParameters(noun, verb, args.Split(new[] { ' ' }));
    }

    public Dictionary<string, Option> MapParameters(Noun noun, Verb verb, string[ ] args)
    {
      using (LogContext.PushProperty("method", nameof(MapParameters)))
      using (LogContext.PushProperty("args", new { noun, verb, args }))
      {
        var mappedArguments = new Dictionary<string, Option>();

        var candidateArguments = args.Skip(2).ToArray();

        _logger.Information("found {@numberOfCandidateArguments}; specifically {@candidateArguments}", candidateArguments.Length, candidateArguments);

        var ordinals = verb.GetOrdinalOptions;
        var parameterized = verb.GetParametrizedOptions;

        for (var i = 0; i < candidateArguments.Length; i++)
        {
          var argument = candidateArguments.ElementAtOrDefault(i);
          // nullcheck return

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
            mappedArguments.Add(ordinal.GetLongNameFormatted, ordinal);

            // log found

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

            mappedArguments.Add(optionNamed.GetLongNameFormatted, optionNamed);

            continue;
          }
          else
          {
            var outOfRangeException = new ArgumentOutOfRangeException(argument, optionLikelyValue, $"'{noun.GetDefaultName} {verb.GetDefaultName}' Doesn't understand an argument of '{argument}'");
            _logger.Error(outOfRangeException, "'{@noun.GetDefaultName} {@verb.GetDefaultName}' Doesn't understand an argument of '{@argument}'", noun.GetDefaultName, verb.GetDefaultName, verb.GetDefaultName, argument);

            throw outOfRangeException;
          }
        }

        _logger.Information("identified {@mappedArguments}", mappedArguments);

        foreach (var kvp in verb.Options)
        {
          if (mappedArguments.ContainsKey(kvp.Value.GetLongNameFormatted)) continue;

          mappedArguments.Add(kvp.Value.GetLongNameFormatted, kvp.Value);
        }

        return mappedArguments;
      }
    }
  }
}