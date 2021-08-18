using NikeFarms.Context;
using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Repositories
{
    public class StoreAllocationRepository : IStoreAllocationRepository
    {
        private readonly NikeDbContext _dbContext;

        public StoreAllocationRepository(NikeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public StoreAllocation Add(StoreAllocation storeAllocation)
        {
            _dbContext.StoreAllocations.Add(storeAllocation);
            _dbContext.SaveChanges();
            return storeAllocation;
        }

        public void Delete(int storeAllocationId)
        {
            var storeAllocation = FindById(storeAllocationId);

            if (storeAllocation != null)
            {
                _dbContext.StoreAllocations.Remove(storeAllocation);
                _dbContext.SaveChanges();
            }
        }

        public StoreAllocation FindById(int storeAllocationId)
        {
            return _dbContext.StoreAllocations.FirstOrDefault(u => u.Id.Equals(storeAllocationId));
        }

        public StoreAllocation Update(StoreAllocation storeAllocation)
        {
            _dbContext.StoreAllocations.Update(storeAllocation);
            _dbContext.SaveChanges();
            return storeAllocation;
        }
    }
}
