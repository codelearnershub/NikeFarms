using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Services
{
    public class SalesService : ISalesService
    {
        private readonly ISalesRepository _salesRepository;
        private readonly IUserService _userService;

        public SalesService(ISalesRepository salesRepository, IUserService userService)
        {
            _salesRepository = salesRepository;
            _userService = userService;
        }

        public Sales Add(int userId, string item, int customerId)
        {
            var sales = new Sales
            {
                CreatedBy = _userService.FindById(userId).Email,
                CreatedAt = DateTime.Now,
                Item = item,
                CustomerId = customerId,
                Voucher = (Guid.NewGuid()).ToString("0000000000"),
            };

            return _salesRepository.Add(sales);
        }

        public Sales FindById(int id)
        {
            return _salesRepository.FindById(id);
        }

        public Sales Update(int salesId, string item, decimal totalPrice, int customerId)
        {
            var sales = _salesRepository.FindById(salesId);
            if (sales == null)
            {
                return null;
            }

            sales.Item = item;
            sales.TotalPrice = totalPrice;
            sales.CustomerId = customerId;
            sales.UpdatedAt = DateTime.Now;

            return _salesRepository.Update(sales);
        }

        public void Delete(int id)
        {
            _salesRepository.Delete(id);
        }
    }
}
