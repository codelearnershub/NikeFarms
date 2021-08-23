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

        public List<StoreAllocation> FeedAllocation(int userId)
        {
            return _dbContext.StoreAllocations.Where(s=> s.ItemType == "Feed" && s.ManagerId == userId).ToList();
        }

        public List<StoreAllocation> MedAllocation(int userId)
        {
            return _dbContext.StoreAllocations.Where(s => s.ItemType == "Medication" && s.ManagerId == userId).ToList();
        }

        public StoreAllocation Update(StoreAllocation storeAllocation)
        {
            _dbContext.StoreAllocations.Update(storeAllocation);
            _dbContext.SaveChanges();
            return storeAllocation;
        }

        public List<StoreAllocation> GetAllStoreAllocations()
        {
            return _dbContext.StoreAllocations.ToList();
        }

        public List<StoreAllocation> GetStoreAllocationsByManagerEmail(string managerEmail)
        {
            return _dbContext.StoreAllocations.Where(s => s.CreatedBy == managerEmail).ToList();
        }

        public List<StoreAllocation> GetStoreAllocationsByRecieverId(int receiverId)
        {
            return _dbContext.StoreAllocations.Where(s => s.ManagerId == receiverId).ToList();
        }
    }
}
