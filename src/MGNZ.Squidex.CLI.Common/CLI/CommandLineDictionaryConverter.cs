namespace MGNZ.Squidex.CLI.Common.CLI
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;

  using Serilog;
  using Serilog.Context;

  public class CommandLineDictionaryConverter : ICommandLineDictionaryConverter
  {
    private readonly ILogger _logger;
    private readonly IEnumerable<Noun> _nouns;

    public CommandLineDictionaryConverter(ILogger logger, IEnumerable<Noun> nouns)
    {
      _logger = logger;
      _nouns = nouns;
    }

    public async Task<Dictionary<string, string>> Parse(string args)
    {
      return await Parse(args.Split(new[ ] {' '}));
    }

    public Task<Dictionary<string, string>> Parse(string[ ] args)
    {
      using (LogContext.PushProperty("method", nameof(Parse)))
      using (LogContext.PushProperty("args", args))
      {
        (Noun noun, Verb verb) = RetrieveNounAndVerb(args);

        var mappedArguments = new Dictionary<string, Option>();
        PopulateOptionValues(noun, verb, args, out mappedArguments);
      }

        // take first 2 options; these will always be either the noun or verb (or in reverse)

        // form there match up the rest of the arguments

        // throws on the simple validity check where the manditory fields are supplied; and no unexpected args

        //_logger.Debug("Debug");
        //_logger.Information("Information");
        //_logger.Warning("Warning");
        //_logger.Error("Error");
        //_logger.Fatal("Fatal");

        throw new NotImplementedException();
    }

    private (Noun noun, Verb verb) RetrieveNounAndVerb(string[] args)
    {
      Noun noun = null;
      Verb verb = null;

      using (LogContext.PushProperty("method", nameof(RetrieveNounAndVerb)))
      using (LogContext.PushProperty("args", args))
      {
        var candidateArguments = args.Take(2).ToArray();

        _logger.Information("found {@numberOfCandidateArguments}; specifically {@candidateArguments}", candidateArguments.Length, candidateArguments);

        foreach (var candidateArgument in candidateArguments)
        {
          foreach (var candidateNoun in _nouns)
          {
            if (candidateNoun.Named(candidateArgument))
            {
              noun = candidateNoun;

              break;
            }
          }
        }

        if (noun != null)
        {
          foreach (var candidateArgument in candidateArguments)
          {
            foreach (var candidateVerb in noun.Verbs.Values)
            {
              if (candidateVerb.IsNamed(candidateArgument))
              {
                verb = candidateVerb;
                break;
              }
            }
          }
        }

        _logger.Information("identified {@identifiedNoun}, {@identifiedVerb}", noun, verb);
        return (noun, verb);
      }
    }

    private void PopulateOptionValues(Noun noun, Verb verb, string[ ] args, out Dictionary<string, Option> mappedArguments)
    {
      using (LogContext.PushProperty("method", nameof(PopulateOptionValues)))
      using (LogContext.PushProperty("args", args))
      {
        mappedArguments = new Dictionary<string, Option>();

        var candidateArguments = args.Skip(2).ToArray();

        var ordinals = verb.GetOrdinalOptions;
        var parameterized = verb.GetParametrizedOptions;

        for (var i = 0; i < candidateArguments.Length; i++)
        {
          var argument = candidateArguments.ElementAtOrDefault(i);

          // check ordinal fields
          var ordinal = ordinals.ElementAtOrDefault(i);
          if (ordinal != null)
          {
            ordinal.Value = argument;
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
              i ++;
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
      }
    }

    //private void PopulateOptionValues(Noun noun, Verb verb, Dictionary<string, string> argumentDictionary, out Dictionary<string, Option> mappedArguments)
    //{
    //  using (LogContext.PushProperty("method", nameof(PopulateOptionValues)))
    //  using (LogContext.PushProperty("args", new { noun, verb, argumentDictionary }))
    //  {
    //    mappedArguments = new Dictionary<string, Option>();

    //    foreach (var candidateArgument in argumentDictionary)
    //    {
    //      var candidateOption = verb.GetOptionNamed(candidateArgument.Key);
    //      if (candidateOption != null)
    //      {
    //        candidateOption.Value = candidateOption.Value;
    //        mappedArguments.Add(candidateArgument.Key, candidateOption);
    //      }
    //      else
    //      {
    //        var outOfRangeException = new ArgumentOutOfRangeException(candidateArgument.Key, candidateArgument.Value, $"'{noun.GetDefaultName} {verb.GetDefaultName}' Doesn't understand an argument of '{candidateArgument.Key}'");
    //       _logger.Error(outOfRangeException, "'{@noun.GetDefaultName} {@verb.GetDefaultName}' Doesn't understand an argument of '{@candidateArgument.Key}'", noun.GetDefaultName, verb.GetDefaultName, verb.GetDefaultName, candidateArgument.Key);

    //        throw outOfRangeException;
    //      }
    //    }

    //    _logger.Information("identified {@mappedArguments}", mappedArguments);
    //  }
    //}
  }
}