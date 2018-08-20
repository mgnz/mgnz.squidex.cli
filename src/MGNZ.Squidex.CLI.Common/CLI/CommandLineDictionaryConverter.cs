namespace MGNZ.Squidex.CLI.Common.CLI
{
  using System;
  using System.Collections.Generic;
  using System.Threading.Tasks;

  using Serilog;

  public class CommandLineDictionaryConverter : ICommandLineDictionaryConverter
  {
    private readonly ILogger _logger;

    public CommandLineDictionaryConverter(ILogger logger)
    {
      _logger = logger;
    }

    public Task<Dictionary<string, string>> Parse(string[ ] args)
    {
      // take first 2 options; these will always be either the noun or verb (or in reverse)

      // form there match up the rest of the arguments

      // throws on the simple validity check where the manditory fields are supplied; and no unexpected args

      throw new NotImplementedException();
    }
  }
}