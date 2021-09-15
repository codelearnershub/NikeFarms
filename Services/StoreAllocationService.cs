using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
using NikeFarms.v2._0.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Services
{
    public class StoreAllocationService : IStoreAllocationService
    {
        private readonly IStoreAllocationRepository _storeAllocationRepository;
        private readonly IUserService _userService;
        private readonly IStoreItemService _storeItemService;
        private readonly INotificationRepository _notificationRepository;

        public StoreAllocationService(IStoreAllocationRepository storeAllocationRepository, IUserService userService, INotificationRepository notificationRepository, IStoreItemService storeItemService)
        {
            _storeAllocationRepository = storeAllocationRepository;
            _userService = userService;
            _notificationRepository = notificationRepository;
            _storeItemService = storeItemService;
        }

        public StoreAllocation Add(StoreAllocationDTO storeAllocationDTO)
        {
            var storeAllocation = new StoreAllocation
            {
                CreatedBy = _userService.FindById(storeAllocationDTO.UserId).Email,
                CreatedAt = DateTime.Now,
                StoreItemId = storeAllocationDTO.StoreItemId,
                ManagerId = storeAllocationDTO.ManagerId,
                NoOfItem = storeAllocationDTO.NoOfItem,
                ItemPerKg = storeAllocationDTO.ItemPerKg,
                ItemType = storeAllocationDTO.ItemType,
                ItemRemaining = storeAllocationDTO.ItemRemaining,
                BatchNo = Guid.NewGuid().ToString().Substring(0, 6).ToUpper(),
                IsApproved = storeAllocationDTO.IsApproved,
            };
            _storeAllocationRepository.Add(storeAllocation);

            var user = _userService.FindById(storeAllocationDTO.UserId);
            Notification notify = new Notification
            {
                CreatedBy = user.Email,
                CreatedAt = DateTime.Now,
                Type = "Allocation",
                ApproveId = _storeAllocationRepository.FindByBatchNo(storeAllocation.BatchNo).Id,
                Content = $"Store Manager {user.LastName} {user.FirstName} Allocated {storeAllocationDTO.ItemType}: {_storeItemService.FindById(storeAllocationDTO.StoreItemId).Name}, " +
                $" Qty: {storeAllocationDTO.NoOfItem},  Item Per Kg: {storeAllocationDTO.ItemPerKg} Kg To You",
                RecieverId = storeAllocationDTO.ManagerId,

            };
            _notificationRepository.Add(notify);
            return storeAllocation;
        }

        public StoreAllocation FindById(int id)
        {
            return _storeAllocationRepository.FindById(id);
        }

        public StoreAllocation Update(StoreAllocationDTO storeAllocationDTO)
        {
            var storeAllocation = _storeAllocationRepository.FindById(storeAllocationDTO.Id);
            if (storeAllocation == null)
            {
                return null;
            }

            storeAllocation.StoreItemId = storeAllocationDTO.StoreItemId;
            storeAllocation.NoOfItem = storeAllocationDTO.NoOfItem;
            storeAllocation.ItemRemaining = storeAllocationDTO.ItemRemaining;
            storeAllocation.IsApproved = storeAllocationDTO.IsApproved;
            storeAllocation.ManagerId = storeAllocation.ManagerId;
            storeAllocation.UpdatedAt = DateTime.Now;


            var notify = _notificationRepository.FindByApproveId(storeAllocation.Id);
            if (notify == null)
            {
                return null;
            }
            var user = _userService.FindById(storeAllocationDTO.UserId);
            notify.Content = $"Store Manager {user.LastName} {user.FirstName} Allocated {storeAllocationDTO.ItemType}: {_storeItemService.FindById(storeAllocationDTO.StoreItemId).Name}, " +
                $" Qty: {storeAllocationDTO.NoOfItem},  Item Per Kg: {storeAllocation.ItemPerKg} Kg To You";

            _notificationRepository.Update(notify);

            return _storeAllocationRepository.Update(storeAllocation);
        }

        public void Delete(int id)
        {
            _storeAllocationRepository.Delete(id);
              _notificationRepository.Delete(id);
           
        }

        public IEnumerable<StoreAllocation> FeedAllocation(int userId)
        {
            return _storeAllocationRepository.FeedAllocation(userId);
        }

        public IEnumerable<StoreAllocation> MedAllocation(int userId)
        {
            return _storeAllocationRepository.MedAllocation(userId);
        }

        public IEnumerable<StoreAllocation> GetAllStoreAllocations()
        {
            return _storeAllocationRepository.GetAllStoreAllocations();
        }

        public IEnumerable<StoreAllocation> GetStoreAllocationsByManagerEmail(string managerEmail)
        {
            return _storeAllocationRepository.GetStoreAllocationsByManagerEmail(managerEmail);
        }

        public IEnumerable<StoreAllocation> GetStoreAllocationsByRecieverId(int receiverId)
        {
            return _storeAllocationRepository.GetStoreAllocationsByRecieverId(receiverId);
        }

        public StoreAllocation FindMedById(int? MedAllocationId)
        {
            return _storeAllocationRepository.FindMedById(MedAllocationId);
        }
    }
}
