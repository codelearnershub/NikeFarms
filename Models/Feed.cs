using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Models
{
    public class Feed : BaseEntity
    {
        public string Name { get; set; }

        public decimal PricePerKg { get; set; }

        public double StockedFeed { get; set; }

        public double AvailableFeed { get; set; }
    }
}
