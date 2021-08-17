using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Interface
{
    public interface IStoreAllocationService
    {
        public StoreAllocation Add(int userId, int storeItemId, double noOfItem, double itemPerKg);

        public StoreAllocation FindById(int id);

        public StoreAllocation Update(int storeAllocationId, int storeItemId, double noOfItem, double itemPerKg);

        public void Delete(int id);
    }
}
