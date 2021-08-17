using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUserService _userService;

        public CustomerService(ICustomerRepository customerRepository, IUserService userService)
        {
            _customerRepository = customerRepository;
            _userService = userService;
        }

        public Customer Add(int userId, string lastName, string firstName, string email, string phoneNo, string address)
        {
            var customer = new Customer
            {
                CreatedBy = _userService.FindById(userId).Email,
                CreatedAt = DateTime.Now,
                LastName  = lastName,
                FirstName = firstName,
                Email = email,
                PhoneNo = phoneNo,
                Address = address,
                
            };

            return _customerRepository.Add(customer);
        }

        public Customer FindById(int id)
        {
            return _customerRepository.FindById(id);
        }

        public Customer Update(int customerId, string lastName, string firstName, string email, string phoneNo, string address)
        {
            var customer = _customerRepository.FindById(customerId);
            if(customer == null)
            {
                return null;
            }

            customer.LastName = lastName;
            customer.FirstName = firstName;
            customer.Email = email;
            customer.PhoneNo = phoneNo;
            customer.Address = address;
            customer.UpdatedAt = DateTime.Now;

            return _customerRepository.Update(customer);
        }

        public void Delete(int id)
        {
            _customerRepository.Delete(id);
        }
    }
}
