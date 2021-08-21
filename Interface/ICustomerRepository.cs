using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Interface
{
    public interface ICustomerRepository
    {
        public Customer Add(Customer customer);

        public Customer FindById(int customerId);

        public List<Customer> GetAllCustomers();

        public Customer Update(Customer customer);

        public void Delete(int customerId);
    }
}
