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
                NoOfItem = storeAllocationDTO.NoOfItem,
                ItemPerKg = storeAllocationDTO.ItemPerKg,
            };

            return _storeAllocationRepository.Add(storeAllocation);
        }

        public StoreAllocation FindById(int id)
        {
            return _storeAllocationRepository.FindById(id);
        }

        public StoreAllocation Update(int storeAllocationId, StoreAllocationDTO storeAllocationDTO)
        {
            var storeAllocation = _storeAllocationRepository.FindById(storeAllocationId);
            if (storeAllocation == null)
            {
                return null;
            }

            storeAllocation.StoreItemId = storeAllocationDTO.StoreItemId;
            storeAllocation.NoOfItem = storeAllocationDTO.NoOfItem;
            storeAllocation.ItemPerKg = storeAllocationDTO.ItemPerKg;
            storeAllocation.UpdatedAt = DateTime.Now;

            return _storeAllocationRepository.Update(storeAllocation);
        }

        public void Delete(int id)
        {
            _storeAllocationRepository.Delete(id);
        }
    }
}
