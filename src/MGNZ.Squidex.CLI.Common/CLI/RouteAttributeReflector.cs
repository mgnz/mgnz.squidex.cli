namespace MGNZ.Squidex.CLI.Common.CLI
{
  using System;
  using System.Collections.Generic;
  using System.Reflection;

  using Serilog;
  using Serilog.Context;

  public class RouteAttributeReflector : IReflectRouteAttributes
  {
    private readonly ILogger _logger;

    public RouteAttributeReflector(ILogger logger)
    {
      _logger = logger;
    }

    public Dictionary<string, Tuple<NounAttribute, Type>> ReflectNouns(Assembly assembly)
    {
      var results = new Dictionary<string, Tuple<NounAttribute, Type>>();

      using (LogContext.PushProperty("method", nameof(ReflectNouns)))
      using (LogContext.PushProperty("args", assembly))
      {
        var types = assembly.GetTypes();
        foreach (var type in types)
        {
          var attribute = type.GetCustomAttribute<NounAttribute>(inherit: true);
          if (attribute != null)
          {
            var longName = attribute.GetDefaultName;
            results.Add(longName, new Tuple<NounAttribute, Type>(attribute, type));
          }
        }

        return results;
      }
    }

    public Dictionary<string, Tuple<VerbAttribute, Type>> ReflectVerbs(Assembly assembly)
    {
      var results = new Dictionary<string, Tuple<VerbAttribute, Type>>();

      using (LogContext.PushProperty("method", nameof(ReflectNouns)))
      using (LogContext.PushProperty("args", assembly))
      {
        var types = assembly.GetTypes();
        foreach (var type in types)
        {
          var attribute = type.GetCustomAttribute<VerbAttribute>(inherit:true);
          if (attribute != null)
          {
            var longName = attribute.GetDefaultName;
            results.Add(longName, new Tuple<VerbAttribute, Type>(attribute, type));
          }
        }

        return results;
      }
    }

    public Dictionary<string, Tuple<OptionAttribute, PropertyInfo>> ReflectOptions(Type type)
    {
      var results = new Dictionary<string, Tuple<OptionAttribute, PropertyInfo>>();

      using (LogContext.PushProperty("method", nameof(ReflectNouns)))
      using (LogContext.PushProperty("args", type))
      {
        var properties = type.GetProperties();
        foreach (var property in properties)
        {
          var attribute = property.GetCustomAttribute<OptionAttribute>();
          if (attribute != null)
          {
            var longName = attribute.LongName;
            results.Add(longName, new Tuple<OptionAttribute, PropertyInfo>(attribute, property));
          }
        }

        return results;
      }
    }
  }

  public interface IReflectRouteAttributes
  {
    Dictionary<string, Tuple<NounAttribute, Type>> ReflectNouns(Assembly assembly);
    Dictionary<string, Tuple<VerbAttribute, Type>> ReflectVerbs(Assembly assembly);
    Dictionary<string, Tuple<OptionAttribute, PropertyInfo>> ReflectOptions(Type type);
  }
}