using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
using NikeFarms.v2._0.Models.DTO;
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

        public Expenses Add(ExpensesDTO expensesDTO)
        {
            var expenses = new Expenses
            {
                CreatedBy = _userService.FindById(expensesDTO.UserId).Email,
                CreatedAt = DateTime.Now,
                Description = expensesDTO.Description,
                Price = expensesDTO.Price,
            };

            return _expensesRepository.Add(expenses);
        }

        public Expenses FindById(int id)
        {
            return _expensesRepository.FindById(id);
        }

        public Expenses Update(ExpensesDTO expensesDTO)
        {
            var expenses = _expensesRepository.FindById(expensesDTO.Id);
            if (expenses == null)
            {
                return null;
            }

            expenses.Description = expensesDTO.Description;
            expenses.Price = expensesDTO.Price;
            expenses.UpdatedAt = DateTime.Now;

            return _expensesRepository.Update(expenses);
        }

        public void Delete(int id)
        {
            _expensesRepository.Delete(id);
        }

        public IEnumerable<Expenses> GetAllExpenses()
        {
            return _expensesRepository.GetAllExpenses();
        }
    }
}
