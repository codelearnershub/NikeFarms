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
    public class ExpensesController : Controller
    {
        private readonly IExpensesService _expensesService;
        private readonly IUserService _userService;

        public ExpensesController(IExpensesService expensesService, IUserService userService)
        {
            _expensesService = expensesService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            var expenses = _expensesService.GetAllExpenses();
            List<ListExpensesVM> ListExpenses = new List<ListExpensesVM>();

            foreach (var expense in expenses)
            {
                ListExpensesVM listExpensesVM = new ListExpensesVM
                {

                    Id = expense.Id,
                    Description = expense.Description,
                    Price = expense.Price,
                    CreatedBy = $"{_userService.FindByEmail(expense.CreatedBy).FirstName} .{_userService.FindByEmail(expense.CreatedBy).LastName[0]}",
                };

                ListExpenses.Add(listExpensesVM);
            }

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

            return View(ListExpenses);

        }

        public IActionResult AddExpenses()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

            return View();
        }

        [HttpPost]
        public IActionResult AddExpenses(AddExpensesVM addExpenses)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            ExpensesDTO expenses = new ExpensesDTO
            {
                UserId = userId,
                Description = addExpenses.Description,
                Price = addExpenses.Price,
            };

            _expensesService.Add(expenses);
            return RedirectToAction("ListRole");
        }


        public IActionResult UpdateExpenses(int id)
        {
            var expense = _expensesService.FindById(id);
            if (expense == null)
            {
                return NotFound();
            }
            else
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
                User userlogin = _userService.FindById(userId);
                ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

                UpdateExpensesVM updateExpenses = new UpdateExpensesVM
                {
                    Description = expense.Description,
                    Price = expense.Price,
                };

                return View(updateExpenses);
            }

        }

        [HttpPost]
        public IActionResult UpdateExpenses(UpdateExpensesVM updateExpenses)
        {
            ExpensesDTO expenses = new ExpensesDTO
            {
                Id = updateExpenses.Id,
                Description = updateExpenses.Description,
                Price = updateExpenses.Price,
            };
            _expensesService.Update(expenses);
            return RedirectToAction("Index");
        }



        public IActionResult Delete(int id)
        {
            var expense = _expensesService.FindById(id);
            if (expense == null)
            {
                return NotFound();
            }
            _expensesService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
