using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Models
{
    public class Mortality : BaseEntity
    {
        public int NoOfDeaths { get; set; }

        public Stock Stock { get; set; }

        public int? StockId { get; set; }

        public Flock Flock { get; set; }

        public int? FlockId { get; set; }

        public string Date { get; set; }
    }
}
