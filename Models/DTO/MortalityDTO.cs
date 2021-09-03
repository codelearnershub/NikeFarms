using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Models.DTO
{
    public class MortalityDTO
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int NoOfDeaths { get; set; }

        public int? StockId { get; set; }

        public int? FlockId { get; set; }
    }
}
