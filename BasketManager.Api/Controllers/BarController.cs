using BasketManager.Model;
using BasketManager.Service;
using Configuration.Jwt;
using Microsoft.AspNetCore.Mvc;

namespace BasketManager.Api.Controllers
{
    [JwtTokenRequired]
    [Produces("application/json")]
    [Route("api/bar")]
    public class BarController : BaseItemController<Bar>
    {
        public BarController(IBasketManagerService basketManagerService) : base(basketManagerService) { }
    }
}
