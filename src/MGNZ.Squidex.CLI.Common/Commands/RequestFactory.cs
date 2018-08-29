namespace MGNZ.Squidex.CLI.Common.Commands
{
  using System;

  using MediatR;

  using MGNZ.Squidex.CLI.Common.CLI;

  using Serilog;

  public class RequestFactory : IRequestFactory
  {
    private readonly ILogger _logger;

    public RequestFactory(ILogger logger)
    {
      _logger = logger;
    }

    public IRequest GetRequestForVerb(Verb verb)
    {
      var instance = Activator.CreateInstance(verb.RequestType) as IRequest;

      // https://docs.microsoft.com/en-us/dotnet/standard/attributes/retrieving-information-stored-in-attributes
      // https://docs.microsoft.com/en-us/dotnet/framework/reflection-and-codedom/accessing-custom-attributes
      // 


      return instance;
    }
  }

  public interface IRequestFactory
  {
    IRequest GetRequestForVerb(Verb verb);
  }
}