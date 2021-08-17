using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Services
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;
        private readonly IUserService _userService;

        public StockService(IStockRepository stockRepository, IUserService userService)
        {
            _stockRepository = stockRepository;
            _userService = userService;
        }

        public Stock Add(int userId, string item, double noOfItem, double availableItem, decimal? pricePerCrate, decimal? pricePerKg, int flockId)
        {
            var stock = new Stock
            {
                CreatedBy = _userService.FindById(userId).Email,
                CreatedAt = DateTime.Now,
                Item = item,
                NoOfItem = noOfItem,
                AvailableItem = availableItem,
                PricePerCrate = pricePerCrate,
                PricePerKg = pricePerKg,
                FlockId = flockId,
            };

            return _stockRepository.Add(stock);
        }

        public Stock FindById(int id)
        {
            return _stockRepository.FindById(id);
        }

        public Stock Update(int stockId, string item, double noOfItem, double availableItem, decimal? pricePerCrate, decimal? pricePerKg, int flockId)
        {
            var stock = _stockRepository.FindById(stockId);
            if (stock == null)
            {
                return null;
            }

            stock.Item = item;
            stock.NoOfItem = noOfItem;
            stock.AvailableItem = availableItem;
            stock.PricePerCrate = pricePerCrate;
            stock.PricePerKg = pricePerKg;
            stock.FlockId = flockId;
            stock.UpdatedAt = DateTime.Now;

            return _stockRepository.Update(stock);
        }

        public void Delete(int id)
        {
            _stockRepository.Delete(id);
        }
    }
}
