using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Models
{
    public class Sales : BaseEntity
    {
        public string Item { get; set; }

        public decimal? TotalPrice { get; set; }
       
        public Customer Customer { get; set; }

        public int CustomerId { get; set; }

        public string Voucher { get; set; }

        List<SalesItem> SalesItems { get; set; }
    }
}
