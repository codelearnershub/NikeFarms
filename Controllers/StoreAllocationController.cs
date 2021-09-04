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
    [Authorize(Roles = "Super Admin, Store Manager")]
    public class StoreAllocationController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;
        private readonly IStoreAllocationService _storeAllocationService;
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

            var StoreAllocations = _storeAllocationService.GetAllStoreAllocations();
            List<ListStoreAllocationVM> ListStoreAllocations = new List<ListStoreAllocationVM>();

            foreach (var storeAllocation in StoreAllocations)
            {
                var Created = _userService.FindByEmail(storeAllocation.CreatedBy);
                var Allocated = _userService.FindById(storeAllocation.ManagerId);

                ListStoreAllocationVM listStoreAllocationVM = new ListStoreAllocationVM
                {
                    Id = storeAllocation.Id,
                    NoOfItem = storeAllocation.NoOfItem,
                    StoreItemName = _storeItemService.FindById(storeAllocation.StoreItemId).Name,
                    AllocatedTo = $"{Allocated.LastName } {Allocated.FirstName }",
                    ItemRemaining = storeAllocation.ItemRemaining,
                    ItemType = storeAllocation.ItemType,
                    ItemPerKg = storeAllocation.ItemPerKg,
                    IsApproved = storeAllocation.IsApproved,
                    CreatedBy = $"{Created.FirstName} .{Created.LastName[0]}",

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
                StoreItemList = _storeItemService.GetApprovedStoreItems().Select(m => new SelectListItem
                {
                    Text = $"{m.Name} ({m.ItemRemaining})",
                    Value = m.Id.ToString()
                }),


                ManagerList = _userRoleService.FindUsersWithParticularRole(role.Id).Select(m => new SelectListItem
                {
                    Text = $"{_userService.FindById(m.UserId).LastName} {_userService.FindById(m.UserId).FirstName} (Farm Manager)",
                    Value = m.UserId.ToString()
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
                ItemPerKg = _storeItemService.FindById(vm.StoreItemId).ItemPerKg,
                ItemType = _storeItemService.FindById(vm.StoreItemId).ItemType,
                ItemRemaining = vm.NoOfItem,
                IsApproved = false,
            };


            double StoreItemRemaining = _storeItemService.FindById(vm.StoreItemId).ItemRemaining;
            if (StoreItemRemaining < vm.NoOfItem)
            {
                ViewBag.Message = "error";

                var role = _roleService.FindByName("Farm Manager");
                AddStoreAllocationVM storeAllocationVM = new AddStoreAllocationVM
                {
                    StoreItemList = _storeItemService.GetAllStoreItems().Select(m => new SelectListItem
                    {
                        Text = $"{m.Name} ({m.ItemRemaining})",
                        Value = m.Id.ToString()
                    }),


                    ManagerList = _userRoleService.FindUsersWithParticularRole(role.Id).Select(m => new SelectListItem
                    {
                        Text = $"{_userService.FindById(m.UserId).LastName} {_userService.FindById(m.UserId).FirstName} (Farm Manager)",
                        Value = m.UserId.ToString()
                    }),

                };
                return View(storeAllocationVM);
            }

            _storeAllocationService.Add(storeAllocationDTO);
            return RedirectToAction("Index");

        }


        public IActionResult UpdateStoreAllocation(int id)
        {
            var storeAllocation = _storeAllocationService.FindById(id);
            if (storeAllocation == null || storeAllocation.IsApproved == true)
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
                        Text = $"{m.Name} ({m.ItemRemaining})",
                        Value = m.Id.ToString()
                    }),
                    ManagerList = _userRoleService.FindUsersWithParticularRole(role.Id).Select(m => new SelectListItem
                    {
                        Text = $"{_userService.FindById(m.UserId).LastName} {_userService.FindById(m.UserId).FirstName} (Farm Manager)",
                        Value = m.UserId.ToString()
                    }),
                };

                return View(updateStoreAllocation);
            }

        }

        [HttpPost]
        public IActionResult UpdateStoreAllocation(UpdateStoreAllocationVM updateStoreAllocation)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            StoreAllocationDTO storeAllocation = new StoreAllocationDTO
            {
                Id = updateStoreAllocation.Id,
                UserId = userId,
                StoreItemId = updateStoreAllocation.StoreItemId,
                ManagerId = updateStoreAllocation.ManagerId,
                NoOfItem = updateStoreAllocation.NoOfItem,
                ItemRemaining = updateStoreAllocation.NoOfItem,
                IsApproved = false,
            };

            double StoreItemRemaining = _storeItemService.FindById(updateStoreAllocation.StoreItemId).ItemRemaining;
            if (StoreItemRemaining < updateStoreAllocation.NoOfItem)
            {
                ViewBag.Message = "error";

                var role = _roleService.FindByName("Farm Manager");
                UpdateStoreAllocationVM updateStoreAllocationD = new UpdateStoreAllocationVM
                {
                    Id = storeAllocation.Id,
                    NoOfItem = storeAllocation.NoOfItem,
                    ItemPerKg = storeAllocation.ItemPerKg,
                    ItemType = storeAllocation.ItemType,
                    StoreItemId = storeAllocation.StoreItemId,
                    ManagerId = storeAllocation.ManagerId,
                    StoreItemList = _storeItemService.GetAllStoreItems().Select(m => new SelectListItem
                    {
                        Text = $"{m.Name} ({m.ItemRemaining})",
                        Value = m.Id.ToString()
                    }),
                    ManagerList = _userRoleService.FindUsersWithParticularRole(role.Id).Select(m => new SelectListItem
                    {
                        Text = $"{_userService.FindById(m.UserId).LastName} {_userService.FindById(m.UserId).FirstName} (Farm Manager)",
                        Value = m.UserId.ToString()
                    }),
                };

                return View(updateStoreAllocationD);
            }

            _storeAllocationService.Update(storeAllocation);
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            var storeAllocation = _storeAllocationService.FindById(id);
            if (storeAllocation == null || storeAllocation.IsApproved == true)
            {
                return NotFound();
            }
            _storeAllocationService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
