using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Models.DTO
{
    public class SalesDTO
    {
        public int UserId { get; set; }

        public string Item { get; set; }

        public decimal? TotalPrice { get; set; }

        public Customer Customer { get; set; }

        public int CustomerId { get; set; }

        public string Voucher { get; set; }
    }
}
