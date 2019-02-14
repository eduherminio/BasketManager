using System.IO;
using Microsoft.Extensions.Configuration;

namespace Configuration.Helpers
{
    public static class AppSettingsHelpers
    {
        public static IConfiguration GetConfiguration(bool reloadConfigOnChange = false)
        {
            return GetConfigurationFromFile("appsettings.json", reloadConfigOnChange);
        }

        public static IConfiguration GetConfigurationFromFile(string fileName)
        {
            return GetConfigurationFromFile(fileName, false);
        }

        public static IConfiguration GetConfigurationFromFile(string fileName, bool reloadConfigOnChange)
        {
            return string.IsNullOrWhiteSpace(fileName)
                ? GetConfiguration(reloadConfigOnChange)
                : new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(fileName, optional: false, reloadOnChange: reloadConfigOnChange)
                    .AddEnvironmentVariables()
                .Build();
        }
    }
}
