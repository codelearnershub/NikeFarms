using NikeFarms.v2._0.Models;
using NikeFarms.v2._0.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Interface
{
    public interface ISalesItemService
    {
        public SalesItem Add(SalesItemDTO salesItemDTO);

        public SalesItem FindById(int id);

        public SalesItem Update(SalesItemDTO salesItemDTO);

        public IEnumerable<SalesItem> GetAllSalesItem();

        public IEnumerable<SalesItem> GetSalesItemBySalesId(int salesId);

        public IEnumerable<SalesItem> GetSalesItemByStockId(int stockId);

        public void Delete(int id);

        public decimal TotalPriceOfSales(int salesId);

        public decimal AmountOfSalesPerFlock(int flockId);


        public int TotalNoOfBirdSoldPerFlock(int flockId);
    }
}
