using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
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

        public Flock Add(int userId, int flockTypeId, int totalNo, int age, double averageWeight)
        {
            var flock = new Flock
            {
                CreatedBy = _userService.FindById(userId).Email,
                CreatedAt = DateTime.Now,
                FlockTypeId = flockTypeId,
                BatchNo = (Guid.NewGuid()).ToString("000000"),
                TotalNo = totalNo,
                Age = age,
                AverageWeight = averageWeight,
            };

            return _flockRepository.Add(flock);
        }

        public Flock FindById(int id)
        {
            return _flockRepository.FindById(id);
        }

        public Flock Update(int flockId, int flockTypeId, int totalNo, int age, double averageWeight)
        {
            var flock = _flockRepository.FindById(flockId);
            if (flock == null)
            {
                return null;
            }

            flock.FlockTypeId = flockTypeId;
            flock.TotalNo = totalNo;
            flock.Age = age;
            flock.AverageWeight = averageWeight;
            flock.UpdatedAt = DateTime.Now;

            return _flockRepository.Update(flock);
        }

        public void Delete(int id)
        {
            _flockRepository.Delete(id);
        }
    }
}
