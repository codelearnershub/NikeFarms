using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    [Authorize(Roles = "Super Admin, Admin, Farm Manager")]
    public class NotificationController : Controller
    {
        private readonly INotificationService _notificationService;
        private readonly IUserService _userService;
        private readonly IFlockService _flockService;
        private readonly IStoreItemService _storeItemService;
        private readonly IStoreAllocationService _storeAllocationService;
        private readonly IExpensesService _expensesService;


        public NotificationController(INotificationService notificationService, IUserService userService, IFlockService flockService, IStoreItemService storeItemService, IStoreAllocationService storeAllocationService, IExpensesService expensesService)
        {
            _notificationService = notificationService;
            _userService = userService;
            _flockService = flockService;
            _storeItemService = storeItemService;
            _storeAllocationService = storeAllocationService;
            _expensesService = expensesService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Super Admin, Admin")]
        public IActionResult ListAdminNotifications()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

            var notifications = _notificationService.GetNotifications(userId);
            ViewBag.NotifyCount = notifications.Count();
            List<ListNotificationVM> ListNotifications = new List<ListNotificationVM>();
            foreach (var notification in notifications)

            {
                ListNotificationVM listNotificationVM = new ListNotificationVM
                {
                    Id = notification.Id,
                    Content = notification.Content,
                    ApproveId = notification.ApproveId,
                    Type = notification.Type,
                    CreatedAt = notification.CreatedAt,
                };

                ListNotifications.Add(listNotificationVM);
            }

            return View(ListNotifications);
        }

        [Authorize(Roles = "Super Admin, Farm Manager")]
        public IActionResult ListManagerNotifications()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

            var notifications = _notificationService.GetNotifications(userId);

            ViewBag.NotifyCount = notifications.Count();
            List<ListNotificationVM> ListNotifications = new List<ListNotificationVM>();
            foreach (var notification in notifications)

            {
                ListNotificationVM listNotificationVM = new ListNotificationVM
                {
                    Id = notification.Id,
                    Content = notification.Content,
                    ApproveId = notification.ApproveId,
                    Type = notification.Type,
                    CreatedAt = notification.CreatedAt,
                };

                ListNotifications.Add(listNotificationVM);
            }

            return View(ListNotifications);
        }

        [Authorize(Roles = "Super Admin, Farm Manager")]
        public IActionResult InsufficientStoreItem()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";
            return View();
        }

        
        public IActionResult Approve(int id)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var notify = _notificationService.FindByApproveId(id);

            if(notify.Type == "Flock")
            {
                var flock = _flockService.FindById(id);

                FlockDTO flockD = new FlockDTO
                {
                    UserId = _userService.FindByEmail(flock.CreatedBy).Id,
                    Id = flock.Id,
                    TotalNo = flock.TotalNo,
                    AverageWeight = flock.AverageWeight,
                    AvailableBird = flock.TotalNo,
                    Age = flock.Age,
                    AmountPurchased = flock.AmountPurchased,
                    FlockTypeId = flock.FlockTypeId,
                    IsApproved = true,
                };

                _flockService.Update(flockD);
                _notificationService.Delete(id);
            }
            else if(notify.Type == "StoreItem")
            {
                var store= _storeItemService.FindById(id);
                StoreItemDTO storeItem = new StoreItemDTO
                {
                    Id = store.Id,
                    Name = store.Name,
                    Description = store.Description,
                    ItemType = store.ItemType,
                    NoOfItem = store.NoOfItem,
                    ItemPerKg = store.ItemPerKg,
                    ItemRemaining = store.NoOfItem,
                    TotalPricePurchased = store.TotalPricePurchased,
                    IsApproved = true,
                };
                _storeItemService.Update(storeItem);
                _notificationService.Delete(id);

            }
            else if(notify.Type == "Allocation")
            {
                var storeAllocation = _storeAllocationService.FindById(id);
                var store = _storeItemService.FindById(storeAllocation.StoreItemId);

                if(store.ItemRemaining - storeAllocation.NoOfItem < 0)
                {
                    ViewBag.Message = "Insufficient";
                    return RedirectToAction("InsufficientStoreItem");
                }
                StoreItemDTO storeItem = new StoreItemDTO
                {
                    Id = store.Id,
                    Name = store.Name,
                    Description = store.Description,
                    ItemType = store.ItemType,
                    NoOfItem = store.NoOfItem,
                    ItemPerKg = store.ItemPerKg,
                    ItemRemaining = store.ItemRemaining - storeAllocation.NoOfItem,
                    TotalPricePurchased = store.TotalPricePurchased,
                    IsApproved = true,
                };
                _storeItemService.Update(storeItem);

                StoreAllocationDTO storeAllocationD = new StoreAllocationDTO
                {
                    Id = storeAllocation.Id,
                    UserId = _userService.FindByEmail(storeAllocation.CreatedBy).Id,
                    StoreItemId = storeAllocation.StoreItemId,
                    ManagerId = storeAllocation.ManagerId,
                    NoOfItem = storeAllocation.NoOfItem,
                    ItemRemaining = storeAllocation.NoOfItem,
                    IsApproved = true,
                };
                _storeAllocationService.Update(storeAllocationD);
                _notificationService.Delete(id);

                return RedirectToAction("ListManagerNotifications");
            }
            else if (notify.Type == "Expenses")
            {
                var expense = _expensesService.FindById(id);
                ExpensesDTO expenses = new ExpensesDTO
                {
                    UserId = userId,
                    Id = expense.Id,
                    Description = expense.Description,
                    Price = expense.Price,
                    IsApproved = true,
                };
                _expensesService.Update(expenses);
                _notificationService.Delete(id);

            }
            else if(notify.Type == "FlockFinish")
            {
                _notificationService.Delete(id);
            }
            return RedirectToAction("ListAdminNotifications");
        }
    }
}
