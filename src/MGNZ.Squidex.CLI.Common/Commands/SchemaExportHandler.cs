namespace MGNZ.Squidex.CLI.Common.Commands
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;

  using Autofac;

  using MediatR;

  using MGNZ.Squidex.Client;
  using MGNZ.Squidex.Client.Transport;
  using MGNZ.Squidex.CLI.Common.Platform;
  using MGNZ.Squidex.CLI.Common.Routing;

  using Newtonsoft.Json;

  using Serilog;

  public class SchemaExportHandler : BaseHandler<SchemaExportRequest>
  {
    private readonly IConsoleWriter _consoleWriter;

    public SchemaExportHandler(ILogger logger, IClientProxyFactory clientFactory, IConsoleWriter consoleWriter, IContainer container)
      : base(logger, clientFactory, container)
    {
      _consoleWriter = consoleWriter;
    }

    /// <inheritdoc />
    public override async Task<Unit> Handle(SchemaExportRequest request, CancellationToken cancellationToken)
    {
      var proxy = ClientFactory.GetClientProxy<ISquidexAppSchemaClient>(request.AliasCredentials);

      if (await proxy.SchemaExists(request.Application, request.Name))
      {
        var data = await proxy.GetSchema(request.Application, request.Name);

        var json = JsonConvert.SerializeObject(data, Formatting.Indented);
        await FileEx.WriteAllTextAsync(request.Path, json);
      }
      else
      {
        // todo : log and throw not found
      }

      return Unit.Value;
    }
  }

  [Noun("schema"), Verb("export")]
  public class SchemaExportRequest: BaseRequest
  {
    [Option("app", "application", required: true, ordanalityOrder: 1)] public string Application { get; set; }
    [Option("n", "name", required: true, ordanalityOrder: 2)] public string Name { get; set; }
    [Option("p", "path", required: true, ordanalityOrder: 3)] public string Path { get; set; }
    [Option("c", "alias-credentials")] public string AliasCredentials { get; set; }
  }
}