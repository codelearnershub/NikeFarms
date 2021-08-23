﻿using Microsoft.AspNetCore.Mvc;
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
    public class CustomerController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICustomerService _customerService;

        public CustomerController(IUserService userService, ICustomerService flockService)
        {
            _userService = userService;
            _customerService = flockService;
        }

        public IActionResult Index()
        {
            var customers = _customerService.GetAllCustomers();
            List<ListCustomerVM> ListCustomer = new List<ListCustomerVM>();

            foreach (var customer in customers)
            {
                ListCustomerVM listCustomerVM = new ListCustomerVM
                {
                    Id = customer.Id,
                    FullName = $"{customer.LastName} {customer.FirstName}",
                    Email = customer.Email,
                    PhoneNo = customer.PhoneNo,
                    Address = customer.Address,
                    Gender = customer.Gender,
                };

                ListCustomer.Add(listCustomerVM);
            }

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

            return View(ListCustomer);
        }

        public IActionResult AddCustomer()
        {

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

            return View();
        }

        [HttpPost]
        public IActionResult AddCustomer(AddCustomerVM addCustomer)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            CustomerDTO customerDTO = new CustomerDTO
            {
                UserId = userId,
                FirstName = addCustomer.FirstName,
                LastName = addCustomer.LastName,
                Email = addCustomer.Email,
                Gender = addCustomer.Gender,
                PhoneNo = addCustomer.PhoneNo,
                Address = addCustomer.Address,
            };

            _customerService.Add(customerDTO);
            return RedirectToAction("Index");
        }


        public IActionResult UpdateCustomer(int id)
        {
            var customer = _customerService.FindById(id);
            if (customer == null)
            {
                return NotFound();
            }
            else
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
                User userlogin = _userService.FindById(userId);
                ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

                UpdateCustomerVM updateCustomer = new UpdateCustomerVM
                {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    PhoneNo = customer.PhoneNo,
                    Address = customer.Address,
                };

                return View(updateCustomer);
            }

        }

        [HttpPost]
        public IActionResult UpdateCustomer(UpdateCustomerVM updateCustomer)
        {
            CustomerDTO customer = new CustomerDTO
            {
                Id = updateCustomer.Id,
                FirstName = updateCustomer.FirstName,
                LastName = updateCustomer.LastName,
                PhoneNo = updateCustomer.PhoneNo,
                Gender = updateCustomer.Gender,
                Email = updateCustomer.Email,
                Address = updateCustomer.Address,
                
            };
            _customerService.Update(customer);
            return RedirectToAction("Index");
        }



        public IActionResult Delete(int id)
        {
            var customer = _customerService.FindById(id);
            if (customer == null)
            {
                return NotFound();
            }
            _customerService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
