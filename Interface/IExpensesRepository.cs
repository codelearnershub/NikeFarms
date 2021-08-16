using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Interface
{
    public interface IExpensesRepository
    {
        

        public Expenses Add(Expenses expenses);

        public Expenses FindById(int expensesId);

        public Expenses Update(Expenses expenses);

        public void Delete(int expensesId);
    }
}
