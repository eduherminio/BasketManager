using System.Collections.Generic;
using BasketManager.Dao;
using BasketManager.Model;
using Configuration.Exceptions;
using Configuration.Logs;

namespace BasketManager.Service.Impl
{
    [Log]
    [ExceptionManagement]
    public class BasketManagerService : IBasketManagerService
    {
        private readonly IItemDao _dao;

        public BasketManagerService(IItemDao dao)
        {
            _dao = dao;
        }

        public TItem Add<TItem>(TItem item)
            where TItem : Item
        {
            return _dao.Create(item);
        }

        public ICollection<Item> Load()
        {
            return _dao.Load();
        }

        public void Remove(string id)
        {
            _dao.Remove(id);
        }

        public void Clear()
        {
            _dao.Clear();
        }
    }
}
