using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
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

        public SalesItemService(ISalesItemRepository salesItemRepository, IUserService userService)
        {
            _salesItemRepository = salesItemRepository;
            _userService = userService;
        }

        public SalesItem Add(int userId, string item, int noOfItem, int stockId, int salesId, decimal pricePerItem)
        {
            var salesItem = new SalesItem
            {
                CreatedBy = _userService.FindById(userId).Email,
                CreatedAt = DateTime.Now,
                Item = item,
                NoOfItem = noOfItem,
                StockId = stockId,
                SalesId = salesId,
                PricePerItem = pricePerItem,
            };

            return _salesItemRepository.Add(salesItem);
        }

        public SalesItem FindById(int id)
        {
            return _salesItemRepository.FindById(id);
        }

        public SalesItem Update(int salesItemId, string item, int noOfItem, int stockId, int salesId, decimal pricePerItem)
        {
            var salesItem = _salesItemRepository.FindById(salesItemId);
            if (salesItem == null)
            {
                return null;
            }

            salesItem.Item = item;
            salesItem.NoOfItem = noOfItem;
            salesItem.StockId = stockId;
            salesItem.SalesId = salesId;
            salesItem.PricePerItem = pricePerItem;
            salesItem.UpdatedAt = DateTime.Now;

            return _salesItemRepository.Update(salesItem);
        }

        public void Delete(int id)
        {
            _salesItemRepository.Delete(id);
        }
    }
}
