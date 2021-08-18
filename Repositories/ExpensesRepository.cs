using NikeFarms.Context;
using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Repositories
{
    public class ExpensesRepository : IExpensesRepository
    {
        private readonly NikeDbContext2 _dbContext;

        public ExpensesRepository(NikeDbContext2 dbContext)
        {
            _dbContext = dbContext;
        }

        public Expenses Add(Expenses expenses)
        {
            _dbContext.Expenses.Add(expenses);
            _dbContext.SaveChanges();
            return expenses;
        }

        public void Delete(int expensesId)
        {
            var expenses = FindById(expensesId);

            if (expenses != null)
            {
                _dbContext.Expenses.Remove(expenses);
                _dbContext.SaveChanges();
            }
        }

        public Expenses FindById(int expensesId)
        {
            return _dbContext.Expenses.FirstOrDefault(u => u.Id.Equals(expensesId));
        }

        public Expenses Update(Expenses expenses)
        {
            _dbContext.Expenses.Update(expenses);
            _dbContext.SaveChanges();
            return expenses;
        }
    }
}
