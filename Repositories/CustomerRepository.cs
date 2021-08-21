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
        private readonly NikeDbContext _dbContext;

        public CustomerRepository(NikeDbContext dbContext)
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

        public List<Customer> GetAllCustomers()
        {
            return _dbContext.Customers.ToList();
        }

        public Customer Update(Customer customer)
        {
            _dbContext.Customers.Update(customer);
            _dbContext.SaveChanges();
            return customer;
        }
    }
}
