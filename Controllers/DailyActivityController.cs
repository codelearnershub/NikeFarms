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

        [Authorize(Roles = "Super Admin, Farm Manager")]
        public IActionResult Index(string sortOrder, string searchString, int? pageNumber)
        {
            var dailyActs = _dailyService.GetAllDailyActivities();
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentFilter"] = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                dailyActs = dailyActs.Where(s => s.Flock.BatchNo.Contains(searchString));
            }

            List<ListDailyActivityVM> ListDailyActivity = new List<ListDailyActivityVM>();
            foreach (var dailyAct in dailyActs)
            {
                var Created = _userService.FindByEmail(dailyAct.CreatedBy);
                var Flock = _flockService.FindById(dailyAct.FlockId);
                var storeAllocationMed = _storeAllocationService.FindMedById(dailyAct.StoreAllocationMedId);
                var storeAllocationFeed = _storeAllocationService.FindById(dailyAct.StoreAllocationFeedId);

                string MedName = "null";
                if (storeAllocationMed != null)
                {
                    MedName = _storeItemService.FindById(storeAllocationMed.StoreItemId).Name;
                };

                ListDailyActivityVM listDailyActivityVM = new ListDailyActivityVM
                {
                    Id = dailyAct.Id,
                    FlockDescription = $"{_flockTypeService.FindById(Flock.FlockTypeId).Name}  Batch No: {Flock.BatchNo}",
                    MedUsed = MedName,
                    NoOfMedUsed = dailyAct.NoOfMedUsed,
                    FeedUsed = $"{_storeItemService.FindById(storeAllocationFeed.StoreItemId).Name}",
                    NoOfFeedUsed = dailyAct.NoOfFeedUsed,
                    CreatedAt = dailyAct.Date,
                    CreatedBy = $"{Created.FirstName} .{Created.LastName[0]}",
                };

                ListDailyActivity.Add(listDailyActivityVM);
            }

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";
            int pageSize = 5;
            return View(PaginatedList<ListDailyActivityVM>.CreateAsync(ListDailyActivity.AsQueryable(), pageNumber ?? 1, pageSize));
        }

        [Authorize(Roles = "Super Admin, Farm Manager")]
        public IActionResult AddDailyActivity()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

            AddDailyActivityVM dailyActivityVM = new AddDailyActivityVM
            {

                FlockList = _flockService.OperationDaily().Select(f => new SelectListItem
                {
                    Text = $"{_flockTypeService.FindById(f.FlockTypeId).Name} Batch No: ({_flockService.FindById(f.Id).BatchNo}) Available: {_flockService.FindById(f.Id).AvailableBirds} Bird(s)",
                    Value = f.Id.ToString()
                }),

                FeedAllocationList = _storeAllocationService.FeedAllocation(userId).Select(f => new SelectListItem
                {
                    Text = $"{_storeItemService.FindById(f.StoreItemId).Name} ({_storeAllocationService.FindById(f.Id).ItemRemaining})",
                    Value = f.Id.ToString()
                }),

                MedAllocationList = _storeAllocationService.MedAllocation(userId).Select(f => new SelectListItem
                {
                    Text = $"{_storeItemService.FindById(f.StoreItemId).Name} ({_storeAllocationService.FindById(f.Id).ItemRemaining})",
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
                NoOfMedUsed = addDailyActivity.NoOfMedUsed,
                NoOfFeedUsed = addDailyActivity.NoOfFeedUsed,
                FlockId = addDailyActivity.FlockId,
                StoreAllocationFeedId = addDailyActivity.StoreAllocationFeedId,
                StoreAllocationMedId = addDailyActivity.StoreAllocationMedId,
            };

            double StoreAlloFeedRemaining = _storeAllocationService.FindById(addDailyActivity.StoreAllocationFeedId).ItemRemaining;
            int availableBirds = _flockService.FindById(addDailyActivity.FlockId).AvailableBirds;
            double? StoreAlloMedRemaining = 0;
            if (_storeAllocationService.FindMedById(addDailyActivity.StoreAllocationMedId) != null)
            {
                StoreAlloMedRemaining = _storeAllocationService.FindMedById(addDailyActivity.StoreAllocationMedId).ItemRemaining;
            }

            if (StoreAlloFeedRemaining < addDailyActivity.NoOfFeedUsed || StoreAlloMedRemaining < addDailyActivity.NoOfMedUsed)
            {
                if (StoreAlloFeedRemaining < addDailyActivity.NoOfFeedUsed)
                {
                    ViewBag.Message = "errorFeed";
                }
                else if (StoreAlloMedRemaining < addDailyActivity.NoOfMedUsed)
                {
                    ViewBag.Message = "errorMed";
                }
               

                AddDailyActivityVM dailyActivityVM = new AddDailyActivityVM
                {

                    FlockList = _flockService.OperationDaily().Select(f => new SelectListItem
                    {
                        Text = $"{_flockTypeService.FindById(f.FlockTypeId).Name} Batch No: ({_flockService.FindById(f.Id).BatchNo}) Available: {_flockService.FindById(f.Id).AvailableBirds} Bird(s)",
                        Value = f.Id.ToString()
                    }),

                    FeedAllocationList = _storeAllocationService.FeedAllocation(userId).Select(f => new SelectListItem
                    {
                        Text = $"{_storeItemService.FindById(f.StoreItemId).Name} ({_storeAllocationService.FindById(f.Id).ItemRemaining})",
                        Value = f.Id.ToString()
                    }),

                    MedAllocationList = _storeAllocationService.MedAllocation(userId).Select(f => new SelectListItem
                    {
                        Text = $"{_storeItemService.FindById(f.StoreItemId).Name} ({_storeAllocationService.FindById(f.Id).ItemRemaining})",
                        Value = f.Id.ToString()
                    }),
                };
                return View(dailyActivityVM);
            };



            var feedAllo = _storeAllocationService.FindById(addDailyActivity.StoreAllocationFeedId);
            if (feedAllo != null)
            {
                StoreAllocationDTO feedStoreAllocation = new StoreAllocationDTO
                {
                    Id = feedAllo.Id,
                    UserId = userId,
                    StoreItemId = feedAllo.StoreItemId,
                    ManagerId = feedAllo.ManagerId,
                    NoOfItem = feedAllo.NoOfItem,
                    ItemRemaining = feedAllo.ItemRemaining - addDailyActivity.NoOfFeedUsed,
                    IsApproved = true,
                };
                _storeAllocationService.Update(feedStoreAllocation);

            };


            var medAllo = _storeAllocationService.FindMedById(addDailyActivity.StoreAllocationMedId);
            if (medAllo != null)
            {
                StoreAllocationDTO medStoreAllocation = new StoreAllocationDTO
                {
                    Id = medAllo.Id,
                    UserId = userId,
                    StoreItemId = medAllo.StoreItemId,
                    ManagerId = medAllo.ManagerId,
                    NoOfItem = medAllo.NoOfItem,
                    ItemRemaining = medAllo.ItemRemaining - addDailyActivity.NoOfMedUsed,
                    IsApproved = true,
                };
                _storeAllocationService.Update(medStoreAllocation);
            };

        
            _dailyService.Add(dailyActivityDTO);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Super Admin, Admin")]
        public IActionResult ListDailyActivity(int id, string sortOrder, int? pageNumber)
        {
            var dailyActs = _dailyService.GetDailyActivitiesPerFlockId(id);
            ViewData["CurrentSort"] = sortOrder;
            var flock = _flockService.FindById(id);
            ViewBag.FlockD = $"{_flockTypeService.FindById(flock.FlockTypeId).Name}  Batch No: {flock.BatchNo}";

            List<ListDailyActivityVM> ListDailyActivity = new List<ListDailyActivityVM>();
            foreach (var dailyAct in dailyActs)
            {
                var Created = _userService.FindByEmail(dailyAct.CreatedBy);
                var storeAllocationMed = _storeAllocationService.FindMedById(dailyAct.StoreAllocationMedId);
                var storeAllocationFeed = _storeAllocationService.FindById(dailyAct.StoreAllocationFeedId);

                string MedName = "null";
                if (storeAllocationMed != null)
                {
                    MedName = _storeItemService.FindById(storeAllocationMed.StoreItemId).Name;
                };
                ListDailyActivityVM listDailyActivityVM = new ListDailyActivityVM
                {
                    Id = dailyAct.Id,
                    MedUsed = MedName,
                    NoOfMedUsed = dailyAct.NoOfMedUsed,
                    FeedUsed = $"{_storeItemService.FindById(storeAllocationFeed.StoreItemId).Name}",
                    NoOfFeedUsed = dailyAct.NoOfFeedUsed,
                    CreatedAt = dailyAct.Date,
                    CreatedBy = $"{Created.FirstName} .{Created.LastName[0]}",
                };

                ListDailyActivity.Add(listDailyActivityVM);
            }

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";
            int pageSize = 5;
            return View(PaginatedList<ListDailyActivityVM>.CreateAsync(ListDailyActivity.AsQueryable(), pageNumber ?? 1, pageSize));
        }

       
        
        [Authorize(Roles = "Super Admin, Farm Manager")]
        public IActionResult Delete(int id)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var dailyActivity = _dailyService.FindById(id);
            if (dailyActivity == null || dailyActivity.Date != DateTime.Now.ToShortDateString())
            {
                return NotFound();
            }

            var feedAllo = _storeAllocationService.FindById(dailyActivity.StoreAllocationFeedId);
            StoreAllocationDTO feedStoreAllocation = new StoreAllocationDTO
            {
                Id = feedAllo.Id,
                UserId = userId,
                StoreItemId = feedAllo.StoreItemId,
                ManagerId = feedAllo.ManagerId,
                NoOfItem = feedAllo.NoOfItem,
                ItemRemaining = feedAllo.ItemRemaining + dailyActivity.NoOfFeedUsed,
                IsApproved = true,
            };
            _storeAllocationService.Update(feedStoreAllocation);


            var medAllo = _storeAllocationService.FindMedById(dailyActivity.StoreAllocationMedId);
            if (medAllo != null)
            {
                StoreAllocationDTO medStoreAllocation = new StoreAllocationDTO
                {
                    Id = medAllo.Id,
                    UserId = userId,
                    StoreItemId = medAllo.StoreItemId,
                    ManagerId = medAllo.ManagerId,
                    NoOfItem = medAllo.NoOfItem,
                    ItemRemaining = medAllo.ItemRemaining + dailyActivity.NoOfMedUsed,
                    IsApproved = true,
                };
                _storeAllocationService.Update(medStoreAllocation);
            }


           
            _dailyService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
