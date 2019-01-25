namespace MGNZ.Squidex.CLI.Common.Routing
{
  using System;
  using System.Collections.Generic;
  using System.Reflection;

  using Serilog;
  using Serilog.Context;

  public class RouteMetadataBuilder : IBuildRouteMetadata
  {
    private readonly ILogger _logger;
    private readonly IReflectRouteAttributes _attributeReflector;

    public RouteMetadataBuilder(ILogger logger, IReflectRouteAttributes attributeReflector)
    {
      _logger = logger;
      _attributeReflector = attributeReflector;
    }

    public Dictionary<string, Noun> GetMetadata(Assembly assembly)
    {
      var results = new Dictionary<string, Noun>();

      using (LogContext.PushProperty("method", nameof(GetMetadata)))
      {
        return GetNounMetadata(assembly);
      }
    }

    public Dictionary<string, Option> GetOptionMetadata(Type containingType)
    {
      var reuslts = new Dictionary<string, Option>();

      var optionKvps = _attributeReflector.ReflectOptions(containingType);
      foreach (var optionKvp in optionKvps)
      {
        var currentOptionAttribute = optionKvp.Value.attribute;
        var currentOptionPropertyInfo = optionKvp.Value.property;

        var currentOption = new Option()
        {
          ShortName = currentOptionAttribute.ShortName,
          LongName = currentOptionAttribute.LongName,
          Required = currentOptionAttribute.Required,
          OrdanalityOrder = currentOptionAttribute.OrdanalityOrder,
          HelpText = currentOptionAttribute.HelpText,
          PropertyInfo = currentOptionPropertyInfo
        };

        reuslts.Add(currentOption.LongName, currentOption);
      }

      return reuslts;
    }

    public Dictionary<string, Verb> GetVerbMetadata(Assembly assembly, string nounName = null)
    {
      var reuslts = new Dictionary<string, Verb>();

      var verbKvps = _attributeReflector.ReflectVerbs(assembly, nounName);
      foreach (var verbKvp in verbKvps)
      {
        var currentVerbAttribute = verbKvp.Value.VerbAttribute;
        var currentVerbType = verbKvp.Value.Type;

        var currentVerb = new Verb()
        {
          Names = currentVerbAttribute.Names,
          RequestType = currentVerbType
        };

        currentVerb.Options = GetOptionMetadata(currentVerbType);

        reuslts.Add(currentVerb.GetDefaultName, currentVerb);
      }

      return reuslts;
    }

    public Dictionary<string, Noun> GetNounMetadata(Assembly assembly, string named = null)
    {
      var results = new Dictionary<string, Noun>();

      var nounKvps = _attributeReflector.ReflectNouns(assembly);
      foreach (var nounKvp in nounKvps)
      {
        var currentNounAttribute = nounKvp.Value.NounAttribute;
        var currentNounType = nounKvp.Value.Type;

        var currentNoun = new Noun()
        {
          Names = currentNounAttribute.Names
        };

        currentNoun.Verbs = GetVerbMetadata(assembly, currentNounAttribute.GetDefaultName);

        results.Add(currentNoun.GetDefaultName, currentNoun);
      }

      return results;
    }
  }

  public interface IBuildRouteMetadata
  {
    Dictionary<string, Noun> GetMetadata(Assembly assembly);
    Dictionary<string, Option> GetOptionMetadata(Type containingType);
    Dictionary<string, Verb> GetVerbMetadata(Assembly assembly, string nounName = null);
    Dictionary<string, Noun> GetNounMetadata(Assembly assembly, string named = null);
  }
}