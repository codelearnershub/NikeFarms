using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Interface
{
    public interface ICustomerService 
    {
        public Customer Add(int userId, string lastName, string firstName, string email, string phoneNo, string address);

        public Customer FindById(int id);

        public Customer Update(int customerId, string lastName, string firstName, string email, string phoneNo, string address);

        public void Delete(int id);
        
    }
}
