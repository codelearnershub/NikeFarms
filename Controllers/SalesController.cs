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
    public class SalesController : Controller
    {
        private readonly IUserService _userService;
        private readonly ISalesService _salesService;
        private readonly ICustomerService _customerService;

       
        public IActionResult Index()
        {
            var sales = _salesService.GetAllSales();
            List<ListSalesVM> ListFlock = new List<ListSalesVM>();
            foreach (var sale in sales)
            {
                ListSalesVM listSalesVM = new ListSalesVM
                {
                    Id = sale.Id,
                    Item = sale.Item,
                    TotalPrice = sale.TotalPrice,
                    CustomerFullName = $"{_customerService.FindById(sale.CustomerId).LastName} {_customerService.FindById(sale.CustomerId).FirstName}",
                    CreatedBy = $"{_userService.FindByEmail(sale.CreatedBy).FirstName} .{_userService.FindByEmail(sale.CreatedBy).LastName[0]}",
                };

                ListFlock.Add(listSalesVM);
            }

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

            return View(ListFlock);
        }

        public IActionResult AddSales()
        {
            
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

            return View();
        }

        [HttpPost]
        public IActionResult AddSales(AddSalesVM addSales)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var customer = _customerService.FindByEmail(addSales.CustomerEmail);
            if (customer == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                SalesDTO SalesDTO = new SalesDTO
                {
                    UserId = userId,
                    Item = addSales.Item,
                    CustomerId = customer.Id,
                    IsSold = false,
                };


                _salesService.Add(SalesDTO);
                return RedirectToAction("Index");
            }
            
        }


        public IActionResult UpdateSales(int id)
        {
            var sale = _salesService.FindById(id);
            if (sale == null)
            {
                return NotFound();
            }
            else
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
                User userlogin = _userService.FindById(userId);
                ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

                UpdateSalesVM updateSales = new UpdateSalesVM
                {
                    Id = sale.Id,
                    Item = sale.Item,
                    CustomerEmail = _customerService.FindById(sale.CustomerId).Email,
                    
                };

                return View(updateSales);
            }

        }

        [HttpPost]
        public IActionResult UpdateSales(UpdateSalesVM updateSales)
        {
            SalesDTO sale = new SalesDTO
            {
                Id = updateSales.Id,
                Item = updateSales.Item,
                CustomerId = _customerService.FindByEmail(updateSales.CustomerEmail).Id,
                IsSold = false,
            };
            _salesService.Update(sale);
            return RedirectToAction("Index");
        }



        public IActionResult Delete(int id)
        {
            var sale = _salesService.FindById(id);
            if (sale == null)
            {
                return NotFound();
            }
            _salesService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
