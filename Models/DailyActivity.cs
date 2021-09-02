using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Models
{
    public class DailyActivity : BaseEntity
    {

        public double NoOfMedUsed { get; set; }

        public double NoOfFeedUsed { get; set; }

        public Flock Flock { get; set; }

        public int FlockId { get; set; }

        public StoreAllocation StoreAllocationFeed { get; set; }

        public int StoreAllocationFeedId { get; set; }

        public StoreAllocation StoreAllocationMed { get; set; }

        public int? StoreAllocationMedId { get; set; }

        public string Date { get; set; }

    }
}
