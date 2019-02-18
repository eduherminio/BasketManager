using BasketManager.Model;
using Configuration.Exceptions;
using Configuration.Logs;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace BasketManager
{
    [Log]
    public class DatabaseFixture
    {
        private readonly ConcurrentDictionary<string, ICollection<Item>> _db;
        private readonly ILogger _logger;

        public DatabaseFixture(ILogger<DatabaseFixture> logger)
        {
            _db = new ConcurrentDictionary<string, ICollection<Item>>();
            _logger = logger;
        }

        public ICollection<Item> LoadItems(string username)
        {
            _logger.LogInformation($"Logging items for {username}");

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
            _logger.LogInformation($"Adding {item.Id} to {username}'s basket");

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
            _logger.LogInformation($"Removing {itemId} from {username}'s basket");

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
            _logger.LogInformation($"Emptying {username}'s basket");

            _db.TryRemove(username, out _);
        }
    }
}
