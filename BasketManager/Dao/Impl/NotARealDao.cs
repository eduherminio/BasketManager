using BasketManager.Model;
using Configuration;
using System;
using System.Collections.Generic;

namespace BasketManager.Dao.Impl
{
    /// <summary>
    /// Untested
    /// </summary>
    public class NotARealDao : IItemDao
    {
        private readonly ISession _session;
        private readonly DatabaseFixture _databaseFixture;

        public NotARealDao(ISession session, DatabaseFixture databaseFixture)
        {
            _session = session;
            _databaseFixture = databaseFixture;
        }

        public ICollection<Item> Load()
        {
            return _databaseFixture.LoadItems(_session.Username);
        }

        public TItem Create<TItem>(TItem item)
            where TItem : Item
        {
            item.Id = Guid.NewGuid().ToString();
            _databaseFixture.AddItem(_session.Username, item);

            return item;
        }

        public void Remove(string itemId)
        {
            _databaseFixture.RemoveItem(_session.Username, itemId);
        }

        public void Remove<TItem>(TItem item)
            where TItem : Item
        {
            _databaseFixture.RemoveItem(_session.Username, item);
        }

        public void Clear()
        {
            _databaseFixture.ClearBasket(_session.Username);
        }
    }
}
