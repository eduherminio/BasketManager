using BasketManager.Model;
using Configuration.Exceptions;
using Configuration.Jwt;
using Configuration.Logs;
using System.Collections.Generic;

namespace BasketManager.Service
{
    public interface IBasketManagerService
    {
        /// <summary>
        /// Adds an item to user's basket
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="item"></param>
        /// <returns>Added item, with its id</returns>
        [Authorization]
        TItem Add<TItem>(TItem item)
            where TItem : Item;

        /// <summary>
        /// Removes an item from user's basket
        /// </summary>
        /// <param name="id"></param>
        [Authorization]
        void Remove(string id);

        /// <summary>
        /// Loads all items from user's basket
        /// </summary>
        /// <returns></returns>
        [Authorization]
        ICollection<Item> Load();

        /// <summary>
        /// Removes all elements from user's basket
        /// </summary>
        [Authorization]
        void Clear();
    }
}
