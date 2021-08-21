using NikeFarms.v2._0.Models;
using NikeFarms.v2._0.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Interface
{
    public interface IStoreAllocationService
    {
        public StoreAllocation Add(StoreAllocationDTO storeAllocationDTO);

        public StoreAllocation FindById(int id);

        public IEnumerable<StoreAllocation> FeedAllocation(int userId);

        public IEnumerable<StoreAllocation> MedAllocation(int userId);

        public StoreAllocation Update(int storeAllocationId, StoreAllocationDTO storeAllocationDTO);

        public void Delete(int id);
    }
}
