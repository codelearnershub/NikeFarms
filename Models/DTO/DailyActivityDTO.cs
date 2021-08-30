using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Models.DTO
{
    public class DailyActivityDTO
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public double FeedConsumedPerKg { get; set; }

        public int Mortality { get; set; }

        public double NoOfMedUsed { get; set; }

        public double NoOfFeedUsed { get; set; }

        public int FlockId { get; set; }

        public int StoreAllocationFeedId { get; set; }

        public int? StoreAllocationMedId { get; set; }
    }
}
