namespace MGNZ.Squidex.CLI.Common.CLI
{
  using System.Collections.Generic;
  using System.Threading.Tasks;

  /// <summary>
  /// Takes a CLI argumnet for an input; returns a KV dictionary in the following form
  /// - Noun
  /// - Action
  /// - K,V[] { parameter, value }
  /// Actual deserialization into a HandlerRequest happens at a later stage; all this
  /// is interested in is getting the chunks out; so suppose it could be called the chunkifier
  /// </summary>
  public interface ICommandLineDictionaryConverter
  {
    Task<Dictionary<string, string>> Parse(string[ ] args);
    Task<Dictionary<string, string>> Parse(string args);
  }
}