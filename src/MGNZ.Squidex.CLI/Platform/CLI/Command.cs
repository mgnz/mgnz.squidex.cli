using System;
using System.Collections.Generic;

namespace MGNZ.Squidex.CLI.Platform.CLI
{
  // attribute of a noun
  internal class Attribute
  {

  }
  // name of a thing
  internal class Noun
  {
    public Dictionary<string, Verb<BaseArguments>> Verbs { get; set; }
    public string[] Names { get; set; }
  }
  // name of an action
  internal class Verb<TArguments>
    where TArguments : BaseArguments
  {
    public string[] Names { get; set; }
    public Operation<TArguments> Operation { get; set; }
  }
  // describes a link to a command (a thing that links an noun => verb [arguments,...] to something that actually executes)
  internal class Operation<TArguments>
    where TArguments : BaseArguments
  {
    public Type TypeReference { get; set; }
    public TArguments Arguments { get; set; }
  }

  // each command should have its own arguments
  internal class BaseArguments
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


  class testy
  {
    public testy()
    {
      var app = new Noun()
      {
        Names = new [] {"app", "apps"},
        Verbs = new Dictionary<string, Verb<BaseArguments>>()
        {
          { "delete-app", new Verb<BaseArguments>() { Names = new []{ "delete" } }},
          { "list-app", new Verb<BaseArguments>() { Names = new []{ "list" } }},
          { "login-app", new Verb<BaseArguments>() { Names = new []{ "login" } }},
          { "logout-app", new Verb<BaseArguments>() { Names = new []{ "logout" } }},
          { "new-app", new Verb<BaseArguments>() { Names = new []{ "new" } }}
        }
      };
      var asset = new Noun()
      {
        Names = new[] { "asset", "assets" },
        Verbs = new Dictionary<string, Verb<BaseArguments>>()
        {
          { "delete-asset", new Verb<BaseArguments>() { Names = new []{ "delete" } }},
          { "list-asset", new Verb<BaseArguments>() { Names = new []{ "list" } }},
          { "import-asset", new Verb<BaseArguments>() { Names = new []{ "import" } }},
          { "export-asset", new Verb<BaseArguments>() { Names = new []{ "export" } }},
          { "tag-asset", new Verb<BaseArguments>() { Names = new []{ "tag" } }}
        }
      };
      var content = new Noun()
      {
        Names = new[] { "content", "contents" },
        Verbs = new Dictionary<string, Verb<BaseArguments>>()
        {
          { "delete-content", new Verb<BaseArguments>() { Names = new []{ "delete" } }},
          { "import-content", new Verb<BaseArguments>() { Names = new []{ "import" } }},
          { "export-content", new Verb<BaseArguments>() { Names = new []{ "export" } }}
        }
      };
      var schema = new Noun
      {
        Names = new[] { "schema", "schemas ", "schemata" },
        Verbs = new Dictionary<string, Verb<BaseArguments>>()
        {
          { "delete-schema", new Verb<BaseArguments>() { Names = new []{ "delete" } }},
          { "list-schema", new Verb<BaseArguments>() { Names = new []{ "list" } }},
          { "export-schema", new Verb<BaseArguments>() { Names = new []{ "export" } }},
          { "import-schema", new Verb<BaseArguments>() { Names = new []{ "import" } }}
        }
      };
    }
  }
}