using Microsoft.AspNetCore.Authorization;
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
    
    public class FlockController : Controller
    {
        private readonly IUserService _userService;
        private readonly IFlockService _flockService;
        private readonly IWeeklyReportService _weeklyReportService;
        private readonly IFlockTypeService _flockTypeService;
        private readonly ISalesItemService _salesItemService;
        private readonly IUserRoleService _userRoleService;
        private readonly IRoleService _roleService;
        private readonly INotificationService _notificationService;

        public FlockController(IUserService userService, IFlockService flockService, IFlockTypeService flockTypeService, IUserRoleService userRoleService, INotificationService notificationService, IRoleService roleService, IWeeklyReportService weeklyReportService, ISalesItemService salesItemService)
        {
            _userService = userService;
            _flockService = flockService;
            _flockTypeService = flockTypeService;
            _userRoleService = userRoleService;
            _notificationService = notificationService;
            _roleService = roleService;
            _weeklyReportService = weeklyReportService;
            _salesItemService = salesItemService;
        }

        [Authorize(Roles = "Super Admin, Store Manager")]
        public IActionResult Index()
        {
            var flocks = _flockService.GetAllFlocks();
            List<ListFlockVM> ListFlock = new List<ListFlockVM>();
            foreach (var flock in flocks)
            {
                var Created = _userService.FindByEmail(flock.CreatedBy);

                ListFlockVM listFlockVM = new ListFlockVM
                {
                    Id = flock.Id,
                    BatchNo = flock.BatchNo,
                    FlockType = _flockTypeService.FindById(flock.FlockTypeId).Name,
                    currentAge = (int)((DateTime.Now - flock.CreatedAt).TotalDays) + flock.Age,
                    TotalNo = flock.TotalNo,
                    AvailableBirds = flock.AvailableBirds,
                    CurrentAverageWeight = _flockService.GetCurrentAverageWeight(flock.Id),
                    Mortality = _flockService.Mortality(flock.Id),
                    IsApproved = flock.IsApproved,
                    CreatedBy = $"{Created.FirstName} .{Created.LastName[0]}",
                };

                ListFlock.Add(listFlockVM);
            }

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

            return View(ListFlock);
        }

        [Authorize(Roles = "Super Admin, Store Manager")]
        public IActionResult AddFlock()
        {
            AddFlockVM flockVM = new AddFlockVM
            {
                FlockTypeList = _flockTypeService.GetAllFlockTypes().Select(m => new SelectListItem
                {
                    Text = m.Name,
                    Value = m.Id.ToString()
                })
            };

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

            return View(flockVM);
        }

        [HttpPost]
        public IActionResult AddFlock(AddFlockVM addFlock)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            
            FlockDTO flockDTO = new FlockDTO
            {
                UserId = userId,
                TotalNo = addFlock.TotalNo,
                Age = addFlock.Age,
                AverageWeight = addFlock.AverageWeight,
                AvailableBird = addFlock.TotalNo,
                FlockTypeId = addFlock.FlockTypeId,
                AmountPurchased = addFlock.AmountPurchased,
                IsApproved = false,
            };

            _flockService.Add(flockDTO);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Super Admin, Store Manager")]
        public IActionResult UpdateFlock(int id)
        {
            var flock = _flockService.FindById(id);
            if (flock == null || flock.IsApproved == true)
            {
                return NotFound();
            }
            else
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
                User userlogin = _userService.FindById(userId);
                ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

                UpdateFlockVM updateFlock = new UpdateFlockVM
                {
                    Id = flock.Id,
                    TotalNo = flock.TotalNo,
                    AverageWeight = flock.AverageWeight,
                    Age = flock.Age,
                    AmountPurchased = flock.AmountPurchased,
                    FlockTypeId = flock.FlockTypeId,
                    FlockTypeList = _flockTypeService.GetAllFlockTypes().Select(m => new SelectListItem
                    {
                        Text = m.Name,
                        Value = m.Id.ToString(),
                    }),
                };

                return View(updateFlock);
            }

        }

        [HttpPost]
        public IActionResult UpdateFlock(UpdateFlockVM updateFlock)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = _userService.FindById(userId);

            FlockDTO flock = new FlockDTO
            {
                UserId = userId,
                Id = updateFlock.Id,
                TotalNo = updateFlock.TotalNo,
                AverageWeight = updateFlock.AverageWeight,
                Age = updateFlock.Age,
                AvailableBird = updateFlock.TotalNo,
                AmountPurchased = updateFlock.AmountPurchased,
                FlockTypeId = updateFlock.FlockTypeId,
                IsApproved = false,
            };
            _flockService.Update(flock);
            
            return RedirectToAction("Index");
        }


        [Authorize(Roles = "Super Admin, Store Manager")]
        public IActionResult Delete(int id)
        {
            var flock = _flockService.FindById(id);
            if (flock == null || flock.IsApproved == true)
            {
                return NotFound();
            }
            _flockService.Delete(id);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Super Admin, Admin")]
        public IActionResult ListApprovedFlock()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var flocks = _flockService.GetApprovedFlocks();
            List<ListFlockVM> ListFlock = new List<ListFlockVM>();
            foreach (var flock in flocks)
            {
                var Created = _userService.FindByEmail(flock.CreatedBy);

                ListFlockVM listFlockVM = new ListFlockVM
                {
                    Id = flock.Id,
                    BatchNo = flock.BatchNo,
                    FlockType = _flockTypeService.FindById(flock.FlockTypeId).Name,
                    currentAge = (int)((DateTime.Now - flock.CreatedAt).TotalDays) + flock.Age,
                    TotalNo = flock.TotalNo,
                    AvailableBirds = flock.AvailableBirds,
                    InitialAverageWeight = flock.AverageWeight,
                    CurrentAverageWeight = _flockService.GetCurrentAverageWeight(flock.Id),
                    IsApproved = flock.IsApproved,
                    Mortality = _flockService.Mortality(flock.Id),
                    CreatedBy = $"{Created.FirstName} .{Created.LastName[0]}",
                    PriceSpentOnFeed = _flockService.GetAmountSpentOnFlockFeed(flock.Id),
                    PriceSpentOnMed = _flockService.GetAmountSpentOnFlockMed(flock.Id),
                    NoOfBirdsSold = _salesItemService.TotalNoOfBirdSoldPerFlock(flock.Id),
                    AmountPurchased = _salesItemService.AmountOfSalesPerFlock(flock.Id),
                };

                ListFlock.Add(listFlockVM);
            }

            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

            return View(ListFlock);
        }

        [Authorize(Roles = "Super Admin, Admin")]
        public IActionResult Details(int id)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

            var flock = _flockService.FindById(id);
            var Created = _userService.FindByEmail(flock.CreatedBy);

            ListFlockVM listFlockVM = new ListFlockVM
            {
                Id = flock.Id,
                BatchNo = flock.BatchNo,
                FlockType = _flockTypeService.FindById(flock.FlockTypeId).Name,
                currentAge = (int)((DateTime.Now - flock.CreatedAt).TotalDays) + flock.Age,
                TotalNo = flock.TotalNo,
                AvailableBirds = flock.AvailableBirds,
                InitialAverageWeight = flock.AverageWeight,
                CurrentAverageWeight = _flockService.GetCurrentAverageWeight(flock.Id),
                IsApproved = flock.IsApproved,
                CreatedBy = $"{Created.FirstName} .{Created.LastName[0]}",
                CreatedAt = flock.CreatedAt,
                Mortality = _flockService.Mortality(flock.Id),
                PriceSpentOnFeed = _flockService.GetAmountSpentOnFlockFeed(flock.Id),
                PriceSpentOnMed = _flockService.GetAmountSpentOnFlockMed(flock.Id),
                TotalKgOfFeedConsumed = _flockService.GetTotalWeightOfFlockFeed(flock.Id),
                NoOfBirdsSold = _salesItemService.TotalNoOfBirdSoldPerFlock(flock.Id),
                AmountPurchased = _salesItemService.AmountOfSalesPerFlock(flock.Id),
            };

            return View(listFlockVM);
        }
    }
}
