using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Interface
{
    public interface IStockService
    {
        public Stock Add(int userId, string item, double noOfItem, double availableItem, decimal? pricePerCrate, decimal? pricePerKg, int flockId);

        public Stock FindById(int id);

        public Stock Update(int stockId, string item, double noOfItem, double availableItem, decimal? pricePerCrate, decimal? pricePerKg, int flockId);

        public void Delete(int id);
    }
}
