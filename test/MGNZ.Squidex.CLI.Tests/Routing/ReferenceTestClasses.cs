namespace MGNZ.Squidex.CLI.Tests.Routing
{
  using MGNZ.Squidex.CLI.Common.Routing;

  [Noun("noun1")]
  [Verb("verb1")]
  internal class ReferenceA
  {
    [Option("a", "option-a", true, 1)]
    public string OptionA { get; set; }

    [Option("b", "option-b")]
    public string OptionB { get; set; }
  }

  [Noun("noun1")]
  [Verb("verbb")]
  internal class ReferenceB
  {
    [Option("a", "option-a", true, 1)]
    public string OptionA { get; set; }

    [Option("b", "option-b")]
    public string OptionB { get; set; }
  }

  [Noun("noun2")]
  internal class BaseNounReference1
  {
  }

  [Verb("verb2")]
  internal class BaseVerbReference1 : BaseNounReference1
  {
    [Option("a", "option-a", true, 1)]
    public string OptionA { get; set; }

    [Option("b", "option-b")]
    public string OptionB { get; set; }
  }

  [Verb("verb3")]
  internal class BaseVerbReference2 : BaseNounReference1
  {
    [Option("a", "option-a", true, 1)]
    public string OptionA { get; set; }

    [Option("b", "option-b")]
    public string OptionB { get; set; }
  }
}