namespace MGNZ.Squidex.CLI.Common.Routing
{
  using System;
  using System.Linq;

  using MGNZ.Squidex.CLI.Common.Commands;

  using Serilog;

  public class RouteRequestValidator : IValidateRouteRequests
  {
    private readonly ILogger _logger;

    public RouteRequestValidator(ILogger logger)
    {
      _logger = logger;
    }

    public void Validate(string[ ] arguments, BaseRequest request)
    {
      var validity = request.Validate();
      if (validity.Any(v => v.isValid == false))
      {
        var reasons = validity.TakeWhile(f => f.isValid == false).Select(s => $"- {s.property}: {s.invalidReason}").ToList();
        var message = $"The request to route the command '{string.Join(" ", arguments)}' into a {request.GetType().Name} failed because due to the following reasons{Environment.NewLine}{string.Join(Environment.NewLine, reasons)}";
        var error = new ArgumentException(message);

        _logger.Error(error, "The request to route the {@arguments} into a {@request} failed due to the following {@reasons}", arguments, request, reasons);
        throw error;
      }
    }
  }

  public interface IValidateRouteRequests
  {
    void Validate(string[] arguments, BaseRequest request);
  }
}