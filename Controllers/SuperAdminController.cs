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
    public class SuperAdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;
        private readonly IRoleService _roleService;

        public SuperAdminController(IUserService userService, IUserRoleService userRoleService, IRoleService roleService)
        {
            _userService = userService;
            _userRoleService = userRoleService;
            _roleService = roleService;
        }

        public IActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public IActionResult RegisterUser()
        {
            RegisterVM registerVM = new RegisterVM
            {
                RoleList = _roleService.GetAllRoles().Select(m => new SelectListItem
                {
                    Text = m.Name,
                    Value = m.Id.ToString()
                })
            };

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

            return View(registerVM);
        }


        [HttpPost]
        public IActionResult RegisterUser(RegisterVM vm)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            UserDTO userDTO = new UserDTO
            {
                UserId = userId,
                Email = vm.Email,
                Password = vm.Password,
                LastName = vm.LastName,
                FirstName = vm.FirstName,
                PhoneNo = vm.PhoneNo,
                Address = vm.Address,
                RoleId = vm.RoleId,
            };
            _userService.RegisterUser(userDTO);
            return RedirectToAction("ListUser");
        }

        public IActionResult ListUser()
        {
            var users = _userService.GetAllUser();
            List<ListUserVM> ListUser = new List<ListUserVM>();

            foreach (var user in users)
            {
                var Created = _userService.FindByEmail(user.CreatedBy);

                ListUserVM listUserVM = new ListUserVM
                {

                    UserId = user.Id,
                    FullName = $"{user.LastName} {user.FirstName}",
                    Email = user.Email,
                    PhoneNo = user.PhoneNo,
                    RoleName = _userRoleService.FindRole(user.Id),
                    CreatedBy = $"{Created.FirstName} .{Created.LastName[0]}",

                };

                ListUser.Add(listUserVM);
            }

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

            return View(ListUser);
        }


        public IActionResult UpdateUser(int id)
        {
            var user = _userService.FindById(id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
                User userlogin = _userService.FindById(userId);
                ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

                UpdateUserVM updateUser = new UpdateUserVM
                {
                    Id = user.Id,
                    LastName = user.LastName,
                    FirstName = user.FirstName,
                    Email = user.Email,
                    PhoneNo = user.PhoneNo,
                    Address = user.Address,
                    RoleId = _userRoleService.FindUserRole(user.Id).RoleId,
                    RoleList = _roleService.GetAllRoles().Select(m => new SelectListItem
                    {
                        Text = m.Name,
                        Value = m.Id.ToString()
                    })
                };

                return View(updateUser);
            }

        }

        [HttpPost]
        public IActionResult UpdateUser(UpdateUserVM updateUser)
        {
            UserDTO user = new UserDTO
            {
                Id = updateUser.Id,
                LastName = updateUser.LastName,
                FirstName = updateUser.FirstName,
                PhoneNo = updateUser.PhoneNo,
                Email = updateUser.Email,
                Password = updateUser.Password,
                Address = updateUser.Address,
                RoleId = updateUser.RoleId,
            };
            _userService.Update(user);
            return RedirectToAction("ListUser");
        }


        public IActionResult Delete(int id)
        {
            var user = _userService.FindById(id);
            if (user == null)
            {
                return NotFound();
            }
            _userService.Delete(id);
            return RedirectToAction("ListUser");
        }
    }
}
