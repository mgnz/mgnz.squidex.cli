namespace MGNZ.Squidex.CLI.Common.Platform
{
  using System;

  using Serilog;

  public class ConsoleWriter : IConsoleWriter
  {
    private readonly ILogger _logger;

    public ConsoleWriter(ILogger logger)
    {
      _logger = logger;
    }
    public void WriteLine(string messageTemplate, params object[] propertyValues)
    {
      _logger.Verbose(messageTemplate, propertyValues);

      Console.WriteLine(messageTemplate, propertyValues);
    }

    public void WriteLine(string message)
    {
      _logger.Verbose(message);

      Console.WriteLine(message);
    }
  }

  public interface IConsoleWriter
  {
    void WriteLine(string messageTemplate, params object[ ] propertyValues);
    void WriteLine(string message);
  }
}