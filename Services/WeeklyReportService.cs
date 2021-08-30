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
        private readonly IFlockService _flockService;

        public WeeklyReportService(IWeeklyReportRepository weeklyReportRepository, IUserService userService, IFlockService flockService)
        {
            _weeklyReportRepository = weeklyReportRepository;
            _userService = userService;
            _flockService = flockService;
        }

        public WeeklyReport Add(WeeklyReportDTO weeklyReportDTO)
        {
            bool check = true;
            var weeklyReports = _weeklyReportRepository.GetWeeklyReportByFlockId(weeklyReportDTO.FlockId);
            foreach (var weekly in weeklyReports)
            {
                var date = DateTime.Now;
                var actualDate = weekly.CreatedAt;
                var result = (int)(date - actualDate).TotalDays;
                if (result < 7)
                {
                    check = false;
                    break;
                }
            }

            if (check)
            {
                var weeklyReport = new WeeklyReport
                {
                    CreatedBy = _userService.FindById(weeklyReportDTO.UserId).Email,
                    CreatedAt = DateTime.Now,
                    AverageWeight = weeklyReportDTO.AverageWeight,
                    FlockId = weeklyReportDTO.FlockId,
                    Date = DateTime.Now.ToShortDateString(),
                };
                return _weeklyReportRepository.Add(weeklyReport);
            }
            else
            {
                return null;
            }


        }

        public WeeklyReport FindById(int id)
        {
            return _weeklyReportRepository.FindById(id);
        }

        public WeeklyReport Update(WeeklyReportDTO weeklyReportDTO)
        {
            var weeklyReport = _weeklyReportRepository.FindById(weeklyReportDTO.Id);
            if (weeklyReport == null)
            {
                return null;
            }

            weeklyReport.AverageWeight = weeklyReportDTO.AverageWeight;
            weeklyReport.FlockId = weeklyReport.FlockId;
            weeklyReport.UpdatedAt = DateTime.Now;
            weeklyReport.Date = weeklyReport.Date;

            return _weeklyReportRepository.Update(weeklyReport);
        }

        public void Delete(int id)
        {
            _weeklyReportRepository.Delete(id);
        }

        public IEnumerable<WeeklyReport> GetWeeklyReport()
        {
            return _weeklyReportRepository.GetWeeklyReport();
        }

        public WeeklyReport GetWeeklyReportFlockId(int flockId)
        {
            return _weeklyReportRepository.GetWeeklyReportFlockId(flockId);
        }

        public List<Flock> OperationWeekly()
        {
            List<Flock> FlockId = new List<Flock>();
            var flocks = _flockService.GetApprovedFlocks();
            foreach (var flock in flocks)
            {
                var weekly = _weeklyReportRepository.GetWeeklyReportFlockId(flock.Id);
                if (weekly == null)
                {
                    FlockId.Add(flock);
                }

            }

            return FlockId;
        }

        public double GetCurrentAverageWeight(int flockId)
        {
            var weekly = _weeklyReportRepository.GetWeeklyReportByFlockId(flockId);
            double CAW = _flockService.FindById(flockId).AverageWeight;
            if (weekly != null)
            {
                foreach (var week in weekly)
                {
                    CAW += week.AverageWeight;

                }
            };
            return CAW;
        }
    }
}
