namespace MGNZ.Squidex.CLI.Common.CLI
{
  using System;
  using System.Threading.Tasks;

  using Serilog;

  public class CommandlineArgumentParser
  {
    private readonly ILogger _logger;
    private readonly ICommandLineDictionaryConverter _dictionaryConverter;

    public CommandlineArgumentParser(ILogger logger, ICommandLineDictionaryConverter dictionaryConverter)
    {
      _logger = logger;
      _dictionaryConverter = dictionaryConverter;
    }

    public async Task Parse(string args) => await Parse(args.Split(new[] { ' ' }, StringSplitOptions.None)).ConfigureAwait(false);

    public async Task Parse(string[ ] args)
    {
      throw new NotImplementedException();
    }
  }
}