using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Models
{
    public class DailyActivity : BaseEntity
    {
        public double FeedConsumed { get; set; }

        public int Mortality { get; set; }

        public Medication Medication { get; set; }

        public int MedicationId { get; set; }

        public Flock Flock { get; set; }

        public int FlockId { get; set; }




    }
}
