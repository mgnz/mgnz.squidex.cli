namespace MGNZ.Squidex.CLI.Common.CLI
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  using Serilog;
  using Serilog.Context;

  public class RouteCommandsParser : IParseRouteCommands
  {
    private readonly ILogger _logger;
    private readonly IEnumerable<Noun> _nouns;

    public RouteCommandsParser(ILogger logger, IEnumerable<Noun> nouns)
    {
      _logger = logger;
      _nouns = nouns;
    }

    public void
      ParseAndPopulateOperation(out Noun nounVerbCombination, string args)
    {
      ParseAndPopulateOperation(out nounVerbCombination, args.Split(new[] { ' ' }));
    }

    public void ParseAndPopulateOperation(out Noun nounVerbCombination, string[] args)
    {
      using (LogContext.PushProperty("method", nameof(ParseAndPopulateOperation)))
      using (LogContext.PushProperty("args", args))
      {
        Noun noun = null;
        Verb verb = null;

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

        if (noun == null || verb == null)
        {
          var outOfRangeException = new ArgumentOutOfRangeException("nounVerbPair", string.Join(",", candidateArguments), $"Cannot identify either a Noun or a Verb; what we got was '{string.Join(" ", candidateArguments)}'");
          _logger.Error(outOfRangeException, "Cannot identify either a Noun or a Verb; what we got was '{candidateArguments}'", string.Join(" ", candidateArguments));

          throw outOfRangeException;
        }
        else
        {
          _logger.Information("identified {@identifiedNoun}, {@identifiedVerb}", noun, verb);

          noun.Verbs.Clear();
          noun.Verbs.Add(verb.GetDefaultName, verb);
          nounVerbCombination = noun;
        }
      }
    }
  }

  /// <summary>
  /// Takes a CLI argumnet for an input; returns a KV dictionary in the following form
  /// - Noun
  /// - Action
  /// - K,V[] { parameter, value }
  /// Actual deserialization into a HandlerRequest happens at a later stage; all this
  /// is interested in is getting the chunks out; so suppose it could be called the chunkifier
  /// </summary>
  public interface IParseRouteCommands
  {
    void ParseAndPopulateOperation(out Noun nounVerbCombination, string args);
    void ParseAndPopulateOperation(out Noun nounVerbCombination, string[] args);
  }
}