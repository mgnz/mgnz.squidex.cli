namespace MGNZ.Squidex.CLI
{
  using System;
  using System.Collections.Generic;
  using System.Threading.Tasks;

  using Autofac;
  using Autofac.Extensions.DependencyInjection;

  using MGNZ.Squidex.CLI.Common.Configuration;
  using MGNZ.Squidex.CLI.Platform;

  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.DependencyInjection;

  using MGNZ.Squidex.CLI.Platform.Logging;

  using Serilog;

  using LoggerExtensions = Microsoft.Extensions.Logging.LoggerExtensions;

  public class Program
  {
    public static int Main(string[ ] args)
    {
      var configurationBuilder = new ConfigurationBuilder();
      configurationBuilder.AddEnvironmentVariables();
      configurationBuilder.AddJsonFile("appsettings.json");
      configurationBuilder.AddJsonFile("appsettings.aut.json", optional: true);
      var configurationRoot = configurationBuilder.Build();

      var configurationSettings = new ApplicationConfiguration();
      configurationRoot.Bind("squidex-cli", configurationSettings);

      Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(configurationRoot)
        .CreateLogger();

      var serviceCollection = new ServiceCollection();
      serviceCollection.AddLogging(loggingBuilder =>
      {
        loggingBuilder.AddSerilog(dispose: true);
      });

      var containerBuilder = new ContainerBuilder();
      containerBuilder.Populate(serviceCollection);

      containerBuilder.RegisterLogger(logger: Log.Logger, autowireProperties: true);
      containerBuilder.Register(c => configurationSettings).As<ApplicationConfiguration>().SingleInstance();
      containerBuilder.RegisterModule<ApplicationModule>();

      try
      {
        using (var container = containerBuilder.Build())
        {
          var examples = container.Resolve<IEnumerable<IExample>>();
          foreach (var example in examples)
          {
            example.Show();
          }
        }

        Log.Information("Starting");
        Log.Error("woooot");

        return 0;
      }
      catch (Exception ex)
      {
        Log.Fatal(ex, "Terminated unexpectedly");
        return 1;
      }
      finally
      {
        Log.CloseAndFlush();
      }
    }
  }

  interface IExample
  {
    void Show();
  }

  class AcceptsLogViaCtor : IExample
  {
    readonly ILogger _log;
    private readonly ApplicationConfiguration _options;

    public AcceptsLogViaCtor(ILogger log, ApplicationConfiguration options)
    {
      if (log == null) throw new ArgumentNullException(nameof(log));

      _log = log;
      this._options = options;
    }

    public void Show()
    {
      _log.Information("Hello!");
    }
  }

  class AcceptsLogViaCtorITypeSafeOldLogger : IExample
  {
    readonly Microsoft.Extensions.Logging.ILogger<AcceptsLogViaCtorITypeSafeOldLogger> _log;
    private readonly ApplicationConfiguration _options;

    public AcceptsLogViaCtorITypeSafeOldLogger(Microsoft.Extensions.Logging.ILogger<AcceptsLogViaCtorITypeSafeOldLogger> log, ApplicationConfiguration options)
    {
      if (log == null) throw new ArgumentNullException(nameof(log));
      _log = log;
      this._options = options;
    }

    public void Show()
    {
      LoggerExtensions.LogInformation(this._log, "and again from AcceptsLogViaCtorTypeSafeOldLogger");
    }
  }

  class AcceptsLogViaProperty : IExample
  {
    public ApplicationConfiguration Options { get; set; }
    public ILogger Log { get; set; }

    public void Show()
    {
      Log.Information("Hello, also!");
    }
  }
}