namespace MGNZ.Squidex.CLI.Tests.Commands
{
  using System.Collections.Generic;
  using System.Linq;

  using FluentAssertions;

  using MGNZ.Squidex.CLI.Common.Commands;
  using MGNZ.Squidex.CLI.Tests.Routing;

  using Xunit;

  public class SchemaRequestsStaticRouterUnitTests : BaseRequestStaticRouterUnitTest
  {
    public static List<object[]> StaticRouter_Verify_SchemaImport_Data => BuildSchemaImportData("schema", "import").ToList();

    [Theory]
    [MemberData(nameof(StaticRouter_Verify_SchemaImport_Data))]
    public void StaticRouter_Verify_SchemaImport(SchemaImportRequest expectedRequest, string inCommandLine)
    {
      var router = new StaticCommandLineRouterFixture()
      {
        WithRouteValidator = new RouteRequestValidatorFixture().Mock()
      }.Build();

      var actual = router.GetOne(RoutingMetadata.Value.Values, inCommandLine.Split(' '));

      actual.Should().NotBeNull();
      actual.Should().BeAssignableTo<SchemaImportRequest>();
      actual.Should().BeEquivalentTo(expectedRequest);
    }
  }
}