using System.Threading.Tasks;
using BasketManager.Api.Client.Configuration;
using BasketManager.Api.Client.SwaggerClient;
using Configuration;

namespace BasketManager.Api.Client.Wrappers.Impl
{
    [Wrapper(typeof(IFooClient))]
    public class FooClient : BaseClient, IFooClient
    {
        public FooClient(IBasketManagerClientConfiguration configuration, BasketManagerHttpClientWrapper httpClient, ISession session)
            : base(configuration, httpClient, session)
        {
        }

        public Task<Foo> Add(Foo foo)
        {
            return Client.Add2Async(foo);
        }

        public async Task Remove(string fooId)
        {
            await Client.Remove2Async(fooId).ConfigureAwait(false);
        }
    }
}
