using System;
using System.Collections.Generic;
using System.Text;

namespace MGNZ.Squidex.CLI.Tests.Platform
{
  using Moq;

  using Serilog;

  public static class SerilogFixture
  {
    public static ILogger UsefullLogger<TFor>()
    {
      return UsefullLogger(typeof(TFor));
    }

    public static ILogger UsefullLogger(Type forType)
    {
      Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .WriteTo.Seq("http://localhost:5341")
        .CreateLogger();

      return Log.ForContext(forType);
    }

    public static ILogger UsefullLogger()
    {
      Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .WriteTo.Seq("http://localhost:5341")
        .CreateLogger();

      return Log.Logger;
    }

    public static Mock<ILogger> MockLogger()
    {
      return new Mock<ILogger>();
    }
  }
}
