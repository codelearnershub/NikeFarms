using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
using NikeFarms.v2._0.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Controllers
{
    [Authorize(Roles = "Super Admin, Admin")]
    public class AdminDashboardController : Controller
    {
        
        private readonly IUserService _userService;
        private readonly IAdminDashboardService _adminDashboardService;

        public AdminDashboardController(IUserService userService, IAdminDashboardService adminDashboardService)
        {
            _userService = userService;
            _adminDashboardService = adminDashboardService;
        }

        public IActionResult Index()
        {

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

            AdminDashboardVM dashboardVM = new AdminDashboardVM
            {
                Revenue = _adminDashboardService.Revenue(),
                Expenses =_adminDashboardService.Expenses(),
                Profit =_adminDashboardService.Profit(),
                SalesCount = _adminDashboardService.SalesCount(),
                ActiveMortality = _adminDashboardService.ActiveMortality(),
                AllMortality = _adminDashboardService.AllMortality(),
                ActiveFeed = _adminDashboardService.ActiveFeed(),
                AllFeed = _adminDashboardService.AllFeed(),
                ActiveMed = _adminDashboardService.ActiveMed(),
                AllMed = _adminDashboardService.AllMed(),
            };

            return View(dashboardVM);
        }
    }
}
