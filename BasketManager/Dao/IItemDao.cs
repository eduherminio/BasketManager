using BasketManager.Model;
using System.Collections.Generic;

namespace BasketManager.Dao
{
    /// <summary>
    /// Item Data Access Object
    /// </summary>
    public interface IItemDao
    {
        /// <summary>
        /// Loads all items associated to an user
        /// </summary>
        /// <returns></returns>
        ICollection<Item> Load();

        /// <summary>
        /// Associates Session's user and an item
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        TItem Create<TItem>(TItem item)
            where TItem : Item;

        /// <summary>
        /// Removes association between Session's user and an item
        /// </summary>
        /// <param name="itemId"></param>
        void Remove(string itemId);

        /// <summary>
        ///  Removes association between Session's user and an item
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="item"></param>
        void Remove<TItem>(TItem item)
            where TItem : Item;

        /// <summary>
        /// Remove all existing Session's user association with items
        /// </summary>
        void Clear();
    }
}
