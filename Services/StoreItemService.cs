using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
using NikeFarms.v2._0.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Services
{
    public class StoreItemService : IStoreItemService
    {
        private readonly IStoreItemRepository _storeItemRepository;
        private readonly IUserService _userService;

        public StoreItemService(IStoreItemRepository storeItemRepository, IUserService userService)
        {
            _storeItemRepository = storeItemRepository;
            _userService = userService;
        }

        public StoreItem Add(StoreItemDTO storeItemDTO)
        {
            var storeItem = new StoreItem
            {
                CreatedBy = _userService.FindById(storeItemDTO.UserId).Email,
                CreatedAt = DateTime.Now,
                Name = storeItemDTO.Name,
                Description = storeItemDTO.Description,
                ItemType = storeItemDTO.ItemType,
                NoOfItem = storeItemDTO.NoOfItem,
                ItemPerKg = storeItemDTO.ItemPerKg,
                TotalPricePurchased = storeItemDTO.TotalPricePurchased,
            };

            return _storeItemRepository.Add(storeItem);
        }

        public StoreItem FindById(int id)
        {
            return _storeItemRepository.FindById(id);
        }

        public StoreItem Update(int storeItemId, StoreItemDTO storeItemDTO)
        {
            var storeItem = _storeItemRepository.FindById(storeItemId);
            if (storeItem == null)
            {
                return null;
            }

            storeItem.Name = storeItemDTO.Name;
            storeItem.Description = storeItemDTO.Description;
            storeItem.ItemType = storeItemDTO.ItemType;
            storeItem.NoOfItem = storeItemDTO.NoOfItem;
            storeItem.ItemPerKg = storeItemDTO.ItemPerKg;
            storeItem.TotalPricePurchased = storeItemDTO.TotalPricePurchased;
            storeItem.UpdatedAt = DateTime.Now;

            return _storeItemRepository.Update(storeItem);
        }

        public void Delete(int id)
        {
            _storeItemRepository.Delete(id);
        }
    }
}
