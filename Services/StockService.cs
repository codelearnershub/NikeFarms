using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
using NikeFarms.v2._0.Models.DTO;
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

        

        public Stock Add(StockDTO stockDTO)
        {
            var stock = new Stock
            {
                CreatedBy = _userService.FindById(stockDTO.UserId).Email,
                CreatedAt = DateTime.Now,
                NoOfItem = stockDTO.NoOfItem,
                ItemType = stockDTO.ItemType,
                AvailableItem = stockDTO.AvailableItem,
                PricePerCrate = stockDTO.PricePerCrate,
                PricePerKg = stockDTO.PricePerKg,
                FlockId = stockDTO.FlockId,
            };

            return _stockRepository.Add(stock);
        }

        public Stock FindById(int id)
        {
            return _stockRepository.FindById(id);
        }

        public Stock Update(StockDTO stockDTO)
        {
            var stock = _stockRepository.FindById(stockDTO.Id);
            if (stock == null)
            {
                return null;
            }

            stock.NoOfItem = stock.NoOfItem;
            stock.ItemType = stockDTO.ItemType;
            stock.AvailableItem = stock.AvailableItem;
            stock.PricePerCrate = stockDTO.PricePerCrate;
            stock.PricePerKg = stockDTO.PricePerKg;
            stock.FlockId = stockDTO.FlockId;
            stock.UpdatedAt = DateTime.Now;

            return _stockRepository.Update(stock);
        }

        public Stock UpdateMore(StockDTO stockDTO)
        {
            var stock = _stockRepository.FindById(stockDTO.Id);
            if (stock == null)
            {
                return null;
            }

            stock.NoOfItem = stock.NoOfItem;
            stock.ItemType = stockDTO.ItemType;
            stock.AvailableItem = stockDTO.AvailableItem;
            stock.PricePerCrate = stockDTO.PricePerCrate;
            stock.PricePerKg = stockDTO.PricePerKg;
            stock.FlockId = stockDTO.FlockId;
            stock.UpdatedAt = DateTime.Now;

            return _stockRepository.Update(stock);
        }

        public void Delete(int id)
        {
            _stockRepository.Delete(id);
        }

        public IEnumerable<Stock> GetAllStocks()
        {
            return _stockRepository.GetAllStocks();
        }

        public IEnumerable<Stock> GetStocksByManagerEmail(string managerEmail)
        {
            return _stockRepository.GetStocksByManagerEmail(managerEmail);
        }

        public IEnumerable<Stock> GetStocksByFlockId(int flockId)
        {
            return _stockRepository.GetStocksByFlockId(flockId);
        }

        public IEnumerable<Stock> GetBirdStocks()
        {
            return _stockRepository.GetBirdStocks();
        }

       
    }
}
