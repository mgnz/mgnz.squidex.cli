namespace MGNZ.Squidex.CLI.Platform.Logging
{
  using System;

  using Serilog;

  internal class LoggerProvider
  {
    private readonly ILogger _logger;
    private readonly Action _releaseAction;

    public LoggerProvider(ILogger logger = null)
    {
      this._logger = logger ?? Log.Logger;
      if (logger == null)
        this._releaseAction = () => { Log.CloseAndFlush(); };
      else
        this._releaseAction = () => { (this._logger as IDisposable)?.Dispose(); };
    }

    public ILogger GetLogger()
    {
      return this._logger;
    }

    public void Release()
    {
      this._releaseAction();
    }
  }
}