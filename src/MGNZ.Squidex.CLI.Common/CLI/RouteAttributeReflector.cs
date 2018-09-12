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

    public Dictionary<string, Tuple<VerbAttribute, Type>> ReflectVerbs(Assembly assembly, NounAttribute pairedWith = null)
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
            if (pairedWith == null)
            {
              var longName = attribute.GetDefaultName;
              results.Add(longName, new Tuple<VerbAttribute, Type>(attribute, type));
            }
            else
            {
              // only returns the verbs that are paired with the noun we specify

              var matchedNoun = type.GetCustomAttribute<NounAttribute>(inherit: true);
              if (matchedNoun != null && matchedNoun.GetDefaultName.Equals(pairedWith.GetDefaultName))
              {
                var longName = attribute.GetDefaultName;
                results.Add(longName, new Tuple<VerbAttribute, Type>(attribute, type));
              }
            }
          }
        }

        return results;
      }
    }

    public IEnumerable<Tuple<Attribute, Type>> ReflectAttributes(Assembly assembly, params Attribute[] attributes)
    {
      using (LogContext.PushProperty("method", nameof(ReflectNouns)))
      using (LogContext.PushProperty("args", assembly))
      {
        var types = assembly.GetTypes();
        foreach (var type in types)
        {
          foreach (var candidate in attributes)
          {
            var attribute = type.GetCustomAttribute(candidate.GetType(), inherit: true);
            if (attribute != null)
            {
              yield return new Tuple<Attribute, Type>(attribute, type);
            }
          }
        }
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
    Dictionary<string, Tuple<VerbAttribute, Type>> ReflectVerbs(Assembly assembly, NounAttribute pairedWith = null);
    Dictionary<string, Tuple<OptionAttribute, PropertyInfo>> ReflectOptions(Type type);
  }
}