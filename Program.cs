using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SabisTest.Data;

namespace SabisTest
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var host = BuildWebHost(args);

      SeedDb(host);

      host.Run();
    }

    private static void SeedDb(IWebHost host)
    {
      var scopeFactory = host.Services.GetService<IServiceScopeFactory>();
      using (var scope = scopeFactory.CreateScope())
      {
        var seeder = scope.ServiceProvider.GetService<DataSeeder>();
        seeder.SeedAsync().Wait();
      }
    }
    public static IWebHost BuildWebHost(string[] args) =>
     WebHost.CreateDefaultBuilder(args)
         .ConfigureAppConfiguration(SetupConfiguration)
         .UseStartup<Startup>()
         .Build();
    private static void SetupConfiguration(WebHostBuilderContext ctx, IConfigurationBuilder builder)
    {
      builder.Sources.Clear();

      builder.AddJsonFile("appsettings.json", false, true)
             .AddEnvironmentVariables();
    }
  }//end class
}//end NmeSpace