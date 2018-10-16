namespace MGNZ.Squidex.CLI.Common.Platform
{
  using System.Collections.Generic;

  public static class DictionaryExtensionMethods
  {
    public static TValue GetOrCreate<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
      where TValue : new()
    {
      TValue value;
      if (!dictionary.TryGetValue(key, out value))
      {
        value = new TValue();
        dictionary[key] = value;
      }
      return value;
    }
  }
}