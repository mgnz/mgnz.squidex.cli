namespace MGNZ.Squidex.CLI
{
  using System;
  using System.Collections.Generic;
  using System.Threading.Tasks;

  using Autofac;
  using Autofac.Extensions.DependencyInjection;

  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.DependencyInjection;

  using MGNZ.Squidex.CLI.Platform.Logging;

  using Serilog;

  using LoggerExtensions = Microsoft.Extensions.Logging.LoggerExtensions;

  public class Program
  {
    public static async Task<int> Main(string[ ] args)
    {
      try
      {
        var configurationBuilder = new ConfigurationBuilder();
        configurationBuilder.AddEnvironmentVariables();
        configurationBuilder.AddJsonFile("appsettings.json");
        configurationBuilder.AddJsonFile("appsettings.aut.json", optional: true);
        var configurationRoot = configurationBuilder.Build();

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
        containerBuilder.RegisterType<AcceptsLogViaCtor>().As<IExample>();
        containerBuilder.RegisterType<AcceptsLogViaProperty>().As<IExample>();
        containerBuilder.RegisterType<AcceptsLogViaCtorITypeSafeOldLogger>().As<IExample>();
        // other registrations


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

    public AcceptsLogViaCtor(ILogger log)
    {
      if (log == null) throw new ArgumentNullException("log");
      _log = log;
    }

    public void Show()
    {
      _log.Information("Hello!");
    }
  }

  class AcceptsLogViaCtorITypeSafeOldLogger : IExample
  {
    readonly Microsoft.Extensions.Logging.ILogger<AcceptsLogViaCtorITypeSafeOldLogger> _log;

    public AcceptsLogViaCtorITypeSafeOldLogger(Microsoft.Extensions.Logging.ILogger<AcceptsLogViaCtorITypeSafeOldLogger> log)
    {
      if (log == null) throw new ArgumentNullException("log");
      _log = log;
    }

    public void Show()
    {
      LoggerExtensions.LogInformation(this._log, "and again from AcceptsLogViaCtorTypeSafeOldLogger");
    }
  }

  class AcceptsLogViaProperty : IExample
  {
    public ILogger Log { get; set; }

    public void Show()
    {
      Log.Information("Hello, also!");
    }
  }
}