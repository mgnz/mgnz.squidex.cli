namespace MGNZ.Squidex.CLI.Common.CLI
{
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;

  using Serilog;
  using Serilog.Context;

  public class CommandLineOperationMapper : ICommandLineOperationMapper
  {
    private readonly ILogger _logger;
    private readonly IEnumerable<Noun> _nouns;

    public CommandLineOperationMapper(ILogger logger, IEnumerable<Noun> nouns)
    {
      _logger = logger;
      _nouns = nouns;
    }

    public (Noun noun, Verb verb) MapOperation(string args)
    {
      return MapOperation(args.Split(new[ ] {' '}));
    }

    public (Noun noun, Verb verb) MapOperation(string[ ] args)
    {
      using (LogContext.PushProperty("method", nameof(MapOperation)))
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

        _logger.Information("identified {@identifiedNoun}, {@identifiedVerb}", noun, verb);
        return (noun, verb);
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
  public interface ICommandLineOperationMapper
  {
    (Noun noun, Verb verb) MapOperation(string[] args);
    (Noun noun, Verb verb) MapOperation(string args);
  }
}