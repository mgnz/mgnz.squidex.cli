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
    public Operation Operation { get; set; }
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