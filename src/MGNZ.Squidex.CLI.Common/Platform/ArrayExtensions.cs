namespace MGNZ.Squidex.CLI.Common.Platform
{
  using System.Linq;

  public static class ArrayExtensions
  {
    public static void Deconstruct<T>(this T[ ] array, out T first, out T[ ] rest)
    {
      first = array.Length > 0 ? array[0] : default(T);
      rest = array.Skip(1).ToArray();
    }

    public static void Deconstruct<T>(this T[ ] array, out T first, out T second, out T[ ] rest) => (first, (second, rest)) = array;
    public static void Deconstruct<T>(this T[ ] array, out T first, out T second, out T third, out T[ ] rest) => (first, second, (third, rest)) = array;
    public static void Deconstruct<T>(this T[ ] array, out T first, out T second, out T third, out T forth, out T[ ] rest) => (first, second, third, (forth, rest)) = array;
    public static void Deconstruct<T>(this T[ ] array, out T first, out T second, out T third, out T forth, out T fifth, out T[ ] rest) => (first, second, third, forth, (fifth, rest)) = array;
    public static void Deconstruct<T>(this T[ ] array, out T first, out T second, out T third, out T forth, out T fifth, out T sixth, out T[ ] rest) => (first, second, third, forth, fifth, (sixth, rest)) = array;
    public static void Deconstruct<T>(this T[ ] array, out T first, out T second, out T third, out T forth, out T fifth, out T sixth, out T seventh, out T[ ] rest) => (first, second, third, forth, fifth, sixth, (seventh, rest)) = array;
  }
}