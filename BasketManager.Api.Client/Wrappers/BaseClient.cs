using BasketManager.Api.Client.Configuration;
using BasketManager.Api.Client.SwaggerClient;
using Configuration;

namespace BasketManager.Api.Client.Wrappers
{
    public abstract class BaseClient
    {
        protected BasketManagerClient Client;

        protected BaseClient(IBasketManagerClientConfiguration configuration, BasketManagerHttpClientWrapper httpClient, ISession session)
        {
            Client = new BasketManagerClient(configuration.BasketManagerUri, httpClient.HttpClient, session);
        }
    }
}
