namespace MGNZ.Squidex.CLI.Tests.Commands
{
  using System.Collections.Generic;

  using FluentAssertions;

  using MGNZ.Squidex.CLI.Common.Commands;
  using MGNZ.Squidex.CLI.Common.Routing;
  using MGNZ.Squidex.CLI.Tests.Routing;

  using Xunit;

  [Trait("category", "unit")]
  public class ContentDeleteRequestUnitTests
  {
    // [Option("sc", "schema", required: true, ordanalityOrder: 1)] public string Schema { get; set; }
    // [Option("id", "id", required: true, ordanalityOrder: 2)] public string Id { get; set; }
    // [Option("c", "alias-credentials")] public string AliasCredentials { get; set; }

    public static List<object[]> StaticRouter_Verify_SimpleMap_HappyPath_Data => new List<object[]>
    {
      new object[ ]
      {
        // expected
        new ContentDeleteRequest
        {
          Schema = "schema:schema",
          Id = "id:id",
          AliasCredentials = "aliascredentials:aliascredentials"
        },
        // input : command line
        "content delete schema:schema id:id -c aliascredentials:aliascredentials"
      },
      new object[ ]
      {
        // expected
        new ContentDeleteRequest
        {
          Schema = "schema:schema",
          Id = "id:id",
          AliasCredentials = "aliascredentials:aliascredentials"
        },
        // input : command line
        "delete content schema:schema id:id -c aliascredentials:aliascredentials"
      }
    };

    [Theory]
    [MemberData(nameof(StaticRouter_Verify_SimpleMap_HappyPath_Data))]
    public void StaticRouter_Verify_SimpleMap_HappyPath(ContentDeleteRequest expected, string inCommandLine)
    {
      var cachedNouns = new RouteMetadataBuilderFixture().Build().GetMetadata(typeof(RouteCommandsParser).Assembly);
      var router = new StaticCommandLineRouterFixture()
      {
        WithRouteValidator = new RouteRequestValidatorFixture().Mock()
      }.Build();

      var actual = router.GetOne(cachedNouns.Values, inCommandLine.Split(' '));

      actual.Should().NotBeNull();
      actual.Should().BeAssignableTo<ContentDeleteRequest>();
      actual.Should().BeEquivalentTo(expected);
    }
  }
}