namespace MGNZ.Squidex.CLI.Tests.Commands.CommandRouting
{
  using System.Collections.Generic;

  using FluentAssertions;

  using MGNZ.Squidex.CLI.Common.Commands;
  using MGNZ.Squidex.CLI.Tests.Routing;

  using Xunit;

  [Trait("category", "unit")]
  public class AssetCommandRoutingUnitTest : BaseCommandRoutingUnitTest
  {
    public static List<object[]> StaticRouter_Verify_AssetImport_Data => BuildSchemaImportData("asset", "import");
    public static List<object[ ]> StaticRouter_Verify_AssetExport_Data => BuildSchemaImportData("asset", "export");
    public static List<object[]> StaticRouter_Verify_AssetDelete_Data => BuildSchemaImportData("asset", "delete");
    public static List<object[]> StaticRouter_Verify_AssetList_Data => BuildSchemaImportData("asset", "list");
    public static List<object[]> StaticRouter_Verify_AssetTag_Data => BuildSchemaImportData("asset", "tag");
    public static List<object[]> StaticRouter_Verify_AssetUpdate_Data => BuildSchemaImportData("asset", "update");

    [Theory]
    [MemberData(nameof(StaticRouter_Verify_AssetImport_Data))]
    public void StaticRouter_Verify_AssetImport(AssetImportRequest expectedRequest, string inCommandLine)
    {
      var router = new StaticCommandLineRouterFixture()
      {
        WithRouteValidator = new RouteRequestValidatorFixture().Mock()
      }.Build();

      var actual = router.GetOne(RoutingMetadata.Value.Values, inCommandLine.Split(' '));

      actual.Should().NotBeNull();
      actual.Should().BeAssignableTo<AssetImportRequest>();
      actual.Should().BeEquivalentTo(expectedRequest);
    }

    [Theory]
    [MemberData(nameof(StaticRouter_Verify_AssetExport_Data))]
    public void StaticRouter_Verify_AssetExport(AssetExportRequest expectedRequest, string inCommandLine)
    {
      var router = new StaticCommandLineRouterFixture()
      {
        WithRouteValidator = new RouteRequestValidatorFixture().Mock()
      }.Build();

      var actual = router.GetOne(RoutingMetadata.Value.Values, inCommandLine.Split(' '));

      actual.Should().NotBeNull();
      actual.Should().BeAssignableTo<AssetExportRequest>();
      actual.Should().BeEquivalentTo(expectedRequest);
    }

    [Theory]
    [MemberData(nameof(StaticRouter_Verify_AssetDelete_Data))]
    public void StaticRouter_Verify_AssetDelete(AssetDeleteRequest expectedRequest, string inCommandLine)
    {
      var router = new StaticCommandLineRouterFixture()
      {
        WithRouteValidator = new RouteRequestValidatorFixture().Mock()
      }.Build();

      var actual = router.GetOne(RoutingMetadata.Value.Values, inCommandLine.Split(' '));

      actual.Should().NotBeNull();
      actual.Should().BeAssignableTo<AssetDeleteRequest>();
      actual.Should().BeEquivalentTo(expectedRequest);
    }

    [Theory]
    [MemberData(nameof(StaticRouter_Verify_AssetList_Data))]
    public void StaticRouter_Verify_AssetPost(AssetListRequest expectedRequest, string inCommandLine)
    {
      var router = new StaticCommandLineRouterFixture()
      {
        WithRouteValidator = new RouteRequestValidatorFixture().Mock()
      }.Build();

      var actual = router.GetOne(RoutingMetadata.Value.Values, inCommandLine.Split(' '));

      actual.Should().NotBeNull();
      actual.Should().BeAssignableTo<AssetListRequest>();
      actual.Should().BeEquivalentTo(expectedRequest);
    }

    [Theory]
    [MemberData(nameof(StaticRouter_Verify_AssetTag_Data))]
    public void StaticRouter_Verify_AssetTag(AssetTagRequest expectedRequest, string inCommandLine)
    {
      var router = new StaticCommandLineRouterFixture()
      {
        WithRouteValidator = new RouteRequestValidatorFixture().Mock()
      }.Build();

      var actual = router.GetOne(RoutingMetadata.Value.Values, inCommandLine.Split(' '));

      actual.Should().NotBeNull();
      actual.Should().BeAssignableTo<AssetTagRequest>();
      actual.Should().BeEquivalentTo(expectedRequest);
    }

    [Theory]
    [MemberData(nameof(StaticRouter_Verify_AssetUpdate_Data))]
    public void StaticRouter_Verify_AssetUpdate(AssetUpdateContentRequest expectedRequest, string inCommandLine)
    {
      var router = new StaticCommandLineRouterFixture()
      {
        WithRouteValidator = new RouteRequestValidatorFixture().Mock()
      }.Build();

      var actual = router.GetOne(RoutingMetadata.Value.Values, inCommandLine.Split(' '));

      actual.Should().NotBeNull();
      actual.Should().BeAssignableTo<AssetUpdateContentRequest>();
      actual.Should().BeEquivalentTo(expectedRequest);
    }
  }
}