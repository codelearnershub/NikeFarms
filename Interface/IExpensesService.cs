using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Interface
{
    public interface IExpensesService
    {
        public Expenses Add(int userId, string description, decimal price);


        public Expenses FindById(int id);

        public Expenses Update(int expensesId, string description, decimal price);

        public void Delete(int id);

    }
}
