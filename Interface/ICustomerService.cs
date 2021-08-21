using NikeFarms.v2._0.Models;
using NikeFarms.v2._0.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Interface
{
    public interface ICustomerService 
    {
        public Customer Add(CustomerDTO customerDTO);

        public Customer FindById(int id);

        public IEnumerable<Customer> GetAllCustomers();

        public Customer Update(CustomerDTO customerDTO);

        public void Delete(int id);
        
    }
}
