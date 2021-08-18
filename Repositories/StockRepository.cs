using NikeFarms.Context;
using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly NikeDbContext2 _dbContext;

        public StockRepository(NikeDbContext2 dbContext)
        {
            _dbContext = dbContext;
        }

        public Stock Add(Stock stock)
        {
            _dbContext.Stocks.Add(stock);
            _dbContext.SaveChanges();
            return stock;
        }

        public void Delete(int stockId)
        {
            var stock = FindById(stockId);

            if (stock != null)
            {
                _dbContext.Stocks.Remove(stock);
                _dbContext.SaveChanges();
            }
        }

        public Stock FindById(int stockId)
        {
            return _dbContext.Stocks.FirstOrDefault(u => u.Id.Equals(stockId));
        }

        public Stock Update(Stock stock)
        {
            _dbContext.Stocks.Update(stock);
            _dbContext.SaveChanges();
            return stock;
        }
    }
}
