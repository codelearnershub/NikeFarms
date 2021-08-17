using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
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

        public DailyActivity Add(int userId, double feedPerKg, int mortality, int medUsed, int flockId, int storeAllocationFeedId, int storeAllocationMedId)
        {
            var dailyActivity = new DailyActivity
            {
                CreatedBy = _userService.FindById(userId).Email,
                CreatedAt = DateTime.Now,
                FeedConsumedPerKg = feedPerKg,
                Mortality = mortality,
                NoOfMedUsed = medUsed,
                FlockId = flockId,
                StoreAllocationFeedId = storeAllocationFeedId,
                StoreAllocationMedId = storeAllocationMedId,

            };

            return _dailyActivityRepository.Add(dailyActivity);
        }

        public DailyActivity FindById(int id)
        {
            return _dailyActivityRepository.FindById(id);
        }

        public DailyActivity Update(int dailyId, double feedPerKg, int mortality, int medUsed, int flockId, int storeAllocationFeedId, int storeAllocationMedId)
        {
            var dailyActivity = _dailyActivityRepository.FindById(dailyId);
            if (dailyActivity == null)
            {
                return null;
            }

            dailyActivity.FeedConsumedPerKg = feedPerKg;
            dailyActivity.Mortality = mortality;
            dailyActivity.NoOfMedUsed = medUsed;
            dailyActivity.FlockId = flockId;
            dailyActivity.StoreAllocationFeedId = storeAllocationFeedId;
            dailyActivity.StoreAllocationMedId = storeAllocationMedId;
            dailyActivity.UpdatedAt = DateTime.Now;

            return _dailyActivityRepository.Update(dailyActivity);
        }

        public void Delete(int id)
        {
            _dailyActivityRepository.Delete(id);
        }
    }
}
