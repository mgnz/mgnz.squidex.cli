namespace MGNZ.Squidex.CLI
{
  using System;
  using System.Threading.Tasks;

  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.DependencyInjection;

  using Serilog;

  public class Program
  {
    public static async Task<int> Main(string[ ] args)
    {
      try
      {
        var builder = new ConfigurationBuilder();

        builder.AddEnvironmentVariables();
        builder.AddJsonFile("appsettings.json");
        builder.AddJsonFile("appsettings.aut.json");

        var configurationRoot = builder.Build();

        Log.Logger = new LoggerConfiguration()
          .ReadFrom.Configuration(configurationRoot)
          .CreateLogger();

        Log.Information("Starting");

        //var host = CreateHostBuilder(args)
        //  .Build();

        //await host.RunAsync();

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

    //public static IHostBuilder CreateHostBuilder(string[ ] args)
    //{
    //  return new HostBuilder()
    //    .ConfigureHostConfiguration(config =>
    //    {
    //      /* Host configuration */
    //    })
    //    .ConfigureAppConfiguration((hostingContext, config) =>
    //    {
    //      config.AddJsonFile("appsettings.json", true);
    //      config.AddJsonFile("appsettings.aut.json", true);
    //      config.AddEnvironmentVariables();

    //      if (args != null) config.AddCommandLine(args);
    //    })
    //    .ConfigureServices((hostContext, services) =>
    //    {
    //      services.AddOptions();
    //      //services.Configure<AppConfig>(hostContext.Configuration.GetSection("AppConfig"));

    //      //services.AddSingleton<IHostedService, PrintTextToConsoleService>();
    //    })
    //    .ConfigureLogging((hostingContext, logging) =>
    //    {
    //      //logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
    //      //logging.AddConsole();
    //      logging.AddSerilog();
    //    })
    //    .UseSerilog(); // <- Add this line
    //}
  }
}