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

        public IEnumerable<WeeklyReport> GetWeeklyReport();

        public WeeklyReport GetWeeklyReportFlockId(int flockId);

        public WeeklyReport Update(WeeklyReportDTO weeklyReportDTO);

        public void Delete(int id);

        public double GetCurrentAverageWeight(int flockId);

        public List<Flock> OperationWeekly();

    }
}
