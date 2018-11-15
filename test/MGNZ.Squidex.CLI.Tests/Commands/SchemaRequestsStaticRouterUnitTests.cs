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
    public static List<object[]> StaticRouter_Verify_SchemaImport_Data => BuildSchemaImportData("schema", "import");
    public static List<object[]> StaticRouter_Verify_SchemaExport_Data => BuildSchemaImportData("schema", "export");
    public static List<object[]> StaticRouter_Verify_SchemaDelete_Data => BuildSchemaImportData("schema", "delete");
    public static List<object[]> StaticRouter_Verify_SchemaList_Data => BuildSchemaImportData("schema", "list");
    public static List<object[]> StaticRouter_Verify_SchemaTag_Data => BuildSchemaImportData("schema", "tag");

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

    [Theory]
    [MemberData(nameof(StaticRouter_Verify_SchemaExport_Data))]
    public void StaticRouter_Verify_SchemaExport(SchemaExportRequest expectedRequest, string inCommandLine)
    {
      var router = new StaticCommandLineRouterFixture()
      {
        WithRouteValidator = new RouteRequestValidatorFixture().Mock()
      }.Build();

      var actual = router.GetOne(RoutingMetadata.Value.Values, inCommandLine.Split(' '));

      actual.Should().NotBeNull();
      actual.Should().BeAssignableTo<SchemaExportRequest>();
      actual.Should().BeEquivalentTo(expectedRequest);
    }

    [Theory]
    [MemberData(nameof(StaticRouter_Verify_SchemaDelete_Data))]
    public void StaticRouter_Verify_SchemaDelete(SchemaDeleteRequest expectedRequest, string inCommandLine)
    {
      var router = new StaticCommandLineRouterFixture()
      {
        WithRouteValidator = new RouteRequestValidatorFixture().Mock()
      }.Build();

      var actual = router.GetOne(RoutingMetadata.Value.Values, inCommandLine.Split(' '));

      actual.Should().NotBeNull();
      actual.Should().BeAssignableTo<SchemaDeleteRequest>();
      actual.Should().BeEquivalentTo(expectedRequest);
    }

    [Theory]
    [MemberData(nameof(StaticRouter_Verify_SchemaList_Data))]
    public void StaticRouter_Verify_SchemaList(SchemaListRequest expectedRequest, string inCommandLine)
    {
      var router = new StaticCommandLineRouterFixture()
      {
        WithRouteValidator = new RouteRequestValidatorFixture().Mock()
      }.Build();

      var actual = router.GetOne(RoutingMetadata.Value.Values, inCommandLine.Split(' '));

      actual.Should().NotBeNull();
      actual.Should().BeAssignableTo<SchemaListRequest>();
      actual.Should().BeEquivalentTo(expectedRequest);
    }

    [Theory]
    [MemberData(nameof(StaticRouter_Verify_SchemaTag_Data))]
    public void StaticRouter_Verify_SchemaTag(SchemaTagRequest expectedRequest, string inCommandLine)
    {
      var router = new StaticCommandLineRouterFixture()
      {
        WithRouteValidator = new RouteRequestValidatorFixture().Mock()
      }.Build();

      var actual = router.GetOne(RoutingMetadata.Value.Values, inCommandLine.Split(' '));

      actual.Should().NotBeNull();
      actual.Should().BeAssignableTo<SchemaTagRequest>();
      actual.Should().BeEquivalentTo(expectedRequest);
    }
  }
}