using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Interface
{
    public interface IStoreItemService
    {
        public StoreItem Add(int userId, string name, string itemType, double noOfItem, double itemPerKg);

        public StoreItem FindById(int id);

        public StoreItem Update(int storeItemId, string Name, string itemType, double noOfItem, double itemPerKg);

        public void Delete(int id);
    }
}
