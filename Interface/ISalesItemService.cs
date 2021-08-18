using NikeFarms.v2._0.Models;
using NikeFarms.v2._0.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Interface
{
    public interface ISalesItemService
    {
        public SalesItem Add(SalesItemDTO salesItemDTO);

        public SalesItem FindById(int id);

        public SalesItem Update(int salesItemId, SalesItemDTO salesItemDTO);

        public void Delete(int id);
    }
}
