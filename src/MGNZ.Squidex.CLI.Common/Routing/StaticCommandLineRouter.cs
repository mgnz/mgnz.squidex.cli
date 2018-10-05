namespace MGNZ.Squidex.CLI.Common.Routing
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  using MGNZ.Squidex.CLI.Common.Commands;

  using Serilog;

  public class StaticCommandLineRouter : ICommandLineRouter
  {
    private readonly ILogger _logger;
    private readonly IParseRouteCommands _commandParser;
    private readonly IParseRouteOptions _optionParser;
    private readonly IBuildRouteRequests _routeBuilder;

    public StaticCommandLineRouter(ILogger logger, IParseRouteCommands commandParser, IParseRouteOptions optionParser, IBuildRouteRequests routeBuilder)
    {
      _logger = logger;
      _commandParser = commandParser;
      _optionParser = optionParser;
      _routeBuilder = routeBuilder;
    }

    public BaseRequest GetOne(IEnumerable<Noun> cachedNouns, string[ ] arguments)
    {
      var nounVerbCombination = _commandParser.ParseAndPopulateOperation(cachedNouns, arguments);
      _optionParser.ParseAndPopulateOptions(ref nounVerbCombination, arguments);
      var request = _routeBuilder.GetRequestForVerb(nounVerbCombination);

      var validity = request.Validate();
      if (validity.Any(v => v.isValid == false))
      {
        var reasons = validity.TakeWhile(f => f.isValid == false).Select(s => $"- {s.property}: {s.invalidReason}").ToList();
        var message = $"The request to route the command '{string.Join(" ", arguments)}' into a {request.GetType().Name} failed because due to the following reasons{Environment.NewLine}{string.Join(Environment.NewLine,reasons)}";
        var error = new ArgumentException(message);

        _logger.Error(error, "The request to route the {@arguments} into a {@request} failed due to the following {@reasons}", arguments, request, reasons);
        throw error;
      }

      return request;
    }
  }

  public class InteractiveCommandLineRouter : ICommandLineRouter
  {
    private readonly ILogger _logger;

    public InteractiveCommandLineRouter(ILogger logger)
    {
      _logger = logger;
    }
  }

  public interface ICommandLineRouter
  {

  }
}