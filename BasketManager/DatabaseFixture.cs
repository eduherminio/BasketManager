using BasketManager.Model;
using Configuration.Exceptions;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace BasketManager
{
    public class DatabaseFixture
    {
        private readonly ConcurrentDictionary<string, ICollection<Item>> _db;

        public DatabaseFixture()
        {
            _db = new ConcurrentDictionary<string, ICollection<Item>>();
        }

        public ICollection<Item> LoadItems(string username)
        {
            if (_db.TryGetValue(username, out ICollection<Item> items))
            {
                return items;
            }
            else
            {
                _db[username] = new List<Item>();
                return _db[username];
            }
        }

        public void AddItem(string username, Item item)
        {
            if (_db.Keys.Contains(username))
            {
                _db[username].Add(item);
            }
            else
            {
                _db[username] = new HashSet<Item> { item };
            }
        }

        public void RemoveItem(string username, Item item)
        {
            if (_db.Keys.Contains(username))
            {
                _db[username].Remove(item);
            }
            else
            {
                throw new DatabaseException($"Error deleting item {item.Id}, since it doesn't exists for User {username}");
            }
        }

        public void RemoveItem(string username, string itemId)
        {
            if (_db.Keys.Contains(username))
            {
                _db[username].Remove(_db[username].SingleOrDefault(i => itemId == i.Id));
            }
            else
            {
                throw new DatabaseException($"Error deleting item {itemId}, since it doesn't exists for User {username}");
            }
        }

        public void ClearBasket(string username)
        {
            _db.TryRemove(username, out _);
        }
    }
}
