using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Interface
{
    public interface IWeeklyReportRepository
    {
        public WeeklyReport Add(WeeklyReport weeklyReport);

        public WeeklyReport FindById(int weeklyReportId);

        public List<WeeklyReport> GetWeeklyReport();

        public WeeklyReport GetWeeklyReportFlockId(int flockId);

        public IList<WeeklyReport> GetWeeklyReportByFlockId(int flockId);

        public WeeklyReport Update(WeeklyReport weeklyReport);

        public void Delete(int weeklyReportId);
    }
}
