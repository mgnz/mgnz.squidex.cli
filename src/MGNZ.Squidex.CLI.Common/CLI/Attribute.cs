namespace MGNZ.Squidex.CLI.Common.CLI
{
  using System;
  using System.Collections.Generic;

  // attribute of a noun
  public class Attribute
  {

  }
  // name of a thing
  public class Noun
  {
    public Dictionary<string, Verb> Verbs { get; set; }
    public string[] Names { get; set; }
  }
  // name of an action
  public class Verb
  {
    public string[] Names { get; set; }
    public Option[] Options { get; set; }

    public Operation Operation { get; set; }
  }
  public class Option
  {
    /// <summary>
    /// Gets a short name of this command line option, made of one character.
    /// </summary>
    public string ShortName { get; set; }

    /// <summary>
    /// Gets long name of this command line option. This name is usually a single english word.
    /// </summary>
    public string LongName { get; set; }

    /// <summary>
    /// When applying attribute to <see cref="System.Collections.Generic.IEnumerable{T}"/> target properties,
    /// it allows you to split an argument and consume its content as a sequence.
    /// </summary>
    public char Separator { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether a command line option is required.
    /// </summary>
    public bool Required { get; set; }

    /// <summary>
    /// Gets or sets a short description of this command line option. Usually a sentence summary.
    /// </summary>
    public string HelpText { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool Ordinal { get; set; }
  }

  // describes a link to a command (a thing that links an noun => verb [arguments,...] to something that actually executes)
  public class Operation
  {
    public Type TypeReference { get; set; }
    public BaseArguments Arguments { get; set; }
  }

  // each command should have its own arguments
  public class BaseArguments
  {
    public virtual bool IsValid { get; }

    // uses reflection to turn Arguments into a object array;
    // this along with TypeReference is passed into teh DI container
    // so it can resolve the Command and fill in all of the arguments
    public virtual object[] GenerateConstructorArguments()
    {
      return new object[] { "", 1, "foobar" };
    }
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