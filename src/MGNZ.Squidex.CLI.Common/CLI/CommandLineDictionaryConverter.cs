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

    private (Noun noun, Verb verb) RetrieveNounAndVerb(string[ ] args)
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

              _logger.Debug("found {@noun}", noun);
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
              if (candidateVerb.Named(candidateArgument))
              {
                verb = candidateVerb;
                _logger.Debug("found {@verb}", verb);
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
}