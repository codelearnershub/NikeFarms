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
    public class SalesController : Controller
    {
        private readonly IUserService _userService;
        private readonly ISalesService _salesService;
        private readonly ICustomerService _customerService;

        public SalesController(IUserService userService, ISalesService salesService, ICustomerService customerService)
        {
            _userService = userService;
            _salesService = salesService;
            _customerService = customerService;
        }

        public IActionResult Index()
        {
            var sales = _salesService.GetAllSales();
            List<ListSalesVM> ListFlock = new List<ListSalesVM>();
            foreach (var sale in sales)
            {
                var Created = _userService.FindByEmail(sale.CreatedBy);
                ListSalesVM listSalesVM = new ListSalesVM
                {
                    Id = sale.Id,
                    Item = sale.Item,
                    TotalPrice = sale.TotalPrice,
                    CustomerFullName = $"{_customerService.FindById(sale.CustomerId).LastName} {_customerService.FindById(sale.CustomerId).FirstName}",
                    CreatedBy = $"{Created.FirstName} .{Created.LastName[0]}",
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
            AddSalesVM stockVM = new AddSalesVM
            {
                CustomerList = _customerService.GetAllCustomers().Select(m => new SelectListItem
                {
                    Text = $"{_customerService.FindById(m.Id).LastName} {_customerService.FindById(m.Id).FirstName} Email: @{m.Email}",
                    Value = m.Id.ToString()
                })
            };

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

            return View();
        }

        [HttpPost]
        public IActionResult AddSales(AddSalesVM addSales)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));


            SalesDTO SalesDTO = new SalesDTO
            {
                UserId = userId,
                Item = addSales.Item,
                CustomerId = addSales.CustomerId,
                IsSold = false,
            };


            _salesService.Add(SalesDTO);
            return RedirectToAction("Index");


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
                    CustomerList = _customerService.GetAllCustomers().Select(m => new SelectListItem
                    {
                        Text = $"{_customerService.FindById(m.Id).LastName} {_customerService.FindById(m.Id).FirstName} Email: @{m.Email}",
                        Value = m.Id.ToString()
                    }),

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
                CustomerId = updateSales.CustomerId,
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
