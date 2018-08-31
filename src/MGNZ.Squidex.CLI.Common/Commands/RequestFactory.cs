namespace MGNZ.Squidex.CLI.Common.Commands
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Reflection;

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

    public IRequest GetRequestForVerb(Noun noun)
    {
      var verbKeyValuePair = noun.Verbs.Single();
      var verb = verbKeyValuePair.Value;

      var instance = (IRequest)Activator.CreateInstance(verb.RequestType);

      var optionValues = verb.Options;
      var optionProperties = GetOptions(verb.RequestType);

      var updatedOptions = optionValues.Keys.Intersect(optionProperties.Keys);
      foreach (var updatedOption in updatedOptions)
      {
        var optionProperty = optionProperties[updatedOption];
        var optionValue = optionValues[updatedOption];

        optionProperty.Item2.SetValue(instance, optionValue.Value);
      }

      return instance;
    }

    private Dictionary<string, Tuple<OptionAttribute, PropertyInfo>> GetOptions(Type type)
    {
      var results = new Dictionary<string, Tuple<OptionAttribute, PropertyInfo>>();

      var properties = type.GetProperties();
      foreach (var property in properties)
      {
        var optionAttribute = property.GetCustomAttribute<OptionAttribute>();
        if (optionAttribute != null)
        {
          var longName = optionAttribute.LongName;
          results.Add(longName, new Tuple<OptionAttribute, PropertyInfo>(optionAttribute, property));
        }
      }

      return results;
    }

    private OptionAttribute[] GetOptionAttributes(Type t) => (OptionAttribute[])Attribute.GetCustomAttributes(t, typeof(OptionAttribute));
    private NounAttribute GetNounAttribute(Type t) => (NounAttribute)Attribute.GetCustomAttribute(t, typeof(NounAttribute));
    private VerbAttribute GetVerbAttribute(Type t) => (VerbAttribute)Attribute.GetCustomAttribute(t, typeof(VerbAttribute));
  }

  public interface IRequestFactory
  {
    IRequest GetRequestForVerb(Noun noun);
  }
}