using BasketManager.Model;
using BasketManager.Service;
using Microsoft.AspNetCore.Mvc;

namespace BasketManager.Api.Controllers
{
    /// <summary>
    /// Manages items within a basket
    /// </summary>
    /// <typeparam name="TItem">Any BasketManager.Model.Item</typeparam>
    public class BaseItemController<TItem>
        where TItem : Item
    {
        private readonly IBasketManagerService _basketManagerService;

        public BaseItemController(IBasketManagerService basketManagerService)
        {
            _basketManagerService = basketManagerService;
        }

        /// <summary>
        /// Adds an item to the basket
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        public TItem Add([FromBody] TItem item)
        {
            return _basketManagerService.Add(item);
        }

        /// <summary>
        /// Removes an item from the basket
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Remove(string id)
        {
            _basketManagerService.Remove(id);
        }
    }
}
