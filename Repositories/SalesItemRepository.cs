using NikeFarms.Context;
using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Repositories
{
    public class SalesItemRepository : ISalesItemRepository
    {
        private readonly NikeDbContext2 _dbContext;

        public SalesItemRepository(NikeDbContext2 dbContext)
        {
            _dbContext = dbContext;
        }

        public SalesItem Add(SalesItem salesItem)
        {
            _dbContext.SalesItems.Add(salesItem);
            _dbContext.SaveChanges();
            return salesItem;
        }

        public void Delete(int salesItemId)
        {
            var salesItem = FindById(salesItemId);

            if (salesItem != null)
            {
                _dbContext.SalesItems.Remove(salesItem);
                _dbContext.SaveChanges();
            }
        }

        public SalesItem FindById(int salesItemId)
        {
            return _dbContext.SalesItems.FirstOrDefault(u => u.Id.Equals(salesItemId));
        }

        public SalesItem Update(SalesItem salesItem)
        {
            _dbContext.SalesItems.Update(salesItem);
            _dbContext.SaveChanges();
            return salesItem;
        }
    }
}
