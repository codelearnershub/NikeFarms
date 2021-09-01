using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Models
{
    public class Expenses : BaseEntity
    {
        public string Description { get; set; }

        public decimal Price { get; set;  }

        public string BatchNo { get; set; }

        public bool IsApproved { get; set; }
    }
}
