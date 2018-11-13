namespace MGNZ.Squidex.CLI.Tests.Commands
{
  using System;
  using System.IO;
  using System.Threading.Tasks;

  using FluentAssertions;

  using MGNZ.Squidex.CLI.Common.Commands;
  using MGNZ.Squidex.CLI.Tests.Platform;

  using Xunit;
  [Collection("Sequential Squidex Integration Tests")]
  [Trait("category", "squidex-cli-integration")]
  public class ContentHandlersIntegrationTest : BaseHandlerIntegrationTest
  {
    [Fact]
    public async Task ContentImport_Execute_EndToEnd()
    {
    }

    [Fact]
    public async Task ContentExport_Execute_EndToEnd()
    {
    }

    [Fact]
    public async Task ContentDelete_Execute_EndToEnd()
    {
    }
  }

  public class ContentStories
  {
  }
}