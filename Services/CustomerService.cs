using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
using NikeFarms.v2._0.Models.DTO;
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

        public Customer Add(CustomerDTO customerDTO)
        {
            var customer = new Customer
            {
                CreatedBy = _userService.FindById(customerDTO.UserId).Email,
                CreatedAt = DateTime.Now,
                LastName  = customerDTO.LastName,
                FirstName = customerDTO.FirstName,
                Email = customerDTO.Email,
                PhoneNo = customerDTO.PhoneNo,
                Address = customerDTO.Address,
                
            };

            return _customerRepository.Add(customer);
        }

        public Customer FindById(int id)
        {
            return _customerRepository.FindById(id);
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _customerRepository.GetAllCustomers();
        }

        public Customer Update(CustomerDTO customerDTO)
        {
            var customer = _customerRepository.FindById(customerDTO.Id);
            if(customer == null)
            {
                return null;
            }

            customer.LastName = customerDTO.LastName;
            customer.FirstName = customerDTO.FirstName;
            customer.Email = customerDTO.Email;
            customer.PhoneNo = customerDTO.PhoneNo;
            customer.Address = customerDTO.Address;
            customer.UpdatedAt = DateTime.Now;

            return _customerRepository.Update(customer);
        }

        public void Delete(int id)
        {
            _customerRepository.Delete(id);
        }
    }
}
