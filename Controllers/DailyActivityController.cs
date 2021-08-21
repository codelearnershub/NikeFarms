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
    public class DailyActivityController : Controller
    {
        private readonly IUserService _userService;
        private readonly IFlockService _flockService;
        private readonly IFlockTypeService _flockTypeService;
        private readonly IDailyActivityService _dailyService;
        private readonly IStoreAllocationService _storeAllocationService;
        private readonly IStoreItemService _storeItemService;

        public DailyActivityController(IStoreAllocationService storeAllocationService, IUserService userService, IFlockService flockService, IDailyActivityService dailyService, IFlockTypeService flockTypeService, IStoreItemService storeItemService)
        {
            _storeAllocationService = storeAllocationService;
            _userService = userService;
            _flockService = flockService;
            _dailyService = dailyService;
            _flockTypeService = flockTypeService;
            _storeItemService = storeItemService;
        }

        public IActionResult Index()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";


            return View();
        }

        public IActionResult AddDailyActivity()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

            AddDailyActivityVM dailyActivityVM = new AddDailyActivityVM
            {
                FlockList = _flockService.GetAllFlocks().Select(f => new SelectListItem
                {
                    Text = $"{_flockTypeService.FindById(f.FlockTypeId).Name} (Batch: {f.BatchNo})",
                    Value = f.Id.ToString()
                }),

                FeedAllocationList = _storeAllocationService.FeedAllocation(userId).Select(f => new SelectListItem
                {
                    Text = $"{_storeItemService.FindById(f.StoreItemId).Name} ({_storeItemService.FindById(f.StoreItemId).Description})",
                    Value = f.Id.ToString()
                }),

                MedAllocationList = _storeAllocationService.MedAllocation(userId).Select(f => new SelectListItem
                {
                    Text = $"{_storeItemService.FindById(f.StoreItemId).Name} ({_storeItemService.FindById(f.StoreItemId).Description})",
                    Value = f.Id.ToString()
                }),
            };
            return View(dailyActivityVM);
        }

        [HttpPost]
        public IActionResult AddDailyActivity(AddDailyActivityVM addDailyActivity)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            DailyActivityDTO dailyActivityDTO = new DailyActivityDTO
            {
                UserId = userId,
                FeedConsumedPerKg = addDailyActivity.FeedConsumedPerKg,
                Mortality = addDailyActivity.Mortality,
                NoOfMedUsed = addDailyActivity.NoOfMedUsed,
                FlockId = addDailyActivity.FlockId,
                StoreAllocationFeedId = addDailyActivity.StoreAllocationFeedId,
                StoreAllocationMedId = addDailyActivity.StoreAllocationMedId,
            };

            _dailyService.Add(dailyActivityDTO);
            return RedirectToAction("Index");
        }


        public IActionResult UpdateDailyActivity(int id)
        {
            var dailyActivity = _dailyService.FindById(id);
            if (dailyActivity == null)
            {
                return NotFound();
            }
            else
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
                User userlogin = _userService.FindById(userId);
                ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

                UpdateDailyActivityVM updateDailyActivity = new UpdateDailyActivityVM
                {
                    Id = dailyActivity.Id,
                    FeedConsumedPerKg = dailyActivity.FeedConsumedPerKg,
                    Mortality = dailyActivity.Mortality,
                    NoOfMedUsed = dailyActivity.NoOfMedUsed,
                    FlockId = dailyActivity.FlockId,
                    StoreAllocationFeedId = dailyActivity.StoreAllocationFeedId,
                    StoreAllocationMedId = dailyActivity.StoreAllocationMedId,
                    FlockList = _flockService.GetAllFlocks().Select(f => new SelectListItem
                    {
                        Text = $"{_flockTypeService.FindById(f.FlockTypeId).Name} (Batch: {f.BatchNo})",
                        Value = f.Id.ToString()
                    }),

                    FeedAllocationList = _storeAllocationService.FeedAllocation(userId).Select(f => new SelectListItem
                    {
                        Text = $"{_storeItemService.FindById(f.StoreItemId).Name} ({_storeItemService.FindById(f.StoreItemId).Description})",
                        Value = f.Id.ToString()
                    }),

                    MedAllocationList = _storeAllocationService.MedAllocation(userId).Select(f => new SelectListItem
                    {
                        Text = $"{_storeItemService.FindById(f.StoreItemId).Name} ({_storeItemService.FindById(f.StoreItemId).Description})",
                        Value = f.Id.ToString()
                    }),
                };

                return View(updateDailyActivity);
            }

        }

        [HttpPost]
        public IActionResult UpdateDailyActivity(UpdateDailyActivityVM updateDailyActivity)
        {
            DailyActivityDTO dailyActivity = new DailyActivityDTO
            {
                Id = updateDailyActivity.Id,
                FeedConsumedPerKg = updateDailyActivity.FeedConsumedPerKg,
                Mortality = updateDailyActivity.Mortality,
                NoOfMedUsed = updateDailyActivity.NoOfMedUsed,
                FlockId = updateDailyActivity.FlockId,
                StoreAllocationFeedId = updateDailyActivity.StoreAllocationFeedId,
                StoreAllocationMedId = updateDailyActivity.StoreAllocationMedId,
            };
            _dailyService.Update(dailyActivity);
            return RedirectToAction("Index");
        }



        public IActionResult Delete(int id)
        {
            var dailyActivity = _dailyService.FindById(id);
            if (dailyActivity == null)
            {
                return NotFound();
            }
            _dailyService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
