using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
using NikeFarms.v2._0.Models.DTO;
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

        public FlockType Add(FlockTypeDTO flockTypeDTO)
        {
            var flockType = new FlockType
            {
                CreatedBy = _userService.FindById(flockTypeDTO.UserId).Email,
                CreatedAt = DateTime.Now,
                Name = flockTypeDTO.Name,
                Description = flockTypeDTO.Description,
            };

            return _flockTypeRepository.Add(flockType);
        }

        public IEnumerable<FlockType> GetAllFlockTypes()
        {
            return _flockTypeRepository.GetAllFlockTypes();
        }

        public FlockType FindById(int id)
        {
            return _flockTypeRepository.FindById(id);
        }

        public FlockType Update(FlockTypeDTO flockTypeDTO)
        {
            var flockType = _flockTypeRepository.FindById(flockTypeDTO.Id);
            if (flockType == null)
            {
                return null;
            }

            flockType.Name = flockTypeDTO.Name;
            flockType.Description = flockTypeDTO.Description;
            flockType.UpdatedAt = DateTime.Now;

            return _flockTypeRepository.Update(flockType);
        }

        public void Delete(int id)
        {
            _flockTypeRepository.Delete(id);
        }

        public FlockType FindByName(string name)
        {
            return _flockTypeRepository.FindByName(name);
        }
    }
}
