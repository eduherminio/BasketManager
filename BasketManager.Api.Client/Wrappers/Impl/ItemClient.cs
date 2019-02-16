using BasketManager.Api.Client.Configuration;
using BasketManager.Api.Client.SwaggerClient;
using Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasketManager.Api.Client.Wrappers.Impl
{
    [Wrapper(typeof(IITemClient))]
    public class ItemClient : BaseClient, IITemClient
    {
        public ItemClient(IBasketManagerClientConfiguration configuration, BasketManagerHttpClientWrapper httpClient, ISession session)
            : base(configuration, httpClient, session)
        {
        }

        public async Task Clear()
        {
            await Client.ClearAsync().ConfigureAwait(false);
        }

        public Task<ICollection<Item>> Load()
        {
            return Client.LoadAsync();
        }
    }
}
