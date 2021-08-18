using NikeFarms.Context;
using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly NikeDbContext2 _dbContext;

        public CustomerRepository(NikeDbContext2 dbContext)
        {
            _dbContext = dbContext;
        }

        public Customer Add(Customer customer)
        {
            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();
            return customer;
        }

        public void Delete(int customerId)
        {
            var customer = FindById(customerId);

            if (customer != null)
            {
                _dbContext.Customers.Remove(customer);
                _dbContext.SaveChanges();
            }
        }

        public Customer FindById(int customerId)
        {
            return _dbContext.Customers.FirstOrDefault(u => u.Id.Equals(customerId));
        }

        public Customer Update(Customer customer)
        {
            _dbContext.Customers.Update(customer);
            _dbContext.SaveChanges();
            return customer;
        }
    }
}
