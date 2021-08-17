using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Services
{
    public class WeeklyReportService : IWeeklyReportService
    {
        private readonly IWeeklyReportRepository _weeklyReportRepository;
        private readonly IUserService _userService;

        public WeeklyReportService(IWeeklyReportRepository weeklyReportRepository, IUserService userService)
        {
            _weeklyReportRepository = weeklyReportRepository;
            _userService = userService;
        }

        public WeeklyReport Add(int userId, double averageWeight, int flockId)
        {
            var weeklyReport = new WeeklyReport
            {
                CreatedBy = _userService.FindById(userId).Email,
                CreatedAt = DateTime.Now,
                AverageWeight = averageWeight,
                FlockId = flockId,
            };

            return _weeklyReportRepository.Add(weeklyReport);
        }

        public WeeklyReport FindById(int id)
        {
            return _weeklyReportRepository.FindById(id);
        }

        public WeeklyReport Update(int weeklyReporteId, double averageWeight, int flockId)
        {
            var weeklyReport = _weeklyReportRepository.FindById(weeklyReporteId);
            if (weeklyReport == null)
            {
                return null;
            }

            weeklyReport.AverageWeight = averageWeight;
            weeklyReport.FlockId = flockId;
            weeklyReport.UpdatedAt = DateTime.Now;

            return _weeklyReportRepository.Update(weeklyReport);
        }

        public void Delete(int id)
        {
            _weeklyReportRepository.Delete(id);
        }
    }
}
