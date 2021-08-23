using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Interface
{
    public interface ISalesItemRepository
    {
        public SalesItem Add(SalesItem salesItem);

        public SalesItem FindById(int salesItemId);

        public SalesItem Update(SalesItem salesItem);

        public List<SalesItem> GetAllSalesItem();

        public List<SalesItem> GetSalesItemBySalesId(int salesId);

        public void Delete(int salesItemId);
    }
}
