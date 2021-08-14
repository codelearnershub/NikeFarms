using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Models
{
    public class DailyActivity : BaseEntity
    {
        public double FeedConsumedPerKg { get; set; }

        public int Mortality { get; set; }

        public int NoOfMedUsed { get; set; }

        public Flock Flock { get; set; }

        public int FlockId { get; set; }

        public StoreAllocation StoreAllocationFeed { get; set; }

        public int StoreAllocationFeedId { get; set; }

        public StoreAllocation StoreAllocationMed { get; set; }

        public int StoreAllocationMedId { get; set; }

    }
}
