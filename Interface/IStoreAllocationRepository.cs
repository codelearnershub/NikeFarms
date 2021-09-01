using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Interface
{
    public interface IStoreAllocationRepository
    {
        public StoreAllocation Add(StoreAllocation storeAllocation);

        public StoreAllocation FindById(int storeAllocationId);

        public StoreAllocation Update(StoreAllocation storeAllocation);

        public StoreAllocation FindByBatchNo(string batchNo);

        public List<StoreAllocation> FeedAllocation(int userId);

        public List<StoreAllocation> MedAllocation(int userId);

        public List<StoreAllocation> GetAllStoreAllocations();

        public List<StoreAllocation> GetStoreAllocationsByManagerEmail(string managerEmail);

        public List<StoreAllocation> GetStoreAllocationsByRecieverId(int receiverId);

        public void Delete(int storeAllocationId);

        public StoreAllocation FindMedById(int? MedAllocationId);
    }
}
