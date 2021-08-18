using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;
        private readonly IRoleService _roleService;

        public UserController(IUserService userService, IUserRoleService userRoleService, IRoleService roleService)
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
            return RedirectToAction("ListManager");
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vm)
        {

            UserDTO userDTO = new UserDTO
            {
                Email = vm.Email,
                Password = vm.Password,
            };

            User user = _userService.LoginUser(userDTO);

            if (user == null)
            {
                ViewBag.Message = "error";
                return View();
            }

            var role = _userRoleService.FindRole(user.Id);
            if (role == "SuperAdmin")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, "SuperAdmin")

                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                var props = new AuthenticationProperties();

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);
            }
            else
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, "Manager")


                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                var props = new AuthenticationProperties();

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);
            }


            if (role == "SuperAdmin")
            {
                return RedirectToAction("RegisterUser");
            }
            else
            {
                return RedirectToAction("Home", "Index");
            }


        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
