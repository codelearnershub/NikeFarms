using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Models
{
    public class WeeklyReport : BaseEntity
    {
        public double AverageWeight { get; set; }

        public Flock Flock { get; set; }

        public int FlockId { get; set; }

        public string Date { get; set; }
    }
}
