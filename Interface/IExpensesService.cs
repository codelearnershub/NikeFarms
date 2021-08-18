﻿using NikeFarms.v2._0.Models;
using NikeFarms.v2._0.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Interface
{
    public interface IExpensesService
    {
        public Expenses Add(ExpensesDTO expensesDTO);


        public Expenses FindById(int id);

        public Expenses Update(int expensesId, ExpensesDTO expensesDTO);

        public void Delete(int id);

    }
}
