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

        public void Delete(int storeAllocationId);
    }
}
