using NikeFarms.v2._0.Models;
using NikeFarms.v2._0.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Interface
{
    public interface IWeeklyReportService
    {
        public WeeklyReport Add(WeeklyReportDTO weeklyReportDTO);

        public WeeklyReport FindById(int id);

        public WeeklyReport Update(int weeklyReporteId, WeeklyReportDTO weeklyReportDTO);

        public void Delete(int id);
    }
}
