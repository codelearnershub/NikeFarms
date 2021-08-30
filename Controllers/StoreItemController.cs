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
    public class StoreItemController : Controller
    {
        private readonly IStoreItemService _storeItemService;
        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;

        public StoreItemController(IStoreItemService storeItemService, IUserService userService, IUserRoleService userRoleService)
        {
            _storeItemService = storeItemService;
            _userService = userService;
            _userRoleService = userRoleService;
        }

        public IActionResult Index()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            ViewBag.Role = $"{_userRoleService.FindRole(userId)}";

            var storeItems = _storeItemService.GetAllStoreItems();
            List<ListStoreItemVM> ListStoreItem = new List<ListStoreItemVM>();

            foreach (var storeItem in storeItems)
            {
                var Created = _userService.FindByEmail(storeItem.CreatedBy);

                ListStoreItemVM listStoreItemVM = new ListStoreItemVM
                {

                    Id = storeItem.Id,
                    Name = storeItem.Name,
                    Description = storeItem.Description,
                    NoOfItem = storeItem.NoOfItem,
                    ItemType = storeItem.ItemType,
                    ItemPerKg = storeItem.ItemPerKg,
                    ItemRemaining = storeItem.ItemRemaining,
                    TotalPricePurchased = storeItem.TotalPricePurchased,
                    IsApproved = storeItem.IsApproved,
                    CreatedBy = $"{Created.FirstName} .{Created.LastName[0]}",
                };

                ListStoreItem.Add(listStoreItemVM);
            }

            
            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

            return View(ListStoreItem);
        }

        public IActionResult AddStoreItem()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

            return View();
        }

        [HttpPost]
        public IActionResult AddStoreItem(AddStoreItemVM addStoreItem)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            StoreItemDTO storeItem = new StoreItemDTO
            {
                UserId = userId,
                Name = addStoreItem.Name,
                Description = addStoreItem.Description,
                ItemType = addStoreItem.ItemType,
                NoOfItem = addStoreItem.NoOfItem,
                ItemPerKg = addStoreItem.ItemPerKg,
                ItemRemaining  = addStoreItem.NoOfItem,
                TotalPricePurchased = addStoreItem.TotalPricePurchased,
                IsApproved = false,
            };

            _storeItemService.Add(storeItem);
            return RedirectToAction("Index");
        }


        public IActionResult UpdateStoreItem(int id)
        {
            var storeItem = _storeItemService.FindById(id);
            if (storeItem == null || storeItem.IsApproved == true)
            {
                return NotFound();
            }
            else
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
                User userlogin = _userService.FindById(userId);
                ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

                UpdateStoreItemVM updateStoreItem = new UpdateStoreItemVM
                {

                    Id = storeItem.Id,
                    Name = storeItem.Name,
                    Description = storeItem.Description,
                    ItemType = storeItem.ItemType,
                    NoOfItem = storeItem.NoOfItem,
                    ItemPerKg = storeItem.ItemPerKg,
                    TotalPricePurchased = storeItem.TotalPricePurchased,

                };

                return View(updateStoreItem);
            }

        }

        [HttpPost]
        public IActionResult UpdateStoreItem(UpdateStoreItemVM updateStoreItem)
        {
            StoreItemDTO storeItem = new StoreItemDTO
            {
                Id = updateStoreItem.Id,
                Name = updateStoreItem.Name,
                Description = updateStoreItem.Description,
                ItemType = updateStoreItem.ItemType,
                NoOfItem = updateStoreItem.NoOfItem,
                ItemPerKg = updateStoreItem.ItemPerKg,
                ItemRemaining = updateStoreItem.NoOfItem,
                TotalPricePurchased = updateStoreItem.TotalPricePurchased,
                IsApproved = false,
            };
            _storeItemService.Update(storeItem);
            return RedirectToAction("Index");
        }



        public IActionResult Delete(int id)
        {
            var storeItem = _storeItemService.FindById(id);
            if (storeItem == null || storeItem.IsApproved == true)
            {
                return NotFound();
            }
            _storeItemService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
