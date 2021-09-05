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
    [Authorize(Roles = "Super Admin")]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;

        public RoleController(IRoleService roleService, IUserService userService)
        {
            _roleService = roleService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

            return View();
        }

        public IActionResult AddRole()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

            return View();
        }

        [HttpPost]
        public IActionResult AddRole(AddRoleVM addRole)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            RoleDTO role = new RoleDTO
            {
                UserId = userId,
                Name = addRole.Name,
            };

            _roleService.Add(role);
            return RedirectToAction("ListRole");
        }

        public IActionResult ListRole()
        {
            var roles = _roleService.GetAllRoles();
            List<ListRoleVM> ListRole = new List<ListRoleVM>();

            foreach (var role in roles)
            {
                ListRoleVM listRoleVM = new ListRoleVM
                {

                    Id = role.Id,
                    Name = role.Name,
                    CreatedBy = $"{_userService.FindByEmail(role.CreatedBy).FirstName} .{_userService.FindByEmail(role.CreatedBy).LastName[0]}",
                };

                ListRole.Add(listRoleVM);
            }

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

            return View(ListRole);
        }

        public IActionResult UpdateRole(int id)
        {
            var role = _roleService.FindById(id);
            if (role == null)
            {
                return NotFound();
            }
            else
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
                User userlogin = _userService.FindById(userId);
                ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

                UpdateRoleVM updateRole = new UpdateRoleVM
                {
                    Name = role.Name,
                    Id = role.Id,
                };

                return View(updateRole);
            }
           
        }

        [HttpPost]
        public IActionResult UpdateRole(UpdateRoleVM updateRole)
        {
            RoleDTO role = new RoleDTO
            {
                Id = updateRole.Id,
                Name = updateRole.Name,
            };
            _roleService.Update(role);
            return RedirectToAction("ListRole");
        }



        public IActionResult Delete(int id)
        {
            var role = _roleService.FindById(id);
            if (role == null)
            {
                return NotFound();
            }
            _roleService.Delete(id);
            return RedirectToAction("ListRole");
        }
    }
}
