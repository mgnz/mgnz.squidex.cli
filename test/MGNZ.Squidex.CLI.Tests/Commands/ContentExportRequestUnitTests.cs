namespace MGNZ.Squidex.CLI.Tests.Commands
{
  using System.Collections.Generic;

  using FluentAssertions;

  using MGNZ.Squidex.CLI.Common.Commands;
  using MGNZ.Squidex.CLI.Common.Routing;
  using MGNZ.Squidex.CLI.Tests.Routing;

  using Xunit;

  [Trait("category", "working")]
  public class ContentExportRequestUnitTests
  {
    // [Option("sc", "schema", required: true, ordanalityOrder: 1)] public string Schema { get; set; }
    // [Option("p", "path", required: true, ordanalityOrder: 2)] public string Path { get; set; }
    // [Option("a", "all")] public string All { get; set; }
    // [Option("t", "top")] public string Top { get; set; }
    // [Option("s", "skip")] public string Skip { get; set; }
    // [Option("ob", "order-by")] public string OrderBy { get; set; }
    // [Option("qb", "query-by")] public string QueryBy { get; set; }
    // [Option("fb", "filter-by")] public string FilterBy { get; set; }
    // [Option("c", "alias-credentials")] public string AliasCredentials { get; set; }

    public static List<object[ ]> StaticRouter_Verify_SimpleMap_HappyPath_Data => new List<object[ ]>
    {
      new object[ ]
      {
        // expected
        new ContentExportRequest
        {
          Schema = "schema:schema",
          Path = "path:path",
          All = "all:all",
          Top = "top:top",
          Skip = "skip:skip",
          OrderBy = "orderby:orderby",
          QueryBy = "queryby:queryby",
          FilterBy = "filterby:filterby",
          AliasCredentials = "aliascredentials:aliascredentials"
        },
        // input : command line
        "content export schema:schema path:path -a all:all -t top:top -s skip:skip -ob orderby:orderby -qb queryby:queryby -fb filterby:filterby -c aliascredentials:aliascredentials"
      },
      new object[ ]
      {
        // expected
        new ContentExportRequest
        {
          Schema = "schema:schema",
          Path = "path:path",
          All = "all:all",
          Top = "top:top",
          Skip = "skip:skip",
          OrderBy = "orderby:orderby",
          QueryBy = "queryby:queryby",
          FilterBy = "filterby:filterby",
          AliasCredentials = "aliascredentials:aliascredentials"
        },
        // input : command line
        "export content schema:schema path:path --all all:all --top top:top --skip skip:skip --order-by orderby:orderby --query-by queryby:queryby --filter-by filterby:filterby --alias-credentials aliascredentials:aliascredentials"
      }
    };

    [Theory]
    [MemberData(nameof(StaticRouter_Verify_SimpleMap_HappyPath_Data))]
    public void StaticRouter_Verify_SimpleMap_HappyPath(ContentExportRequest expected, string inCommandLine)
    {
      var cachedNouns = new RouteMetadataBuilderFixture().Build().GetMetadata(typeof(RouteCommandsParser).Assembly);
      var router = new StaticCommandLineRouterFixture()
      {
        WithRouteValidator = new RouteRequestValidatorFixture().Mock()
      }.Build();

      var actual = router.GetOne(cachedNouns.Values, inCommandLine.Split(' '));

      actual.Should().NotBeNull();
      actual.Should().BeAssignableTo<ContentExportRequest>();
      actual.Should().BeEquivalentTo(expected);
    }
  }
}