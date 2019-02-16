using System;

namespace BasketManager.Api.Client.Configuration
{
    public sealed class BasketManagerClientConfiguration : IBasketManagerClientConfiguration
    {
        public Uri BasketManagerUri { get; private set; }

        private BasketManagerClientConfiguration() { }

        public static IBasketManagerClientConfiguration CreateFromValues(Uri basketManagerUri)
        {
            return new BasketManagerClientConfiguration
            {
                BasketManagerUri = basketManagerUri
            };
        }
    }
}
