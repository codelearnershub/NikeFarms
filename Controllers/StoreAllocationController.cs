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
    public class StoreAllocationController : Controller
    {
        private readonly IUserService _userService;
        private readonly IStoreAllocationService _storeAllocationService;
        private readonly IUserRoleService _userRoleService;
        private readonly IStoreItemService _storeItemService;
        private readonly IRoleService _roleService;

        public StoreAllocationController(IUserService userService, IStoreAllocationService storeAllocationService, IUserRoleService userRoleService, IStoreItemService storeItemService, IRoleService roleService)
        {
            _userService = userService;
            _storeAllocationService = storeAllocationService;
            _userRoleService = userRoleService;
            _storeItemService = storeItemService;
            _roleService = roleService;
        }

        public IActionResult Index()
        {

            var toreAllocations = _storeAllocationService.GetAllStoreAllocations();
            List<ListStoreAllocationVM> ListStoreAllocations = new List<ListStoreAllocationVM>();

            foreach (var storeAllocation in toreAllocations)
            {
                ListStoreAllocationVM listStoreAllocationVM = new ListStoreAllocationVM
                {

                    NoOfItem = storeAllocation.NoOfItem,
                    StoreItemName = _storeItemService.FindById(storeAllocation.StoreItemId).Name,
                    AllocatedTo = $"{_userService.FindById(storeAllocation.Id).LastName } {_userService.FindById(storeAllocation.Id).FirstName }",
                    ItemRemaining = storeAllocation.ItemRemaining,
                    ItemType = storeAllocation.ItemType,
                    ItemPerKg = storeAllocation.ItemPerKg,
                    CreatedBy = $"{_userService.FindByEmail(storeAllocation.CreatedBy).FirstName} .{_userService.FindByEmail(storeAllocation.CreatedBy).LastName[0]}",

                };

                ListStoreAllocations.Add(listStoreAllocationVM);
            }

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

            return View(ListStoreAllocations);
        }

        [HttpGet]
        public IActionResult AddStoreAllocation()
        {
            var role = _roleService.FindByName("Farm Manager");
            AddStoreAllocationVM storeAllocationVM = new AddStoreAllocationVM
            {
                StoreItemList = _storeItemService.GetAllStoreItems().Select(m => new SelectListItem
                {
                    Text = m.Name,
                    Value = m.Id.ToString()
                }),

                
                ManagerList = _userService.GetAllUser().Select(m => new SelectListItem
                {
                    Text = $"{_userService.FindById(_userRoleService.FindUsersWithParticularRole(role.Id)).LastName} {_userService.FindById(_userRoleService.FindUsersWithParticularRole(role.Id)).FirstName} (Farm Manager)",
                    Value = m.Id.ToString()
                }),

            };

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

            return View(storeAllocationVM);
        }


        [HttpPost]
        public IActionResult AddStoreAllocation(AddStoreAllocationVM vm)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            StoreAllocationDTO storeAllocationDTO = new StoreAllocationDTO
            {
                UserId = userId,
                StoreItemId = vm.StoreItemId,
                ManagerId = vm.ManagerId,
                NoOfItem = vm.NoOfItem,
                ItemPerKg = vm.ItemPerKg,
                ItemType = vm.ItemType,
                ItemRemaining = vm.NoOfItem,
                IsApproved = false,
            };
            _storeAllocationService.Add(storeAllocationDTO);
            return RedirectToAction("Index");
        }


        public IActionResult UpdateStoreAllocation(int id)
        {
            var storeAllocation = _storeAllocationService.FindById(id);
            if (storeAllocation == null)
            {
                return NotFound();
            }
            else
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
                User userlogin = _userService.FindById(userId);
                ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

                var role = _roleService.FindByName("Farm Manager");
                UpdateStoreAllocationVM updateStoreAllocation = new UpdateStoreAllocationVM
                {
                    Id = storeAllocation.Id,
                    NoOfItem = storeAllocation.NoOfItem,
                    ItemPerKg = storeAllocation.ItemPerKg,
                    ItemType = storeAllocation.ItemType,
                    StoreItemId = storeAllocation.StoreItemId,
                    ManagerId = storeAllocation.ManagerId,
                    StoreItemList = _storeItemService.GetAllStoreItems().Select(m => new SelectListItem
                    {
                        Text = m.Name,
                        Value = m.Id.ToString()
                    }),
                    ManagerList = _userService.GetAllUser().Select(m => new SelectListItem
                    {
                        Text = $"{_userService.FindById(_userRoleService.FindUsersWithParticularRole(role.Id)).LastName} {_userService.FindById(_userRoleService.FindUsersWithParticularRole(role.Id)).FirstName} (Farm Manager)",
                        Value = m.Id.ToString()
                    }),
                };

                return View(updateStoreAllocation);
            }

        }

        [HttpPost]
        public IActionResult UpdateStoreAllocation(UpdateStoreAllocationVM updateStoreAllocation)
        {
            StoreAllocationDTO storeAllocation = new StoreAllocationDTO
            {
                Id = updateStoreAllocation.Id,
                StoreItemId = updateStoreAllocation.StoreItemId,
                ManagerId = updateStoreAllocation.ManagerId,
                NoOfItem = updateStoreAllocation.NoOfItem,
                ItemPerKg = updateStoreAllocation.ItemPerKg,
                ItemType = updateStoreAllocation.ItemType,
                ItemRemaining = updateStoreAllocation.NoOfItem,
                IsApproved = false,
            };
            _storeAllocationService.Update(storeAllocation);
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            var storeAllocation = _storeAllocationService.FindById(id);
            if (storeAllocation == null)
            {
                return NotFound();
            }
            _storeAllocationService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
