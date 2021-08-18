using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Models.DTO
{
    public class WeeklyReportDTO
    {
        public int UserId { get; set; }

        public double AverageWeight { get; set; }

        public int FlockId { get; set; }
    }
}
