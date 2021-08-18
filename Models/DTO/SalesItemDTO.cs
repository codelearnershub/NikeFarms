using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Models.DTO
{
    public class SalesItemDTO
    {
        public int UserId { get; set; }

        public string Item { get; set; }

        public int NoOfItem { get; set; }

        public int StockId { get; set; }

        public int SalesId { get; set; }

        public decimal PricePerItem { get; set; }
    }
}
