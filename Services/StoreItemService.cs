using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
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

        public StoreItem Add(int userId, string name, string itemType, double noOfItem, double itemPerKg)
        {
            var storeItem = new StoreItem
            {
                CreatedBy = _userService.FindById(userId).Email,
                CreatedAt = DateTime.Now,
                Name = name,
                ItemType = itemType,
                NoOfItem = noOfItem,
                ItemPerKg = itemPerKg,
            };

            return _storeItemRepository.Add(storeItem);
        }

        public StoreItem FindById(int id)
        {
            return _storeItemRepository.FindById(id);
        }

        public StoreItem Update(int storeItemId, string Name, string itemType, double noOfItem, double itemPerKg)
        {
            var storeItem = _storeItemRepository.FindById(storeItemId);
            if (storeItem == null)
            {
                return null;
            }

            storeItem.Name = Name;
            storeItem.ItemType = itemType;
            storeItem.NoOfItem = noOfItem;
            storeItem.ItemPerKg = itemPerKg;
            storeItem.UpdatedAt = DateTime.Now;

            return _storeItemRepository.Update(storeItem);
        }

        public void Delete(int id)
        {
            _storeItemRepository.Delete(id);
        }
    }
}
