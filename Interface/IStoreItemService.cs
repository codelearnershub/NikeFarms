using NikeFarms.v2._0.Models;
using NikeFarms.v2._0.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Interface
{
    public interface IStoreItemService
    {
        public StoreItem Add(StoreItemDTO storeItemDTO);

        public StoreItem FindById(int id);

        public StoreItem Update(StoreItemDTO storeItemDTO);

        public IEnumerable<StoreItem> GetAllStoreItems();

        public IEnumerable<StoreItem> GetStoreItemsByManagerEmail(string managerEmail);

        public void Delete(int id);
    }
}
