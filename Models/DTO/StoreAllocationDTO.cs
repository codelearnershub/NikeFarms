using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Models.DTO
{
    public class StoreAllocationDTO
    {
        public int UserId { get; set; }

        public int StoreItemId { get; set; }

        public double NoOfItem { get; set; }

        public double? ItemPerKg { get; set; }

        public string ItemType { get; set; }

        public int ManagerId { get; set; }
    }
}
