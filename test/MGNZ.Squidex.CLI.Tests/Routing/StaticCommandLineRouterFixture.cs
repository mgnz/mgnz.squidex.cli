namespace MGNZ.Squidex.CLI.Tests.Routing
{
  using MGNZ.Squidex.CLI.Common.Routing;
  using MGNZ.Squidex.CLI.Tests.Platform;

  internal class StaticCommandLineRouterFixture
  {
    public StaticCommandLineRouterFixture()
    {
      WithRouteCommandParser = new RouteCommandsParser(SerilogFixture.UsefullLogger<RouteCommandsParser>());
      WithRouteOptionsParser = new RouteOptionsParser(SerilogFixture.UsefullLogger<RouteOptionsParser>());
      WithRouteRequestBuilder = new RouteRequestBuilder(SerilogFixture.UsefullLogger<RouteRequestBuilder>());
      WithRouteValidator = new RouteRequestValidatorFixture().Build();
    }

    public IParseRouteCommands WithRouteCommandParser { get; set; }
    public IParseRouteOptions WithRouteOptionsParser { get; set; }
    public IBuildRouteRequests WithRouteRequestBuilder { get; set; }
    public IValidateRouteRequests WithRouteValidator { get; set; }

    public StaticCommandLineRouter Build()
    {
      return new StaticCommandLineRouter(SerilogFixture.UsefullLogger<RouteOptionsParser>(), WithRouteCommandParser, WithRouteOptionsParser, WithRouteRequestBuilder, WithRouteValidator);
    }
  }
}