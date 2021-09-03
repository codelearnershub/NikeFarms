using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
using NikeFarms.v2._0.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Services
{
    public class SalesItemService : ISalesItemService
    {
        private readonly ISalesItemRepository _salesItemRepository;
        private readonly IUserService _userService;
        private readonly ISalesService _salesService;
        private readonly IFlockService _flockService;
        private readonly IStockService _stockService;

        public SalesItemService(ISalesItemRepository salesItemRepository, IUserService userService, ISalesService salesService = null, IStockService stockService = null, IFlockService flockService = null)
        {
            _salesItemRepository = salesItemRepository;
            _userService = userService;
            _salesService = salesService;
            _stockService = stockService;
            _flockService = flockService;
        }

        public SalesItem Add(SalesItemDTO salesItemDTO)
        {
            decimal? pricePerItem = 0;
            if(_stockService.FindById(salesItemDTO.StockId).PricePerKg == null)
            {
                pricePerItem = _stockService.FindById(salesItemDTO.StockId).PricePerCrate;
            }
            else
            {
                pricePerItem = _stockService.FindById(salesItemDTO.StockId).PricePerKg;
            }
            var salesItem = new SalesItem
            {
                CreatedBy = _userService.FindById(salesItemDTO.UserId).Email,
                CreatedAt = DateTime.Now,
                Item = salesItemDTO.Item,
                NoOfItem = salesItemDTO.NoOfItem,
                StockId = salesItemDTO.StockId,
                SalesId = salesItemDTO.SalesId,
                PricePerItem = (decimal)pricePerItem,

            };

            return _salesItemRepository.Add(salesItem);
        }

        public SalesItem FindById(int id)
        {
            return _salesItemRepository.FindById(id);
        }

        public SalesItem Update(SalesItemDTO salesItemDTO)
        {
            var salesItem = _salesItemRepository.FindById(salesItemDTO.Id);
            if (salesItem == null)
            {
                return null;
            }

            salesItem.Item = salesItemDTO.Item;
            salesItem.NoOfItem = salesItemDTO.NoOfItem;
            salesItem.StockId = salesItemDTO.StockId;
            salesItem.SalesId = salesItemDTO.SalesId;
            salesItem.PricePerItem = salesItemDTO.PricePerItem;
            salesItem.UpdatedAt = DateTime.Now;

            return _salesItemRepository.Update(salesItem);
        }

        public void Delete(int id)
        {
            _salesItemRepository.Delete(id);
        }

        public IEnumerable<SalesItem> GetAllSalesItem()
        {
            return _salesItemRepository.GetAllSalesItem();
        }

        public IEnumerable<SalesItem> GetSalesItemBySalesId(int salesId)
        {
            return _salesItemRepository.GetSalesItemBySalesId(salesId);
        }

        public decimal TotalPriceOfSales(int salesId)
        {
            var salesItem = GetSalesItemBySalesId(salesId);
            decimal overallPrice = 0;
            decimal totalPrice = 0;
            decimal? pricePerItem = 0;
            if (salesItem != null)
            {
                foreach(var sale in salesItem)
                {
                    if (_stockService.FindById(sale.StockId).PricePerKg == null)
                    {
                        pricePerItem = _stockService.FindById(sale.StockId).PricePerCrate;
                        totalPrice = (decimal)pricePerItem  * sale.NoOfItem;
                    }
                    else
                    {
                        pricePerItem = _stockService.FindById(sale.StockId).PricePerKg * (decimal)_flockService.GetCurrentAverageWeight(_stockService.FindById(sale.StockId).FlockId);
                        totalPrice = (decimal)pricePerItem * sale.NoOfItem;
                    }

                    overallPrice += totalPrice;
                }
            }

            return overallPrice;
        }
    }
}
