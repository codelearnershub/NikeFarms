using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Interface
{
    public interface IStockRepository
    {
        public Stock Add(Stock stock);

        public Stock FindById(int stockId);

        public Stock Update(Stock stock);

        public List<Stock> GetAllStocks();

        public List<Stock> GetBirdStocks();

        public List<Stock> GetStocksByFlockId(int flockId);

        public List<Stock> GetStocksByManagerEmail(string managerEmail);

        public List<Stock> GetAllStocksByFlockId(int flockId);

        public void Delete(int stockId);
    }
}
