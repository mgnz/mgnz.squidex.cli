namespace MGNZ.Squidex.CLI.Common.CLI
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Reflection;

  using Serilog;

  public class RouteAttributeReflector : IReflectRouteAttributes
  {
    private readonly ILogger _logger;

    public RouteAttributeReflector(ILogger logger)
    {
      _logger = logger;
    }

    public Dictionary<string, Tuple<NounAttribute, Type>> ReflectNouns(Assembly assembly)
    {
      throw new NotImplementedException();
    }

    public Dictionary<string, Tuple<VerbAttribute, Type>> ReflectVerbs(Assembly assembly)
    {
      throw new NotImplementedException();
    }

    public Dictionary<string, Tuple<OptionAttribute, PropertyInfo>> ReflectOptions(Type type)
    {
      throw new NotImplementedException();
    }
  }

  public interface IReflectRouteAttributes
  {
    Dictionary<string, Tuple<NounAttribute, Type>> ReflectNouns(Assembly assembly);
    Dictionary<string, Tuple<VerbAttribute, Type>> ReflectVerbs(Assembly assembly);
    Dictionary<string, Tuple<OptionAttribute, PropertyInfo>> ReflectOptions(Type type);
  }
}