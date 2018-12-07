namespace MGNZ.Squidex.CLI.Tests.Commands
{
  using System.Collections.Generic;

  using FluentAssertions;

  using MGNZ.Squidex.CLI.Common.Commands;
  using MGNZ.Squidex.CLI.Tests.Routing;

  using Xunit;

  [Trait("category", "unit")]
  public class ContentRequestStaticRouterUnitTests : BaseRequestStaticRouterUnitTest
  {
    public static List<object[]> StaticRouter_Verify_ContentImport_Data => BuildSchemaImportData("content", "import");
    public static List<object[ ]> StaticRouter_Verify_ContentExport_Data => BuildSchemaImportData("content", "export");
    public static List<object[]> StaticRouter_Verify_ContentDelete_Data => BuildSchemaImportData("content", "delete");
    public static List<object[]> StaticRouter_Verify_ContentPost_Data => BuildSchemaImportData("content", "post");

    [Theory]
    [MemberData(nameof(StaticRouter_Verify_ContentImport_Data))]
    public void StaticRouter_Verify_ContentImport(ContentImportRequest expectedRequest, string inCommandLine)
    {
      var router = new StaticCommandLineRouterFixture()
      {
        WithRouteValidator = new RouteRequestValidatorFixture().Mock()
      }.Build();

      var actual = router.GetOne(RoutingMetadata.Value.Values, inCommandLine.Split(' '));

      actual.Should().NotBeNull();
      actual.Should().BeAssignableTo<ContentImportRequest>();
      actual.Should().BeEquivalentTo(expectedRequest);
    }

    [Theory]
    [MemberData(nameof(StaticRouter_Verify_ContentExport_Data))]
    public void StaticRouter_Verify_ContentExport(ContentExportRequest expectedRequest, string inCommandLine)
    {
      var router = new StaticCommandLineRouterFixture()
      {
        WithRouteValidator = new RouteRequestValidatorFixture().Mock()
      }.Build();

      var actual = router.GetOne(RoutingMetadata.Value.Values, inCommandLine.Split(' '));

      actual.Should().NotBeNull();
      actual.Should().BeAssignableTo<ContentExportRequest>();
      actual.Should().BeEquivalentTo(expectedRequest);
    }

    [Theory]
    [MemberData(nameof(StaticRouter_Verify_ContentDelete_Data))]
    public void StaticRouter_Verify_ContentDelete(ContentDeleteRequest expectedRequest, string inCommandLine)
    {
      var router = new StaticCommandLineRouterFixture()
      {
        WithRouteValidator = new RouteRequestValidatorFixture().Mock()
      }.Build();

      var actual = router.GetOne(RoutingMetadata.Value.Values, inCommandLine.Split(' '));

      actual.Should().NotBeNull();
      actual.Should().BeAssignableTo<ContentDeleteRequest>();
      actual.Should().BeEquivalentTo(expectedRequest);
    }

    [Theory]
    [MemberData(nameof(StaticRouter_Verify_ContentPost_Data))]
    public void StaticRouter_Verify_ContentPost(ContentPostRequest expectedRequest, string inCommandLine)
    {
      var router = new StaticCommandLineRouterFixture()
      {
        WithRouteValidator = new RouteRequestValidatorFixture().Mock()
      }.Build();

      var actual = router.GetOne(RoutingMetadata.Value.Values, inCommandLine.Split(' '));

      actual.Should().NotBeNull();
      actual.Should().BeAssignableTo<ContentPostRequest>();
      actual.Should().BeEquivalentTo(expectedRequest);
    }
  }
}