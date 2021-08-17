using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
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

        public StoreAllocation Add(int userId, int storeItemId, double noOfItem, double itemPerKg)
        {
            var storeAllocation = new StoreAllocation
            {
                CreatedBy = _userService.FindById(userId).Email,
                CreatedAt = DateTime.Now,
                StoreItemId = storeItemId,
                NoOfItem = noOfItem,
                ItemPerKg = itemPerKg,
            };

            return _storeAllocationRepository.Add(storeAllocation);
        }

        public StoreAllocation FindById(int id)
        {
            return _storeAllocationRepository.FindById(id);
        }

        public StoreAllocation Update(int storeAllocationId, int storeItemId, double noOfItem, double itemPerKg)
        {
            var storeAllocation = _storeAllocationRepository.FindById(storeAllocationId);
            if (storeAllocation == null)
            {
                return null;
            }

            storeAllocation.StoreItemId = storeItemId;
            storeAllocation.NoOfItem = noOfItem;
            storeAllocation.ItemPerKg = itemPerKg;
            storeAllocation.UpdatedAt = DateTime.Now;

            return _storeAllocationRepository.Update(storeAllocation);
        }

        public void Delete(int id)
        {
            _storeAllocationRepository.Delete(id);
        }
    }
}
