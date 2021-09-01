using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
using NikeFarms.v2._0.Models.DTO;
using NikeFarms.v2._0.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Controllers
{
    public class WeeklyReportController : Controller
    {
        private readonly IUserService _userService;
        private readonly IWeeklyReportService _weeklyReportService;
        private readonly IFlockService _flockService;
        private readonly IFlockTypeService _flockTypeService;

        public WeeklyReportController(IUserService userService, IWeeklyReportService weeklyReportService, IFlockService flockService, IFlockTypeService flockTypeService)
        {
            _userService = userService;
            _weeklyReportService = weeklyReportService;
            _flockService = flockService;
            _flockTypeService = flockTypeService;
        }

        public IActionResult Index()
        {

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";


            var weekly = _weeklyReportService.GetWeeklyReport();
            List<ListWeeklyReportVM> ListWeeklyReport = new List<ListWeeklyReportVM>();
            foreach (var week in weekly)
            {
                var Created = _userService.FindByEmail(week.CreatedBy);

                ListWeeklyReportVM listWeeklyReportVM = new ListWeeklyReportVM
                {
                    Id = week.Id,
                    CreatedBy = $"{Created.FirstName} .{Created.LastName[0]}",
                    AverageWeight = week.AverageWeight,
                    CreatedAt = week.Date,
                    Flock = $"{_flockService.FindById(week.FlockId).FlockType.Name} Batch No: {_flockService.FindById(week.FlockId).BatchNo}",
                };

                ListWeeklyReport.Add(listWeeklyReportVM);
            }

            return View(ListWeeklyReport);
        }

        public IActionResult AddWeeklyReport()
        {
            
            AddWeeklyReportVM weeklyReportVM = new AddWeeklyReportVM
            {
                FlockList = _flockService.GetAllFlocks().Select(m => new SelectListItem
                {
                    Text = $"{_flockTypeService.FindById(m.FlockTypeId).Name} Batch No: {m.BatchNo}",
                    Value = m.Id.ToString()
                })
            };

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

            return View(weeklyReportVM);
        }

        [HttpPost]
        public IActionResult AddWeeklyReport(AddWeeklyReportVM addWeeklyReport)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            WeeklyReportDTO weeklyReportDTO = new WeeklyReportDTO
            {
                UserId = userId,
                AverageWeight = addWeeklyReport.AverageWeight,
                FlockId = addWeeklyReport.FlockId,
            };


           if( _weeklyReportService.Add(weeklyReportDTO) == null)
            {
                ViewBag.Message = "Error";
                AddWeeklyReportVM weeklyReportVM = new AddWeeklyReportVM
                {
                    FlockList = _flockService.GetAllFlocks().Select(m => new SelectListItem
                    {
                        Text = $"{_flockTypeService.FindById(m.FlockTypeId).Name} Batch No: {m.BatchNo}",
                        Value = m.Id.ToString()
                    })
                };

               
                User userlogin = _userService.FindById(userId);
                ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

                return View(weeklyReportVM);
            }
            else
            {
                _weeklyReportService.Add(weeklyReportDTO);
            }
            return RedirectToAction("Index");
        }


        public IActionResult UpdateWeeklyReport(int id)
        {
            var weeklyReport = _weeklyReportService.FindById(id);
            if (weeklyReport == null || weeklyReport.Date != DateTime.Now.ToShortDateString())
            {
                return NotFound();
            }
            else
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
                User userlogin = _userService.FindById(userId);
                ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

                UpdateWeeklyReportVM updateWeeklyReport = new UpdateWeeklyReportVM
                {
                    Id = weeklyReport.Id,
                    AverageWeight = weeklyReport.AverageWeight,
                    SelectedFlock = $"{_flockService.FindById(weeklyReport.FlockId).FlockType.Name} Batch No: {_flockService.FindById(weeklyReport.FlockId).BatchNo}",
                };

                return View(updateWeeklyReport);
            }

        }

        [HttpPost]
        public IActionResult UpdateWeeklyReport(UpdateWeeklyReportVM updateWeeklyReport)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            WeeklyReportDTO weeklyReport = new WeeklyReportDTO
            {
                Id = updateWeeklyReport.Id,
                AverageWeight = updateWeeklyReport.AverageWeight,
            };

            _weeklyReportService.Update(weeklyReport);
            return RedirectToAction("Index");
        }



        //public IActionResult Delete(int id)
        //{
        //    var weeklyReport = _weeklyReportService.FindById(id);
        //    if (weeklyReport == null)
        //    {
        //        return NotFound();
        //    }
        //    _weeklyReportService.Delete(id);
        //    return RedirectToAction("Index");
        //}
    }
}
