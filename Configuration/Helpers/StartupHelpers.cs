using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json.Converters;
using System.IO;

namespace Configuration.Helpers
{
    public static class StartupHelpers
    {
        public static string GetXmlCommentsFilePath(string fileName)
        {
            var basePath = PlatformServices.Default.Application.ApplicationBasePath;

            return Path.Combine(basePath, $"{fileName}.xml");
        }

        public static void AddCustomJsonSerializerSettings(this Newtonsoft.Json.JsonSerializerSettings settings)
        {
            settings.Converters.Add(new StringEnumConverter());
        }
    }
}
