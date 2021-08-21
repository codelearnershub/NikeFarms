using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
using NikeFarms.v2._0.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Services
{
    public class FlockService : IFlockService
    {
        private readonly IFlockRepository _flockRepository;
        private readonly IUserService _userService;

        public FlockService(IFlockRepository flockRepository, IUserService userService)
        {
            _flockRepository = flockRepository;
            _userService = userService;
        }

        public Flock Add(FlockDTO flockDTO)
        {
            var flock = new Flock
            {
                CreatedBy = _userService.FindById(flockDTO.UserId).Email,
                CreatedAt = DateTime.Now,
                FlockTypeId = flockDTO.FlockTypeId,
                BatchNo = Guid.NewGuid().ToString().Substring(0, 6).ToUpper(),
                TotalNo = flockDTO.TotalNo,
                Age = flockDTO.Age,
                AverageWeight = flockDTO.AverageWeight,
            };

            return _flockRepository.Add(flock);
        }

        public IEnumerable<Flock> GetAllFlocks()
        {
            return _flockRepository.GetAllFlocks();
        }

        public Flock FindById(int id)
        {
            return _flockRepository.FindById(id);
        }

        public Flock Update(FlockDTO flockDTO)
        {
            var flock = _flockRepository.FindById(flockDTO.Id);
            if (flock == null)
            {
                return null;
            }

            flock.FlockTypeId = flockDTO.FlockTypeId;
            flock.TotalNo = flockDTO.TotalNo;
            flock.Age = flockDTO.Age;
            flock.AverageWeight = flockDTO.AverageWeight;
            flock.UpdatedAt = DateTime.Now;

            return _flockRepository.Update(flock);
        }

        public void Delete(int id)
        {
            _flockRepository.Delete(id);
        }
    }
}
