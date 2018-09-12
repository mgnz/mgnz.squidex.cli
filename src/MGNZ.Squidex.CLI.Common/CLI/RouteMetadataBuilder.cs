namespace MGNZ.Squidex.CLI.Common.CLI
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
        //var nounKvps = _attributeReflector.ReflectNouns(referenceAssembly);
        //foreach (var nounKvp in nounKvps)
        //{
        //  var currentNounAttribute = nounKvp.Value.Item1;

        //  var currentNoun = new Noun()
        //  {
        //    Names = currentNounAttribute.Names
        //  };

        //  //var verbKvps = _attributeReflector.ReflectVerbs(typeof(IBuildRouteMetadata).Assembly, currentNounAttribute);
        //  //foreach (var verbKvp in verbKvps)
        //  //{
        //  //  var currentVerbAttribute = verbKvp.Value.Item1;
        //  //  var currentVerbType = verbKvp.Value.Item2;

        //  //  var currentVerb = new Verb()
        //  //  {
        //  //    Names = currentVerbAttribute.Names,
        //  //    RequestType = currentVerbType
        //  //  };

        //  //  //var optionKvps = _attributeReflector.ReflectOptions(currentVerbType);
        //  //  //foreach (var optionKvp in optionKvps)
        //  //  //{
        //  //  //  var currentOptionAttribute = optionKvp.Value.Item1;
        //  //  //  var currentOptionPropertyInfo = optionKvp.Value.Item2;

        //  //  //  var currentOption = new Option()
        //  //  //  {
        //  //  //    ShortName = currentOptionAttribute.ShortName,
        //  //  //    LongName = currentOptionAttribute.LongName,
        //  //  //    Separator = currentOptionAttribute.Seperator,
        //  //  //    Required = currentOptionAttribute.Required,
        //  //  //    OrdanalityOrder = currentOptionAttribute.OrdanalityOrder,
        //  //  //    HelpText = currentOptionAttribute.HelpText
        //  //  //  };

        //  //  //  currentVerb.Options.Add(currentOption.GetFullNameFormatted, currentOption);
        //  //  //}

        //  //  currentVerb.Options = GetOptionMetadata(currentVerbType);

        //  //  currentNoun.Verbs.Add(currentVerb.GetDefaultName, currentVerb);
        //  //}

        //  currentNoun.Verbs = GetVerbMetadata(referenceAssembly, currentNounAttribute);

        //  results.Add(currentNoun.GetDefaultName, currentNoun);
        //}

        return GetNounMetadata(assembly);
      }
    }

    public Dictionary<string, Option> GetOptionMetadata(Type containingType)
    {
      var reuslts = new Dictionary<string, Option>();

      var optionKvps = _attributeReflector.ReflectOptions(containingType);
      foreach (var optionKvp in optionKvps)
      {
        var currentOptionAttribute = optionKvp.Value.Item1;
        var currentOptionPropertyInfo = optionKvp.Value.Item2;

        var currentOption = new Option()
        {
          ShortName = currentOptionAttribute.ShortName,
          LongName = currentOptionAttribute.LongName,
          Separator = currentOptionAttribute.Seperator,
          Required = currentOptionAttribute.Required,
          OrdanalityOrder = currentOptionAttribute.OrdanalityOrder,
          HelpText = currentOptionAttribute.HelpText
        };

        reuslts.Add(currentOption.GetFullNameFormatted, currentOption);
      }

      return reuslts;
    }

    public Dictionary<string, Verb> GetVerbMetadata(Assembly assembly, NounAttribute pairedWith = null)
    {
      var reuslts = new Dictionary<string, Verb>();

      var verbKvps = _attributeReflector.ReflectVerbs(assembly, pairedWith);
      foreach (var verbKvp in verbKvps)
      {
        var currentVerbAttribute = verbKvp.Value.Item1;
        var currentVerbType = verbKvp.Value.Item2;

        var currentVerb = new Verb()
        {
          Names = currentVerbAttribute.Names,
          RequestType = currentVerbType
        };

        //var optionKvps = _attributeReflector.ReflectOptions(currentVerbType);
        //foreach (var optionKvp in optionKvps)
        //{
        //  var currentOptionAttribute = optionKvp.Value.Item1;
        //  var currentOptionPropertyInfo = optionKvp.Value.Item2;

        //  var currentOption = new Option()
        //  {
        //    ShortName = currentOptionAttribute.ShortName,
        //    LongName = currentOptionAttribute.LongName,
        //    Separator = currentOptionAttribute.Seperator,
        //    Required = currentOptionAttribute.Required,
        //    OrdanalityOrder = currentOptionAttribute.OrdanalityOrder,
        //    HelpText = currentOptionAttribute.HelpText
        //  };

        //  currentVerb.Options.Add(currentOption.GetFullNameFormatted, currentOption);
        //}

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
        var currentNounAttribute = nounKvp.Value.Item1;

        var currentNoun = new Noun()
        {
          Names = currentNounAttribute.Names
        };

        //var verbKvps = _attributeReflector.ReflectVerbs(typeof(IBuildRouteMetadata).Assembly, currentNounAttribute);
        //foreach (var verbKvp in verbKvps)
        //{
        //  var currentVerbAttribute = verbKvp.Value.Item1;
        //  var currentVerbType = verbKvp.Value.Item2;

        //  var currentVerb = new Verb()
        //  {
        //    Names = currentVerbAttribute.Names,
        //    RequestType = currentVerbType
        //  };

        //  //var optionKvps = _attributeReflector.ReflectOptions(currentVerbType);
        //  //foreach (var optionKvp in optionKvps)
        //  //{
        //  //  var currentOptionAttribute = optionKvp.Value.Item1;
        //  //  var currentOptionPropertyInfo = optionKvp.Value.Item2;

        //  //  var currentOption = new Option()
        //  //  {
        //  //    ShortName = currentOptionAttribute.ShortName,
        //  //    LongName = currentOptionAttribute.LongName,
        //  //    Separator = currentOptionAttribute.Seperator,
        //  //    Required = currentOptionAttribute.Required,
        //  //    OrdanalityOrder = currentOptionAttribute.OrdanalityOrder,
        //  //    HelpText = currentOptionAttribute.HelpText
        //  //  };

        //  //  currentVerb.Options.Add(currentOption.GetFullNameFormatted, currentOption);
        //  //}

        //  currentVerb.Options = GetOptionMetadata(currentVerbType);

        //  currentNoun.Verbs.Add(currentVerb.GetDefaultName, currentVerb);
        //}

        currentNoun.Verbs = GetVerbMetadata(assembly, currentNounAttribute);

        results.Add(currentNoun.GetDefaultName, currentNoun);
      }

      return results;
    }
  }

  public interface IBuildRouteMetadata
  {
    Dictionary<string, Noun> GetNounMetadata();
  }
}