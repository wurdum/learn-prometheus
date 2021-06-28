using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;

namespace LearnPrometheus
{
  public class Program
  {
    public static void Main(string[] args)
    {
      Log.Logger = new LoggerConfiguration()
        .WriteTo.Console(new RenderedCompactJsonFormatter(), LogEventLevel.Debug)
        .CreateBootstrapLogger();

      try
      {
        CreateHostBuilder(args).Build().Run();
      }
      catch (Exception ex)
      {
        Log.Fatal(ex, "An unhandled exception occured during bootstrapping.");
      }
      finally
      {
        Log.CloseAndFlush();
      }
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
      return Host.CreateDefaultBuilder(args)
        .UseSerilog((context, services, configuration) => configuration
          .WriteTo.Console(new RenderedCompactJsonFormatter(), LogEventLevel.Debug)
          .ReadFrom.Configuration(context.Configuration)
          .ReadFrom.Services(services))
        .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }
  }
}
