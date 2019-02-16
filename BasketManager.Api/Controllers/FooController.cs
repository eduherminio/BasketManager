using BasketManager.Model;
using BasketManager.Service;
using Configuration.Jwt;
using Microsoft.AspNetCore.Mvc;

namespace BasketManager.Api.Controllers
{
    [JwtTokenRequired]
    [Produces("application/json")]
    [Route("api/foo")]
    public class FooController : BaseItemController<Foo>
    {
        public FooController(IBasketManagerService basketManagerService) : base(basketManagerService) { }
    }
}
