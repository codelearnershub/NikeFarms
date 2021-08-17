using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Services
{
    public class FlockTypeService : IFlockTypeService
    {
        private readonly IFlockTypeRepository _flockTypeRepository;
        private readonly IUserService _userService;

        public FlockTypeService(IFlockTypeRepository flockTypeRepository, IUserService userService)
        {
            _flockTypeRepository = flockTypeRepository;
            _userService = userService;
        }

        public FlockType Add(int userId, string name, string description)
        {
            var flockType = new FlockType
            {
                CreatedBy = _userService.FindById(userId).Email,
                CreatedAt = DateTime.Now,
                Name = name,
                Description = description,
            };

            return _flockTypeRepository.Add(flockType);
        }

        public FlockType FindById(int id)
        {
            return _flockTypeRepository.FindById(id);
        }

        public FlockType Update(int flockTypeId, string Name, string description)
        {
            var flockType = _flockTypeRepository.FindById(flockTypeId);
            if (flockType == null)
            {
                return null;
            }

            flockType.Name = Name;
            flockType.Description = description;
            flockType.UpdatedAt = DateTime.Now;

            return _flockTypeRepository.Update(flockType);
        }

        public void Delete(int id)
        {
            _flockTypeRepository.Delete(id);
        }

    }
}
