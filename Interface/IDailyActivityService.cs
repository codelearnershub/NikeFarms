using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Interface
{
    public interface IDailyActivityService
    {
        public DailyActivity Add(int userId, double feedPerKg, int mortality, int medUsed, int flockId, int storeAllocationFeedId, int storeAllocationMedId);


        public DailyActivity FindById(int id);

        public DailyActivity Update(int dailyId, double feedPerKg, int mortality, int medUsed, int flockId, int storeAllocationFeedId, int storeAllocationMedId);


        public void Delete(int id);
        
    }
}
