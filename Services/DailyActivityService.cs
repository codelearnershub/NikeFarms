using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
using NikeFarms.v2._0.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Services
{
    public class DailyActivityService : IDailyActivityService
    {
        private readonly IDailyActivityRepository _dailyActivityRepository;
        private readonly IUserService _userService;

        public DailyActivityService(IDailyActivityRepository dailyActivityRepository, IUserService userService)
        {
            _dailyActivityRepository = dailyActivityRepository;
            _userService = userService;
        }

        public DailyActivity Add(DailyActivityDTO dailyActivityDTO)
        {
            var dailyActivity = new DailyActivity
            {
                CreatedBy = _userService.FindById(dailyActivityDTO.UserId).Email,
                CreatedAt = DateTime.Now,
                NoOfMedUsed = dailyActivityDTO.NoOfMedUsed,
                NoOfFeedUsed = dailyActivityDTO.NoOfFeedUsed,
                FlockId = dailyActivityDTO.FlockId,
                StoreAllocationFeedId = dailyActivityDTO.StoreAllocationFeedId,
                StoreAllocationMedId = dailyActivityDTO.StoreAllocationMedId,
                Date = DateTime.Now.ToShortDateString(),

            };

            return _dailyActivityRepository.Add(dailyActivity);
        }

        public DailyActivity FindById(int id)
        {
            return _dailyActivityRepository.FindById(id);
        }

        public DailyActivity Update(DailyActivityDTO dailyActivityDTO)
        {
            var dailyActivity = _dailyActivityRepository.FindById(dailyActivityDTO.Id);
            if (dailyActivity == null)
            {
                return null;
            }


            dailyActivity.NoOfMedUsed = dailyActivityDTO.NoOfMedUsed;
            dailyActivity.NoOfFeedUsed = dailyActivityDTO.NoOfFeedUsed;
            dailyActivity.FlockId = dailyActivityDTO.FlockId;
            dailyActivity.StoreAllocationFeedId = dailyActivityDTO.StoreAllocationFeedId;
            dailyActivity.StoreAllocationMedId = dailyActivityDTO.StoreAllocationMedId;
            dailyActivity.UpdatedAt = DateTime.Now;
            dailyActivity.Date = dailyActivity.Date;

            return _dailyActivityRepository.Update(dailyActivity);
        }

        public void Delete(int id)
        {
            _dailyActivityRepository.Delete(id);
        }

        public IEnumerable<DailyActivity> GetDailyActivitiesPerFlockId(int flockId)
        {
            return _dailyActivityRepository.GetDailyActivitiesPerFlockId(flockId);
        }

        public List<DailyActivity> GetAllDailyActivities()
        {
            return _dailyActivityRepository.GetAllDailyActivities();
        }

        public DailyActivity GetDailyActivitiesFlockId(int flockId)
        {
            return _dailyActivityRepository.GetDailyActivitiesFlockId(flockId);
        }
    }
}
