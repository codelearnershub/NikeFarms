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
        private readonly IUserRoleService _userRoleService;
        private readonly IRoleService _roleService;
        private readonly INotificationRepository _notificationRepository;

        public StoreItemService(IStoreItemRepository storeItemRepository, IUserService userService, IUserRoleService userRoleService, IRoleService roleService, INotificationRepository notificationRepository)
        {
            _storeItemRepository = storeItemRepository;
            _userService = userService;
            _userRoleService = userRoleService;
            _roleService = roleService;
            _notificationRepository = notificationRepository;
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
                ItemRemaining = storeItemDTO.ItemRemaining,
                IsApproved = storeItemDTO.IsApproved
            };

            _storeItemRepository.Add(storeItem);
            var user = _userService.FindById(storeItemDTO.UserId);
            var role = _roleService.FindByName("Admin");
            Notification notify = new Notification
            {
                CreatedBy = _userService.FindById(storeItemDTO.UserId).Email,
                CreatedAt = DateTime.Now,
                Type = "StoreItem",
                ApproveId = storeItem.Id,
                Content = $"Manager {user.LastName} {user.FirstName} Bought {storeItemDTO.ItemType}: {storeItemDTO.Name}, " +
                $" Qty: {storeItemDTO.NoOfItem},  Item Per Kg: {storeItemDTO.ItemPerKg} Kg , Price Purchased: N{storeItemDTO.TotalPricePurchased}",
                RecieverId = _userRoleService.FindUserWithParticularRole(role.Id).UserId,

            };
            _notificationRepository.Add(notify);
            return storeItem;
        }

        public StoreItem FindById(int id)
        {
            return _storeItemRepository.FindById(id);
        }

        public StoreItem Update(StoreItemDTO storeItemDTO)
        {
            var storeItem = _storeItemRepository.FindById(storeItemDTO.Id);
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
            storeItem.ItemRemaining = storeItemDTO.ItemRemaining;
            storeItem.IsApproved = storeItemDTO.IsApproved;
            storeItem.UpdatedAt = DateTime.Now;


            var notify = _notificationRepository.FindByApproveId(storeItem.Id);
            if (notify == null)
            {
                return null;
            }
            var user = _userService.FindByEmail(storeItem.CreatedBy);
            var role = _roleService.FindByName("Admin");
            notify.Content = $"Manager {user.LastName} {user.FirstName} Bought {storeItemDTO.ItemType}: {storeItemDTO.Name}, " +
                $" Qty: {storeItemDTO.NoOfItem},  Item Per Kg: {storeItemDTO.ItemPerKg} Kg , Price Purchased: N{storeItemDTO.TotalPricePurchased}";

            _notificationRepository.Update(notify);

            return _storeItemRepository.Update(storeItem);
        }

        public void Delete(int id)
        {
            _storeItemRepository.Delete(id);

            _notificationRepository.Delete(id);


        }

        public IEnumerable<StoreItem> GetAllStoreItems()
        {
            return _storeItemRepository.GetAllStoreItems();
        }

        public IEnumerable<StoreItem> GetStoreItemsByManagerEmail(string managerEmail)
        {
            return _storeItemRepository.GetStoreItemsByManagerEmail(managerEmail);
        }

        public List<StoreItem> GetApprovedStoreItems()
        {
            return _storeItemRepository.GetApprovedStoreItems();
        }
    }
}
