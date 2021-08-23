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

        public StoreItemController(IStoreItemService storeItemService, IUserService userService)
        {
            _storeItemService = storeItemService;
            _userService = userService;
        }

        public IActionResult Index()
        {

            var storeItems = _storeItemService.GetAllStoreItems();
            List<ListStoreItemVM> ListStoreItem = new List<ListStoreItemVM>();

            foreach (var storeItem in storeItems)
            {
                ListStoreItemVM listStoreItemVM = new ListStoreItemVM
                {

                    Id = storeItem.Id,
                    Name = storeItem.Name,
                    Description = storeItem.Description,
                    NoOfItem = storeItem.NoOfItem,
                    ItemPerKg = storeItem.ItemPerKg,
                    ItemRemaining = storeItem.ItemRemaining,
                    CreatedBy = $"{_userService.FindByEmail(storeItem.CreatedBy).FirstName} .{_userService.FindByEmail(storeItem.CreatedBy).LastName[0]}",
                };

                ListStoreItem.Add(listStoreItemVM);
            }

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
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
                IsApproved = false,
            };

            _storeItemService.Add(storeItem);
            return RedirectToAction("Index");
        }


        public IActionResult UpdateStoreItem(int id)
        {
            var storeItem = _storeItemService.FindById(id);
            if (storeItem == null)
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
                IsApproved = false,
            };
            _storeItemService.Update(storeItem);
            return RedirectToAction("Index");
        }



        public IActionResult Delete(int id)
        {
            var storeItem = _storeItemService.FindById(id);
            if (storeItem == null)
            {
                return NotFound();
            }
            _storeItemService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
