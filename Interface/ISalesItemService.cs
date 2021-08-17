using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Interface
{
    public interface ISalesItemService
    {
        public SalesItem Add(int userId, string item, int noOfItem, int stockId, int salesId, decimal pricePerItem);

        public SalesItem FindById(int id);

        public SalesItem Update(int salesItemId, string item, int noOfItem, int stockId, int salesId, decimal pricePerItem);

        public void Delete(int id);
    }
}
