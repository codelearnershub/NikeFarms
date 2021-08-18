using NikeFarms.Context;
using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Repositories
{
    public class WeeklyReportRepository : IWeeklyReportRepository
    {
        private readonly NikeDbContext2 _dbContext;

        public WeeklyReportRepository(NikeDbContext2 dbContext)
        {
            _dbContext = dbContext;
        }

        public WeeklyReport Add(WeeklyReport weeklyReport)
        {
            _dbContext.WeeklyReports.Add(weeklyReport);
            _dbContext.SaveChanges();
            return weeklyReport;
        }

        public void Delete(int weeklyReportId)
        {
            var weeklyReport = FindById(weeklyReportId);

            if (weeklyReport != null)
            {
                _dbContext.WeeklyReports.Remove(weeklyReport);
                _dbContext.SaveChanges();
            }
        }

        public WeeklyReport FindById(int weeklyReportId)
        {
            return _dbContext.WeeklyReports.FirstOrDefault(u => u.Id.Equals(weeklyReportId));
        }

        public WeeklyReport Update(WeeklyReport weeklyReport)
        {
            _dbContext.WeeklyReports.Update(weeklyReport);
            _dbContext.SaveChanges();
            return weeklyReport;
        }
    }
}
