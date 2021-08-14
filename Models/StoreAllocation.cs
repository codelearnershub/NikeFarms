using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Models
{
    public class StoreAllocation : BaseEntity
    {
        public StoreItem StoreItem{ get; set; }

        public int StoreItemId { get; set; }

        public double NoOfItem { get; set; }

        public double ItemPerKg { get; set; }
    }
}
