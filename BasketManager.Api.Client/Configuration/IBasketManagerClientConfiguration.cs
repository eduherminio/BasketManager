using System;

namespace BasketManager.Api.Client.Configuration
{
    public interface IBasketManagerClientConfiguration
    {
        Uri BasketManagerUri { get; }
    }
}
