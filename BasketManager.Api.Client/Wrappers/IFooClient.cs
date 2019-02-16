using BasketManager.Api.Client.SwaggerClient;
using System.Threading.Tasks;

namespace BasketManager.Api.Client.Wrappers
{
    /// <summary>
    /// BasketManager.Api.Controllers.FooController client
    /// </summary>
    public interface IFooClient
    {
        Task<Foo> Add(Foo foo);

        Task Remove(string fooId);
    }
}
