namespace MGNZ.Squidex.CLI.Common.Platform
{
  using System.IO;
  using System.Text;
  using System.Threading.Tasks;

  public static class FileEx
  {
    public static async Task<string> ReadAllTextAsync(string path)
    {
      using (var sourceStream = File.Open(path, FileMode.Open))
      {
        var result = new byte[sourceStream.Length];
        await sourceStream.ReadAsync(result, 0, (int)sourceStream.Length);

        return Encoding.ASCII.GetString(result);
      }
    }

    public static async Task WriteAllTextAsync(string path, string data)
    {
      var uniencoding = new UnicodeEncoding();
      var result = uniencoding.GetBytes(data);

      using (var stream = File.Open(path, FileMode.OpenOrCreate))
      {
        stream.Seek(0, SeekOrigin.End);
        await stream.WriteAsync(result, 0, result.Length);
      }
    }
  }
}