using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Models
{
    public class SalesItem : BaseEntity
    {
        public string Item { get; set; }

        public int ItemQty { get; set; }

        public Stock Stock { get; set; }

        public int StockId { get; set; }

        public Sales Sales { get; set; }

        public int SalesId { get; set; }

        public decimal PricePerItem { get; set; }
    }
}
