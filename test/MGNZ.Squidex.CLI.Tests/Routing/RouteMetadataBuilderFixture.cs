namespace MGNZ.Squidex.CLI.Tests.Routing
{
  using MGNZ.Squidex.CLI.Common.Routing;
  using MGNZ.Squidex.CLI.Tests.Platform;

  internal class RouteMetadataBuilderFixture
  {
    public RouteMetadataBuilderFixture()
    {
      WithRouteAttribueReflector = new RouteAttributeReflector(SerilogFixture.UsefullLogger<RouteAttributeReflector>());
    }

    public IReflectRouteAttributes WithRouteAttribueReflector { get; set; }

    public IBuildRouteMetadata Build()
    {
      return new RouteMetadataBuilder(SerilogFixture.UsefullLogger<RouteMetadataBuilder>(), WithRouteAttribueReflector);
    }
  }
}