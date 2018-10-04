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

    public RouteCommandsParser(ILogger logger)
    {
      _logger = logger;
    }

    public Noun ParseAndPopulateOperation(IEnumerable<Noun> allNouns, string[] args)
    {
      Noun nounVerbCombination = null;

      using (LogContext.PushProperty("method", nameof(ParseAndPopulateOperation)))
      using (LogContext.PushProperty("args", args))
      {
        Verb candidateVerb = null;

        var candidateArguments = args.Take(2).ToArray();

        _logger.Information("found {@numberOfCandidateArguments}; specifically {@candidateArguments}", candidateArguments.Length, candidateArguments);

        var candidateNoun = FindCandidateNoun(allNouns, candidateArguments);

        if (candidateNoun != null)
        {
          candidateVerb = ExtractCandidateVerb(candidateArguments, candidateNoun);
        }

        if (candidateNoun == null || candidateVerb == null)
        {
          var outOfRangeException = new ArgumentOutOfRangeException("nounVerbPair", string.Join(",", candidateArguments), $"Cannot identify either a Noun or a Verb; what we got was '{string.Join(" ", candidateArguments)}'");
          _logger.Error(outOfRangeException, "Cannot identify either a Noun or a Verb; what we got was '{candidateArguments}'", string.Join(" ", candidateArguments));

          throw outOfRangeException;
        }
        else
        {
          _logger.Information("identified {@identifiedNoun}, {@identifiedVerb}", candidateNoun, candidateVerb);

          candidateNoun.Verbs.Clear();
          candidateNoun.Verbs.Add(candidateVerb.GetDefaultName, candidateVerb);
          nounVerbCombination = candidateNoun;
        }
      }

      return nounVerbCombination;
    }

    private static Verb ExtractCandidateVerb(string[ ] candidateArguments, Noun candidateNoun)
    {
      foreach (var candidateArgument in candidateArguments)
      {
        foreach (var candidateVerb in candidateNoun.Verbs.Values)
        {
          if (candidateVerb.IsNamed(candidateArgument))
          {
            return candidateVerb;
          }
        }
      }

      return null;
    }

    private static Noun FindCandidateNoun(IEnumerable<Noun> nouns, string[ ] candidateArguments)
    {
      foreach (var candidateArgument in candidateArguments)
      {
        foreach (var candidateNoun in nouns)
        {
          if (candidateNoun.Named(candidateArgument))
          {
            return candidateNoun;
          }
        }
      }

      return null;
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
    Noun ParseAndPopulateOperation(IEnumerable<Noun> allNouns, string[] args);
  }
}