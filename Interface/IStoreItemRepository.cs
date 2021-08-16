using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Interface
{
    public interface IStoreItemRepository
    {
        public StoreItem Add(StoreItem storeItem);

        public StoreItem FindById(int storeItemId);

        public StoreItem Update(StoreItem storeItem);

        public void Delete(int storeItemId);
    }
}
