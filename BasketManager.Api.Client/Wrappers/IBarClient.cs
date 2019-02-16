using BasketManager.Api.Client.SwaggerClient;
using System.Threading.Tasks;

namespace BasketManager.Api.Client.Wrappers
{
    /// <summary>
    /// BasketManager.Api.Controllers.BarController client
    /// </summary>
    public interface IBarClient
    {
        Task<Bar> Add(Bar bar);

        Task Remove(string barId);
    }
}
