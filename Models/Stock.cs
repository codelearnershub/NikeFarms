using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Models
{
    public class Stock : BaseEntity
    {
        public string Item { get; set; }

        public double ItemQty { get; set; }

        public double AvailableItem { get; set; }

        public decimal PricePerItem { get; set; }
    }
}
