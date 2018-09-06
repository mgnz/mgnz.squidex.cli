namespace MGNZ.Squidex.CLI.Common.CLI
{
  using System.Collections.Generic;
  using System.Reflection;

  using Serilog;

  public class RouteCommandOptionReflector : IReflectRouteCommandOptions
  {
    private readonly ILogger _logger;

    public RouteCommandOptionReflector(ILogger logger)
    {
      _logger = logger;
    }

    public IEnumerable<Noun> ReflectNouns(Assembly assembly)
    {

    }

    private IEnumerable<Verb> ReflectVerbs()
  }

  public interface IReflectRouteCommandOptions
  {

  }
}