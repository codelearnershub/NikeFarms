using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
using NikeFarms.v2._0.Models.DTO;
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

        public Sales Add(SalesDTO salesDTO)
        {
            var sales = new Sales
            {
                CreatedBy = _userService.FindById(salesDTO.UserId).Email,
                CreatedAt = DateTime.Now,
                Item = salesDTO.Item,
                CustomerId = salesDTO.CustomerId,
                Voucher = Guid.NewGuid().ToString().Substring(0, 10).ToUpper(),
                IsSold = salesDTO.IsSold,
            };

            return _salesRepository.Add(sales);
        }

        public Sales FindById(int id)
        {
            return _salesRepository.FindById(id);
        }

        public Sales Update(SalesDTO salesDTO)
        {
            var sales = _salesRepository.FindById(salesDTO.Id);
            if (sales == null)
            {
                return null;
            }

            sales.Item = salesDTO.Item;
            sales.TotalPrice = salesDTO.TotalPrice;
            sales.CustomerId = salesDTO.CustomerId;
            sales.IsSold = salesDTO.IsSold;
            sales.UpdatedAt = DateTime.Now;

            return _salesRepository.Update(sales);
        }

        public void Delete(int id)
        {
            _salesRepository.Delete(id);
        }

        public IEnumerable<Sales> GetAllSales()
        {
            return _salesRepository.GetAllSales();
        }

        public IEnumerable<Sales> GetSalesByManagerEmail(string managerEmail)
        {
            return _salesRepository.GetSalesByManagerEmail(managerEmail);
        }
    }
}
