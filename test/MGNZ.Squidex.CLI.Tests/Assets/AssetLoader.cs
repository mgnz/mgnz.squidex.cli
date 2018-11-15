namespace MGNZ.Squidex.CLI.Tests.Assets
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
    private static string ns => typeof(AssetLoader).Namespace;

    public static Lazy<dynamic> Schema1 => LoadAsset($"{ns}.schema1.json");
    public static Lazy<dynamic> Schema1Data1Post => new Lazy<dynamic>(LoadAsset($"{ns}.schema1.data.1.post.json"));
    public static Lazy<dynamic> Schema1Data1PostResponse => new Lazy<dynamic>(LoadAsset($"{ns}.schema1.data.1.post.response.json"));
    public static Lazy<dynamic> Schema1Data2Post => new Lazy<dynamic>(LoadAsset($"{ns}.schema1.data.2.post.json"));
    public static Lazy<dynamic> Schema1Data2PostResponse => new Lazy<dynamic>(LoadAsset($"{ns}.schema1.data.2.post.response.json"));
    public static Lazy<dynamic> Schema1DataImport => new Lazy<dynamic>(LoadAsset($"{ns}.schema1.data.import.json"));
    public static Lazy<dynamic> Schema1DataImportResponse => new Lazy<dynamic>(LoadAsset($"{ns}.schema1.data.import.response.json"));
    public static Lazy<dynamic> Schema1DataQueryResponse => new Lazy<dynamic>(LoadAsset($"{ns}.schema1.data.query.response.json"));
    public static Lazy<dynamic> Schema1DataExportResponse => new Lazy<dynamic>(LoadAsset($"{ns}.schema1.data.export.response.json"));


    public static string ExecutingPath => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    public static string AssetPath => Path.Combine(ExecutingPath, "Assets");
    public static string Schema1Path => Path.Combine(AssetPath, "schema1.json");
    public static string Schema1Data1PostPath => Path.Combine(AssetPath, "schema1.data.1.post.json");
    public static string Schema1Data1PostResponsePath => Path.Combine(AssetPath, "schema1.data.1.post.response.json");
    public static string Schema1Data2PostPath => Path.Combine(AssetPath, "schema1.data.2.post.json");
    public static string Schema1Data2PostResponsePath => Path.Combine(AssetPath, "schema1.data.2.post.response.json");
    public static string Schema1DataImportPath => Path.Combine(AssetPath, "schema1.data.import.json");
    public static string Schema1DataImportResponsePath => Path.Combine(AssetPath, "schema1.data.import.response.json");
    public static string Schema1DataExportResponsePath => Path.Combine(AssetPath, "schema1.data.export.response.json");

    public static string ExportPath => Path.Combine(ExecutingPath, "Exports");

    public static dynamic LoadAsset(string path) => JsonConvert.DeserializeObject<dynamic>(StreamToString(GetManifestResourceStream(path)));

    private static Stream GetManifestResourceStream(string fullyQualifiedNamespace) => GetManifestResourceStream(typeof(AssetLoader).GetTypeInfo().Assembly, fullyQualifiedNamespace);
    private static Stream GetManifestResourceStream(Assembly assembly, string fullyQualifiedNamespace) => assembly.GetManifestResourceStream(fullyQualifiedNamespace);

    protected static string StreamToString(Stream inputStream)
    {
      using (var reader = new StreamReader(inputStream))
        return reader.ReadToEnd();
    }
  }
}