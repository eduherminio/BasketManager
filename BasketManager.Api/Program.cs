using System;
using System.Threading.Tasks;
using BasketManager.Api.Constants;
using Configuration.Helpers;
using Configuration.Logs;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace BasketManager.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var logger = LogsExtensions.GetLogger();
            try
            {
                logger.Debug("Starting BasketManager.Api");
                IConfiguration configuration = GetConfiguration();

                Task publicApi = BuildWebHost(args, configuration, AppSettingsKeys.UrlConfigurationKey)
                    .RunAsync();

                Task.WaitAll(publicApi);
            }
            catch (Exception e)
            {
                logger.Error(e, $"Stopped program because of exception: {e.Message}");
                throw;
            }
        }

        private static IConfiguration GetConfiguration()
        {
            return AppSettingsHelpers.GetConfigurationFromFile(AppSettingsKeys.ConfigurationFile);
        }

        public static IWebHost BuildWebHost(string[] args, IConfiguration configuration, string urlKey)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseConfiguration(configuration)
                .UseStartup<Startup>()
                .UseUrls(configuration[urlKey])
                .UseCommonLogs()
                .Build();
        }
    }
}
