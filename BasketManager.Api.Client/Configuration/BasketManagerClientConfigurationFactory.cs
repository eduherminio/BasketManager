using Microsoft.Extensions.Configuration;
using System;

namespace BasketManager.Api.Client.Configuration
{
    public static class BasketManagerClientConfigurationFactory
    {
        /// <summary>
        /// Configuration Key to be set with BasketManager Uri
        /// </summary>
        public static string BasketManagerUriKey { get; } = "BasketManagerUri";

        public static IBasketManagerClientConfiguration CreateFromIConfiguration(IConfiguration configuration)
        {
            return BasketManagerClientConfiguration.CreateFromValues(new Uri(configuration[BasketManagerUriKey]));
        }
    }
}
