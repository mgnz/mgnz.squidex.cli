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
    private Lazy<dynamic> _schema1 = null;
    private Lazy<dynamic> _schema1dataquery = null;
    private Lazy<dynamic> _schema1data1query = null;
    private Lazy<dynamic> _schema1data1post = null;

    public AssetLoader()
    {
      var ns = typeof(AssetLoader).Namespace;

      _schema1 = new Lazy<dynamic>(() => LoadAsset($"{ns}.schema1.json"));
      _schema1dataquery = new Lazy<dynamic>(() => LoadAsset($"{ns}.schema1.data.query.json"));
      _schema1data1query = new Lazy<dynamic>(() => LoadAsset($"{ns}.schema1.data.1.query.json"));
      _schema1data1post = new Lazy<dynamic>(() => LoadAsset($"{ns}.schema1.data.1.post.json"));
    }

    public Lazy<dynamic> Schema1 => this._schema1.Value;
    public Lazy<dynamic> Schema1DataQuery => this._schema1dataquery.Value;
    public Lazy<dynamic> Schema1Data1Query => this._schema1data1query.Value;
    public Lazy<dynamic> Schema1Data1Post => this._schema1data1post.Value;

    public static string ExecutingPath => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    public static string AssetPath => Path.Combine(ExecutingPath, "Assets");
    public static string Schema1Path => Path.Combine(AssetPath, "schema1.json");
    public static string Schema1DataQueryPath => Path.Combine(AssetPath, "schema1.data.query.json");
    public static string Schema1Data1QueryPath => Path.Combine(AssetPath, "schema1.data.1.query.json");
    public static string Schema1Data1PostPath => Path.Combine(AssetPath, "schema1.data.1.post.json");

    public static string ExportPath => Path.Combine(ExecutingPath, "Exports");

    public dynamic LoadAsset(string path) => JsonConvert.DeserializeObject(this.StreamToString(this.GetManifestResourceStream(path)));

    private Stream GetManifestResourceStream(string fullyQualifiedNamespace) => this.GetManifestResourceStream(typeof(AssetLoader).GetTypeInfo().Assembly, fullyQualifiedNamespace);
    private Stream GetManifestResourceStream(Assembly assembly, string fullyQualifiedNamespace) => assembly.GetManifestResourceStream(fullyQualifiedNamespace);

    protected string StreamToString(Stream inputStream)
    {
      using (var reader = new StreamReader(inputStream))
        return reader.ReadToEnd();
    }
  }
}