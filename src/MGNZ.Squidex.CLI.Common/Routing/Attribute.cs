namespace MGNZ.Squidex.CLI.Common.Routing
{
  using System;
  using System.Collections.Generic;
  using System.Diagnostics;
  using System.Linq;
  using System.Reflection;

  // name of a thing
  [DebuggerDisplay("{GetDefaultName}")]
  public class Noun
  {
    public string[ ] Names { get; set; }
    public string GetDefaultName => Names.First();

    public bool Named(string name)
    {
      return Names.Contains(name);
    }

    public Dictionary<string, Verb> Verbs { get; set; }
  }

  [DebuggerDisplay("{GetDefaultName}")]
  public class Verb
  {
    private readonly Noun _noun;

    public Verb(Noun noun)
    {
      _noun = noun;
    }

    public Verb()
    {
      
    }

    public string[ ] Names { get; set; }
    public Dictionary<string, Option> Options { get; set; }
    public Type RequestType { get; set; }

    public string GetDefaultName => Names.First();

    public bool IsNamed(string name)
    {
      return Names.Contains(name);
    }

    public Option GetOptionNamed(string name) => Options.SingleOrDefault(option => option.Value.IsNamed(name)).Value;

    public Option[ ] GetOrdinalOptions => Options.Values
      .Where(where => where.IsOrdinal == true)
      .OrderBy(order => order.OrdanalityOrder)
      .ToArray();

    public Option[] GetParametrizedOptions => Options.Values
      .Where(where => where.IsOrdinal == false)
      .ToArray();
  }

  [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
  public sealed class NounAttribute : Attribute
  {
    public string[] Names { get; }
    public string GetDefaultName => Names.First();

    public NounAttribute(string[] names)
    {
      Names = names;
    }

    public NounAttribute(string name)
    {
      Names = new[] { name };
    }

    public bool IsNamed(string name)
    {
      return Names.Contains(name);
    }
  }

  [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
  public sealed class VerbAttribute : Attribute
  {
    public string[] Names { get; }
    public string GetDefaultName => Names.First();

    public VerbAttribute(string[] names)
    {
      Names = names;
    }

    public VerbAttribute(string name)
    {
      Names = new[] { name };
    }

    public bool IsNamed(string name)
    {
      return Names.Contains(name);
    }
  }


  [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
  public sealed class OptionAttribute : Attribute
  {
    public string ShortName { get; }
    public string LongName { get; }
    public bool Required { get; }
    public int? OrdanalityOrder { get; }
    public string HelpText { get; }

    public OptionAttribute(string shortName, string longName, bool required = false, int ordanalityOrder = 0, string helpText = "")
    {
      ShortName = shortName;
      LongName = longName;
      Required = required;
      OrdanalityOrder = ordanalityOrder;
      HelpText = helpText;

      // if the option is required; then it must have an ordanality

      if (Required && ordanalityOrder <= 0)
      {
        var argumentOutOfRange = new ArgumentOutOfRangeException(nameof(OrdanalityOrder), OrdanalityOrder, $"When {nameof(Required)} of 'true' is supplied; a {nameof(OrdanalityOrder)} of '>= 1' (greater than or equal to one) must also be supplied.");
        throw argumentOutOfRange;
      }
      else if (!Required && OrdanalityOrder != 0)
      {
        var argumentOutOfRange = new ArgumentOutOfRangeException(nameof(OrdanalityOrder), OrdanalityOrder, $"When {nameof(Required)} of 'false' (the default value) is supplied; a {nameof(OrdanalityOrder)} of '0' must also be supplied (also the default value).");
        throw argumentOutOfRange;
      }

      // something something cant have optional null argument on an attribute; so if we see a zero here we flip it back to null
      // CS0181 C# Attribute constructor parameter has type, which is not a valid attribute parameter type

      if (ordanalityOrder == 0)
        OrdanalityOrder = null;
    }
  }

  [DebuggerDisplay("{GetFullNameFormatted} [{Value}]")]
  public class Option
  {
    /// <summary>
    ///   Gets a short name of this command line option, made of one character.
    /// </summary>
    public string ShortName { get; set; }
    public string GetShortNameFormatted => string.IsNullOrWhiteSpace(ShortName) || string.IsNullOrEmpty(ShortName) ? string.Empty : $"-{ShortName}";

    /// <summary>
    ///   Gets long name of this command line option. This name is usually a single english word.
    /// </summary>
    public string LongName { get; set; }
    public string GetLongNameFormatted => string.IsNullOrWhiteSpace(LongName) || string.IsNullOrEmpty(LongName) ? string.Empty : $"--{LongName}";

    public string GetFullNameFormatted => string.Join("|", new string[] {GetShortNameFormatted, GetLongNameFormatted});

    public bool IsNamed(string name) => name.Equals(GetShortNameFormatted) || name.Equals(GetLongNameFormatted);

    /// <summary>
    ///   Gets or sets a value indicating whether a command line option is required.
    /// </summary>
    public bool Required { get; set; }

    /// <summary>
    ///   Gets or sets a short description of this command line option. Usually a sentence summary.
    /// </summary>
    public string HelpText { get; set; } = string.Empty;

    /// <summary>
    /// Determines Ordanality and the 
    /// </summary>
    public int? OrdanalityOrder { get; set; }

    public bool IsOrdinal => OrdanalityOrder.HasValue == true;

    public string Value { get; set; }
    public bool HasValue => (string.IsNullOrWhiteSpace(Value) || string.IsNullOrEmpty(Value)) == false;

    public PropertyInfo PropertyInfo { get; set; }
  }


  public class Enrichment
  {
  }

  public class TabCompletion : Enrichment
  {
  }

  public class DirectoryNavigation : Enrichment
  {
  }
}