namespace MGNZ.Squidex.CLI.Common.Routing
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
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

    public Dictionary<string, NounAttribute> ReflectNouns(Assembly assembly)
    {
      var results = new Dictionary<string, NounAttribute>();

      using (LogContext.PushProperty("method", nameof(ReflectNouns)))
      using (LogContext.PushProperty("args", assembly))
      {
        var types = assembly.GetTypes();
        foreach (var type in types)
        {
          var attribute = type.GetCustomAttribute<NounAttribute>(inherit: false);
          if (attribute != null)
          {
            var longName = attribute.GetDefaultName;
            if(! results.ContainsKey(longName))
              results.Add(longName, attribute);
          }
        }

        return results;
      }
    }

    public Dictionary<string, Tuple<VerbAttribute, Type>> ReflectVerbs(Assembly assembly, string nounName = null)
    {
      var results = new Dictionary<string, Tuple<VerbAttribute, Type>>();

      using (LogContext.PushProperty("method", nameof(ReflectVerbs)))
      using (LogContext.PushProperty("args", assembly))
      {
        var types = assembly.GetTypes();
        foreach (var type in types)
        {
          var attributeTypePairs = TypeHasAttributes(type, typeof(VerbAttribute), typeof(NounAttribute)).ToList();
          if (attributeTypePairs.All(p => p.isApplied == true))
          {
            var nounAttribute = attributeTypePairs.SingleOrDefault(p => p.attribute is NounAttribute).attribute as NounAttribute;
            var verbAttribute = attributeTypePairs.SingleOrDefault(p => p.attribute is VerbAttribute).attribute as VerbAttribute;

            if(nounAttribute == null || verbAttribute == null) break;

            // given an asspcicatedNounName
            // == then accumulate the verb if the DefaultNounName of the noun matches; else skip it 
            // == otherwise accumulate the verb

            if ((string.IsNullOrWhiteSpace(nounName) || string.IsNullOrEmpty(nounName)) == false && nounAttribute.IsNamed(nounName))
            {
              var defaultName = verbAttribute.GetDefaultName;
              results.Add(defaultName, new Tuple<VerbAttribute, Type>(verbAttribute, type));
            }
            else if ((string.IsNullOrWhiteSpace(nounName) || string.IsNullOrEmpty(nounName)) == true)
            {
              var defaultName = verbAttribute.GetDefaultName;
              results.Add(defaultName, new Tuple<VerbAttribute, Type>(verbAttribute, type));
            }
          }
        }

        return results;
      }
    }

    public IEnumerable<(Type candidate, Attribute attribute, bool isApplied)> TypeHasAttributes(Type type, params Type[] attributes)
    {
      foreach (var candidate in attributes)
      {
        var attribute = type.GetCustomAttribute(candidate, inherit: true);

        yield return (candidate, attribute, attribute != null);
      }
    }

    public Dictionary<string, Tuple<OptionAttribute, PropertyInfo>> ReflectOptions(Type type)
    {
      var results = new Dictionary<string, Tuple<OptionAttribute, PropertyInfo>>();

      using (LogContext.PushProperty("method", nameof(ReflectOptions)))
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
    Dictionary<string, NounAttribute> ReflectNouns(Assembly assembly);
    Dictionary<string, Tuple<VerbAttribute, Type>> ReflectVerbs(Assembly assembly, string nounName = null);
    Dictionary<string, Tuple<OptionAttribute, PropertyInfo>> ReflectOptions(Type type);
  }
}