namespace MGNZ.Squidex.CLI.Common.Commands
{
  using System.Threading;
  using System.Threading.Tasks;

  using MGNZ.Squidex.CLI.Common.Routing;

  using Autofac;

  using MediatR;

  using Serilog;

  public class AppLoginHandler : BaseHandler<AppLoginRequest>
  {
    public AppLoginHandler(ILogger logger, IContainer container) : base(logger, container) { }

    /// <inheritdoc />
    public override async Task<Unit> Handle(AppLoginRequest request, CancellationToken cancellationToken)
    {
      return await base.Handle(request, cancellationToken);
    }
  }

  [Noun("app"), Verb("login")]
  public class AppLoginRequest: BaseRequest
  {
    [Option("u", "url", required: true, ordanalityOrder: 1)] public string Url { get; set; }
    [Option("n", "name", required: true, ordanalityOrder: 2)] public string Name { get; set; }
    [Option("cid", "client-id")] public string ClientId { get; set; }
    [Option("cs", "client-secret")] public string ClientSecret { get; set; }
    [Option("t", "token")] public string Token { get; set; }
    [Option("c", "alias-credentials")] public string AliasCredentials { get; set; }
    [Option("a", "alias-credentials-as")] public string AliasCredentialsAs { get; set; }
  }
}