using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prometheus;

namespace LearnPrometheus
{
  public class Startup
  {
    private static readonly Counter DummyRequests = Metrics
      .CreateCounter("dummy_requests_count", "Number of dummy requests.");

    public void ConfigureServices(IServiceCollection services)
    {
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseRouting();
      app.UseHttpMetrics();
      app.UseMetricServer();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapMetrics();
        endpoints.MapGet("/", async context =>
        {
          var configuration = context.RequestServices.GetRequiredService<IConfiguration>();
          var userName = configuration.GetValue<string>("UserName");
          var secret = configuration.GetValue<string>("Secret");

          DummyRequests.Inc();

          await context.Response.WriteAsJsonAsync(new
          {
            message = $"Hello '{userName}'!",
            secret
          });
        });
      });
    }
  }
}
