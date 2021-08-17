using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Interface
{
    public interface IWeeklyReportService
    {
        public WeeklyReport Add(int userId, double averageWeight, int flockId);

        public WeeklyReport FindById(int id);

        public WeeklyReport Update(int weeklyReporteId, double averageWeight, int flockId);

        public void Delete(int id);
    }
}
