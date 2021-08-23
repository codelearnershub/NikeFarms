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

        public SalesItemService(ISalesItemRepository salesItemRepository, IUserService userService)
        {
            _salesItemRepository = salesItemRepository;
            _userService = userService;
        }

        public SalesItem Add(SalesItemDTO salesItemDTO)
        {
            var salesItem = new SalesItem
            {
                CreatedBy = _userService.FindById(salesItemDTO.UserId).Email,
                CreatedAt = DateTime.Now,
                Item = salesItemDTO.Item,
                NoOfItem = salesItemDTO.NoOfItem,
                StockId = salesItemDTO.StockId,
                SalesId = salesItemDTO.SalesId,
                PricePerItem = salesItemDTO.PricePerItem,
                
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
    }
}
