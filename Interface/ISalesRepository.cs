using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Interface
{
    public interface ISalesRepository
    {
        public Sales Add(Sales sales);

        public Sales FindById(int salesId);

        public Sales Update(Sales sales);

        public List<Sales> GetAllSales();

        public List<Sales> GetSalesByManagerEmail(string managerEmail);

        public void Delete(int salesId);
    }
}
