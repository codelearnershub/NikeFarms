using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Services
{
    public class ExpensesService : IExpensesService
    {
        private readonly IExpensesRepository _expensesRepository;
        private readonly IUserService _userService;

        public ExpensesService(IExpensesRepository expensesRepository, IUserService userService)
        {
            _expensesRepository = expensesRepository;
            _userService = userService;
        }

        public Expenses Add(int userId, string description, decimal price)
        {
            var expenses = new Expenses
            {
                CreatedBy = _userService.FindById(userId).Email,
                CreatedAt = DateTime.Now,
                Description = description,
                Price = price,
            };

            return _expensesRepository.Add(expenses);
        }

        public Expenses FindById(int id)
        {
            return _expensesRepository.FindById(id);
        }

        public Expenses Update(int expensesId, string description, decimal price)
        {
            var expenses = _expensesRepository.FindById(expensesId);
            if (expenses == null)
            {
                return null;
            }

            expenses.Description = description;
            expenses.Price = price;
            expenses.UpdatedAt = DateTime.Now;

            return _expensesRepository.Update(expenses);
        }

        public void Delete(int id)
        {
            _expensesRepository.Delete(id);
        }
    }
}
