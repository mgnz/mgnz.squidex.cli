namespace MGNZ.Squidex.CLI.Common.Routing
{
  using System.Collections.Generic;

  using MGNZ.Squidex.CLI.Common.Commands;

  using Serilog;

  public class StaticCommandLineRouter : ICommandLineRouter
  {
    private readonly IParseRouteCommands _commandParser;
    private readonly ILogger _logger;
    private readonly IParseRouteOptions _optionParser;
    private readonly IValidateRouteRequests _requestValidator;
    private readonly IBuildRouteRequests _routeBuilder;

    public StaticCommandLineRouter(ILogger logger, IParseRouteCommands commandParser, IParseRouteOptions optionParser,
      IBuildRouteRequests routeBuilder, IValidateRouteRequests requestValidator)
    {
      _logger = logger;
      _commandParser = commandParser;
      _optionParser = optionParser;
      _routeBuilder = routeBuilder;
      _requestValidator = requestValidator;
    }

    public BaseRequest GetOne(IEnumerable<Noun> cachedNouns, string[ ] arguments)
    {
      var nounVerbCombination = _commandParser.ParseAndPopulateOperation(cachedNouns, arguments);
      _optionParser.ParseAndPopulateOptions(ref nounVerbCombination, arguments);
      var request = _routeBuilder.GetRequestForVerb(nounVerbCombination);

      _requestValidator.Validate(arguments, request);

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