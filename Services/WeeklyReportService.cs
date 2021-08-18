using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
using NikeFarms.v2._0.Models.DTO;
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

        public WeeklyReport Add(WeeklyReportDTO weeklyReportDTO)
        {
            var weeklyReport = new WeeklyReport
            {
                CreatedBy = _userService.FindById(weeklyReportDTO.UserId).Email,
                CreatedAt = DateTime.Now,
                AverageWeight = weeklyReportDTO.AverageWeight,
                FlockId = weeklyReportDTO.FlockId,
            };

            return _weeklyReportRepository.Add(weeklyReport);
        }

        public WeeklyReport FindById(int id)
        {
            return _weeklyReportRepository.FindById(id);
        }

        public WeeklyReport Update(int weeklyReporteId, WeeklyReportDTO weeklyReportDTO)
        {
            var weeklyReport = _weeklyReportRepository.FindById(weeklyReporteId);
            if (weeklyReport == null)
            {
                return null;
            }

            weeklyReport.AverageWeight = weeklyReportDTO.AverageWeight;
            weeklyReport.FlockId = weeklyReportDTO.FlockId;
            weeklyReport.UpdatedAt = DateTime.Now;

            return _weeklyReportRepository.Update(weeklyReport);
        }

        public void Delete(int id)
        {
            _weeklyReportRepository.Delete(id);
        }
    }
}
