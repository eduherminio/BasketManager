using BasketManager.Api.Client.SwaggerClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasketManager.Api.Client.Wrappers
{
    /// <summary>
    /// BasketManager.Api.Controllers.ItemController client
    /// </summary>
    public interface IITemClient
    {
        Task<ICollection<Item>> Load();

        Task Clear();
    }
}
