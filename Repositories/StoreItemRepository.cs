using NikeFarms.Context;
using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Repositories
{
    public class StoreItemRepository : IStoreItemRepository
    {
        private readonly NikeDbContext2 _dbContext;

        public StoreItemRepository(NikeDbContext2 dbContext)
        {
            _dbContext = dbContext;
        }

        public StoreItem Add(StoreItem storeItem)
        {
            _dbContext.StoreItems.Add(storeItem);
            _dbContext.SaveChanges();
            return storeItem;
        }

        public void Delete(int storeItemId)
        {
            var storeItem = FindById(storeItemId);

            if (storeItem != null)
            {
                _dbContext.StoreItems.Remove(storeItem);
                _dbContext.SaveChanges();
            }
        }
        public StoreItem FindById(int storeItemId)
        {
            return _dbContext.StoreItems.FirstOrDefault(u => u.Id.Equals(storeItemId));
        }

        public StoreItem Update(StoreItem storeItem)
        {
            _dbContext.StoreItems.Update(storeItem);
            _dbContext.SaveChanges();
            return storeItem;
        }
    }
}
