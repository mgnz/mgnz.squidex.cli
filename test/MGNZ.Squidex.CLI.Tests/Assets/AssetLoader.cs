namespace MGNZ.Squidex.CLI.Tests
{
  using System;
  using System.IO;
  using System.Reflection;

  using Newtonsoft.Json;

  using Xunit;

  [Collection("Sequential Squidex Integration Tests")]
  [Trait("category", "squidex-api-integration")]
  public class AssetLoader
  {
    private string ns => typeof(AssetLoader).Namespace;

    public Lazy<dynamic> Schema1 => LoadAsset($"{ns}.schema1.json");
    public Lazy<dynamic> Schema1DataQuery => LoadAsset($"{ns}.schema1.data.query.json");
    public Lazy<dynamic> Schema1Data1Query => LoadAsset($"{ns}.schema1.data.1.query.json");
    public Lazy<dynamic> Schema1Data1Post => LoadAsset($"{ns}.schema1.data.1.post.json");
    public Lazy<dynamic> Schema1Data1PostResponse => LoadAsset($"{ns}.schema1.data.1.post.response.json");


    public static string ExecutingPath => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    public static string AssetPath => Path.Combine(ExecutingPath, "Assets");
    public static string Schema1Path => Path.Combine(AssetPath, "schema1.json");
    public static string Schema1DataQueryPath => Path.Combine(AssetPath, "schema1.data.query.json");
    public static string Schema1Data1QueryPath => Path.Combine(AssetPath, "schema1.data.1.query.json");
    public static string Schema1Data1PostPath => Path.Combine(AssetPath, "schema1.data.1.post.json");
    public static string Schema1Data1PostResponsePath => Path.Combine(AssetPath, "schema1.data.1.post.response.json");

    public static string ExportPath => Path.Combine(ExecutingPath, "Exports");

    public dynamic LoadAsset(string path) => JsonConvert.DeserializeObject(StreamToString(GetManifestResourceStream(path)));

    private Stream GetManifestResourceStream(string fullyQualifiedNamespace) => GetManifestResourceStream(typeof(AssetLoader).GetTypeInfo().Assembly, fullyQualifiedNamespace);
    private Stream GetManifestResourceStream(Assembly assembly, string fullyQualifiedNamespace) => assembly.GetManifestResourceStream(fullyQualifiedNamespace);

    protected string StreamToString(Stream inputStream)
    {
      using (var reader = new StreamReader(inputStream))
        return reader.ReadToEnd();
    }
  }
}