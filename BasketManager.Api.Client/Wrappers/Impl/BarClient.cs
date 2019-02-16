using System.Threading.Tasks;
using BasketManager.Api.Client.Configuration;
using BasketManager.Api.Client.SwaggerClient;
using Configuration;

namespace BasketManager.Api.Client.Wrappers.Impl
{
    [Wrapper(typeof(IBarClient))]
    public class BarClient : BaseClient, IBarClient
    {
        public BarClient(IBasketManagerClientConfiguration configuration, BasketManagerHttpClientWrapper httpClient, ISession session)
            : base(configuration, httpClient, session)
        {
        }

        public Task<Bar> Add(Bar bar)
        {
            return Client.AddAsync(bar);
        }

        public async Task Remove(string barId)
        {
            await Client.RemoveAsync(barId).ConfigureAwait(false);
        }
    }
}
