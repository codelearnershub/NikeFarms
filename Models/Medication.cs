using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Models
{
    public class Medication : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public double Qty { get; set; }

        public double Available { get; set; }

        public decimal PricePerItem { get; set; }
    }
}
