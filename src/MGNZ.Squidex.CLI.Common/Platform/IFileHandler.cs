namespace MGNZ.Squidex.CLI.Common.Platform
{
  using System.Threading.Tasks;

  using MGNZ.Squidex.Client.Transport;

  using Newtonsoft.Json;

  using Serilog;

  public class FileHandler : IFileHandler
  {
    private readonly ILogger _logger;

    public FileHandler(ILogger logger)
    {
      _logger = logger;
    }

    public async Task<QueryResponse<dynamic>> ReadFile(string path)
    {
      var data = await FileEx.ReadAllTextAsync(path);
      return JsonConvert.DeserializeObject<QueryResponse<dynamic>>(data);
    }

    public async Task WriteFile(string path, QueryResponse<dynamic> data)
    {
      var json = JsonConvert.SerializeObject(data, Formatting.Indented);
      await FileEx.WriteAllTextAsync(path, json);
    }
  }

  public interface IFileHandler
  {
    Task<QueryResponse<dynamic>> ReadFile(string path);
    Task WriteFile(string path, QueryResponse<dynamic> data);
  }
}