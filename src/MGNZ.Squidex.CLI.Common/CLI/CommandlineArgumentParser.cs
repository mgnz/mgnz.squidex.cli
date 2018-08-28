namespace MGNZ.Squidex.CLI.Common.CLI
{
  using System;
  using System.Collections.Generic;
  using System.Threading.Tasks;

  using Serilog;

  public class CommandlineArgumentParser
  {
    private readonly ILogger _logger;
    private readonly ICommandLineOperationMapper _operationMapper;
    private readonly ICommandLineParameterMapper _parameterMapper;

    public CommandlineArgumentParser(ILogger logger, ICommandLineOperationMapper operationMapper, ICommandLineParameterMapper parameterMapper)
    {
      _logger = logger;
      _operationMapper = operationMapper;
      _parameterMapper = parameterMapper;
    }

    public async Task Parse(string args) => await Parse(args.Split(new[] { ' ' }, StringSplitOptions.None)).ConfigureAwait(false);

    public async Task Parse(string[ ] args)
    {
      // pull out operation (verb, noun pair)

      // pull out options

      // validate the operation and options that have been set

      throw new NotImplementedException();
    }
  }
}