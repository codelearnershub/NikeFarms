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
        private readonly ISalesService _salesService;

        public CustomerService(ICustomerRepository customerRepository, IUserService userService, ISalesService salesService)
        {
            _customerRepository = customerRepository;
            _userService = userService;
            _salesService = salesService;
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
                Gender = customerDTO.Gender,
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
            customer.Gender = customerDTO.Gender;
            customer.Address = customerDTO.Address;
            customer.UpdatedAt = DateTime.Now;

            return _customerRepository.Update(customer);
        }

        public void Delete(int id)
        {
            _customerRepository.Delete(id);
        }

        public Customer FindByEmail(string customerEmail)
        {
            return _customerRepository.FindByEmail(customerEmail);
        }

        public decimal TotalPriceSpent(int customerId)
        {
            var sales = _salesService.FindSalesByCustomerId(customerId);
            decimal total = 0;
            foreach(var sale in sales)
            {
                total += (decimal)sale.TotalPrice;
            }

            return total;
        }
    }
}
