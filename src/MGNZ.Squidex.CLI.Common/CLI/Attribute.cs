namespace MGNZ.Squidex.CLI.Common.CLI
{
  using System;
  using System.Collections.Generic;
  using System.Diagnostics;
  using System.Linq;

  // name of a thing
  [DebuggerDisplay("{GetDefaultName}")]
  public class Noun
  {
    public Dictionary<string, Verb> Verbs { get; set; }
    public string[ ] Names { get; set; }

    public string GetDefaultName => Names.Take(1).ToString();

    public bool Named(string name)
    {
      return Names.Contains(name);
    }
  }

  [DebuggerDisplay("{GetDefaultName}")]
  public class Verb
  {
    public string[ ] Names { get; set; }
    public Dictionary<string, Option> Options { get; set; }

    public string GetDefaultName => Names.First();

    public bool IsNamed(string name)
    {
      return Names.Contains(name);
    }

    public Option GetOptionNamed(string name) => Options.SingleOrDefault(option => option.Value.IsNamed(name)).Value;

    public Option[ ] GetOrdinalOptions => Options.Values
      .Where(where => where.OrdanalityOrder.HasValue)
      .OrderBy(order => order.OrdanalityOrder)
      .ToArray();

    public Option[] GetParametrizedOptions => Options.Values
      .Where(where => where.OrdanalityOrder.HasValue == false)
      .ToArray();
  }

  [DebuggerDisplay("{GetLongNameFormatted} {Value}")]
  public class Option
  {
    /// <summary>
    ///   Gets a short name of this command line option, made of one character.
    /// </summary>
    public string ShortName { get; set; }

    public string GetShortNameFormatted => $"-{ShortName}";

    /// <summary>
    ///   Gets long name of this command line option. This name is usually a single english word.
    /// </summary>
    public string LongName { get; set; }

    public string GetLongNameFormatted => $"--{LongName}";

    public bool IsNamed(string name) => name.Equals(GetShortNameFormatted) || name.Equals(GetLongNameFormatted);

    /// <summary>
    ///   When applying attribute to <see cref="System.Collections.Generic.IEnumerable{T}" /> target properties,
    ///   it allows you to split an argument and consume its content as a sequence.
    /// </summary>
    public char Separator { get; set; }

    /// <summary>
    ///   Gets or sets a value indicating whether a command line option is required.
    /// </summary>
    public bool Required { get; set; }

    /// <summary>
    ///   Gets or sets a short description of this command line option. Usually a sentence summary.
    /// </summary>
    public string HelpText { get; set; }

    /// <summary>
    /// Determines Ordanality and the 
    /// </summary>
    public int? OrdanalityOrder { get; set; }

    public string Value { get; set; }
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