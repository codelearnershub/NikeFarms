using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Interface
{
    public interface ISalesService
    {
        public Sales Add(int userId, string item, int customerId);

        public Sales FindById(int id);

        public Sales Update(int salesId, string item, decimal totalPrice, int customerId);

        public void Delete(int id;
    }
}
