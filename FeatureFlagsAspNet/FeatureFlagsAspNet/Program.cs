using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FeatureFlagsAspNet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(config =>
                {
                    var configuration = config.Build();
                    config.AddAzureAppConfiguration(azureAppConfig =>
                    {
                        var azureAppConfigConnectionString = configuration["AzureAppConfig:ConnectionString"];
                        azureAppConfig.ConfigureRefresh(refresh =>
                        {
                            refresh.SetCacheExpiration(TimeSpan.FromSeconds(5));
                        });
                        azureAppConfig.UseFeatureFlags();
                        azureAppConfig.Connect(azureAppConfigConnectionString);
                    });
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
