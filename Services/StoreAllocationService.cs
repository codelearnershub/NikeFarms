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

        public StoreAllocationService(IStoreAllocationRepository storeAllocationRepository, IUserService userService)
        {
            _storeAllocationRepository = storeAllocationRepository;
            _userService = userService;
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
                IsApproved = storeAllocationDTO.IsApproved,
            };

            return _storeAllocationRepository.Add(storeAllocation);
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
            storeAllocation.ItemPerKg = storeAllocationDTO.ItemPerKg;
            storeAllocation.ItemType = storeAllocationDTO.ItemType;
            storeAllocation.ItemRemaining = storeAllocation.ItemRemaining;
            storeAllocation.IsApproved = storeAllocationDTO.IsApproved;
            storeAllocation.ManagerId = storeAllocationDTO.ManagerId;
            storeAllocation.UpdatedAt = DateTime.Now;
            

            return _storeAllocationRepository.Update(storeAllocation);
        }

        public void Delete(int id)
        {
            _storeAllocationRepository.Delete(id);
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
    }
}
