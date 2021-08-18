using NikeFarms.Context;
using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Repositories
{
    public class DailyActivityRepository : IDailyActivityRepository
    {
        private readonly NikeDbContext2 _dbContext;

        public DailyActivityRepository(NikeDbContext2 dbContext)
        {
            _dbContext = dbContext;
        }

        public DailyActivity Add(DailyActivity dailyActivity)
        {
            _dbContext.DailyActivities.Add(dailyActivity);
            _dbContext.SaveChanges();
            return dailyActivity;
        }

        public void Delete(int dailyActivityId)
        {
            var dailyActivity = FindById(dailyActivityId);

            if (dailyActivity != null)
            {
                _dbContext.DailyActivities.Remove(dailyActivity);
                _dbContext.SaveChanges();
            }
        }

        public DailyActivity FindById(int dailyActivityId)
        {
            return _dbContext.DailyActivities.FirstOrDefault(u => u.Id.Equals(dailyActivityId));
        }

        public DailyActivity Update(DailyActivity dailyActivity)
        {
            _dbContext.DailyActivities.Update(dailyActivity);
            _dbContext.SaveChanges();
            return dailyActivity;
        }
    }
}
