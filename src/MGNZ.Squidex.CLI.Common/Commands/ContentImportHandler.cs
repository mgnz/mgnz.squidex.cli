namespace MGNZ.Squidex.CLI.Common.Commands
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;

  using Autofac;

  using MediatR;

  using MGNZ.Squidex.Client;
  using MGNZ.Squidex.Client.Model;
  using MGNZ.Squidex.Client.Transport;
  using MGNZ.Squidex.CLI.Common.Platform;
  using MGNZ.Squidex.CLI.Common.Routing;

  using Newtonsoft.Json;

  using Serilog;

  public class ContentImportHandler : BaseHandler<ContentImportRequest>
  {
    private IConsoleWriter _consoleWriter = null;

    public ContentImportHandler(ILogger logger, IClientProxyFactory clientFactory, IConsoleWriter consoleWriter, IContainer container)
      : base(logger, clientFactory, container)
    {
      this._consoleWriter = consoleWriter;
    }
    /// <inheritdoc />
    public override async Task<Unit> Handle(ContentImportRequest request, CancellationToken cancellationToken)
    {
      var proxy = ClientFactory.GetClientProxy<ISquidexContentClient>(request.AliasCredentials);
      var json = await FileEx.ReadAllTextAsync(request.Path);
      var inputFileContent = JsonConvert.DeserializeObject<ImportDto<dynamic>>(json);

      var publish = false;
      bool.TryParse(request.Publish, out publish);
      
      foreach (var itemContent in inputFileContent.Items)
      {
        var data = itemContent.Data;

        var createResponse = await proxy.CreateContent(request.Application, request.Schema, data);
        string createid = Convert.ToString(createResponse.id);
        if (publish) await proxy.PublishContent(request.Application, request.Schema, createid);
      }

      return Unit.Value;
    }
  }

  [Noun("content"), Verb("import")]
  public class ContentImportRequest: BaseRequest
  {
    [Option("app", "application", required: true, ordanalityOrder: 1)] public string Application { get; set; }
    [Option("sc", "schema", required: true, ordanalityOrder: 2)] public string Schema { get; set; }
    [Option("p", "path", required: true, ordanalityOrder: 3)] public string Path { get; set; }
    [Option("u", "publish")] public string Publish { get; set; }
    [Option("c", "alias-credentials")] public string AliasCredentials { get; set; }
  }

  public class ImportDto<TModel>
  {
    [JsonProperty(PropertyName = "id")] public int? Total { get; set; }
    [JsonProperty(PropertyName = "items")] public ImportItemDto<TModel>[] Items { get; set; }
  }

  public class ImportItemDto<TModel>
  {
    [JsonProperty(PropertyName = "id")] public string Id { get; set; }
    [JsonProperty(PropertyName = "data")] public TModel Data { get; set; }
    [JsonProperty(PropertyName = "version")] public int? Version { get; set; }
    [JsonProperty(PropertyName = "created")] public DateTime? Created { get; set; }
    [JsonProperty(PropertyName = "createdby")] public string CreatedBy { get; set; }
    [JsonProperty(PropertyName = "lastmodified")] public DateTime? LastModified { get; set; }
    [JsonProperty(PropertyName = "lastmodifiedby")] public string LastModifiedBy { get; set; }
  }
}