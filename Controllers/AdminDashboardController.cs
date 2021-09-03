using Microsoft.AspNetCore.Mvc;
using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Controllers
{
    public class AdminDashboardController : Controller
    {
        private readonly IUserService _userService;

        public AdminDashboardController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";
            return View();
        }
    }
}
