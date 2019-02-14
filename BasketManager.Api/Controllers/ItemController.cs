using BasketManager.Model;
using BasketManager.Service;
using Configuration.Jwt;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BasketManager.Api.Controllers
{
    [JwtTokenRequired]
    [Produces("application/json")]
    [Route("api/items")]
    public class ItemController
    {
        private readonly IBasketManagerService _basketManagerService;

        public ItemController(IBasketManagerService basketManagerService)
        {
            _basketManagerService = basketManagerService;
        }

        /// <summary>
        /// Loads all items from the baket
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ICollection<Item> Load()
        {
            return _basketManagerService.Load();
        }

        /// <summary>
        /// Clears all items from the basket
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public void Clear()
        {
            _basketManagerService.Clear();
        }
    }
}
