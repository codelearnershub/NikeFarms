using NikeFarms.v2._0.Models;
using NikeFarms.v2._0.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Interface
{
    public interface IDailyActivityService
    {
        public DailyActivity Add(DailyActivityDTO dailyActivityDTO);


        public DailyActivity FindById(int id);

        public IEnumerable<DailyActivity> GetDailyActivitiesPerFlockId(int flockId);

        public DailyActivity Update(DailyActivityDTO dailyActivityDTO);


        public void Delete(int id);

        public List<DailyActivity> GetAllDailyActivities();

        public DailyActivity GetDailyActivitiesFlockId(int flockId);

    }
}
