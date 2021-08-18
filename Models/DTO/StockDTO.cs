using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Models.DTO
{
    public class StockDTO
    {
        public int UserId { get; set; }

        public string Item { get; set; }

        public double NoOfItem { get; set; }

        public double AvailableItem { get; set; }

        public decimal? PricePerCrate { get; set; }

        public decimal? PricePerKg { get; set; }

        public Flock Flock { get; set; }

        public int FlockId { get; set; }
    }
}
