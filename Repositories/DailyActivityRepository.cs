using Microsoft.EntityFrameworkCore;
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
        private readonly NikeDbContext _dbContext;

        public DailyActivityRepository(NikeDbContext dbContext)
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

        public List<DailyActivity> GetDailyActivitiesPerFlockId(int flockId)
        {
            return _dbContext.DailyActivities.Include(f => f.Flock).Where(d=> d.FlockId == flockId).OrderByDescending(r => r.CreatedAt).ToList();
        }

        public List<DailyActivity> GetAllDailyActivities()
        {
            return _dbContext.DailyActivities.Include(f => f.Flock).ToList();
        }

        public DailyActivity GetDailyActivitiesFlockId(int flockId)
        {
            var date = DateTime.Now.ToShortDateString();
            return _dbContext.DailyActivities.FirstOrDefault(d => d.FlockId == flockId && d.Date == date);
        }

        public DailyActivity Update(DailyActivity dailyActivity)
        {
            _dbContext.DailyActivities.Update(dailyActivity);
            _dbContext.SaveChanges();
            return dailyActivity;
        }
    }
}
