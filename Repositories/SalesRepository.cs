﻿using NikeFarms.Context;
using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Repositories
{
    public class SalesRepository : ISalesRepository
    {
        private readonly NikeDbContext2 _dbContext;

        public SalesRepository(NikeDbContext2 dbContext)
        {
            _dbContext = dbContext;
        }

        public Sales Add(Sales sales)
        {
            _dbContext.Sales.Add(sales);
            _dbContext.SaveChanges();
            return sales;
        }

        public void Delete(int salesId)
        {
            var sales = FindById(salesId);

            if (sales != null)
            {
                _dbContext.Sales.Remove(sales);
                _dbContext.SaveChanges();
            }
        }

        public Sales FindById(int salesId)
        {
            return _dbContext.Sales.FirstOrDefault(u => u.Id.Equals(salesId));
        }

        public Sales Update(Sales sales)
        {
            _dbContext.Sales.Update(sales);
            _dbContext.SaveChanges();
            return sales;
        }
    }
}
