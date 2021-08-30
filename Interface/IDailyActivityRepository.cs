using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Interface
{
    public interface IDailyActivityRepository
    {
        public DailyActivity Add(DailyActivity dailyActivity);

        public DailyActivity FindById(int dailyActivityId);

        public List<DailyActivity> GetDailyActivitiesPerFlockId(int flockId);

        public DailyActivity Update(DailyActivity dailyActivity);

        public void Delete(int dailyActivityId);

        public List<DailyActivity> GetAllDailyActivities();

        public DailyActivity GetDailyActivitiesFlockId(int flockId);
    }
}
