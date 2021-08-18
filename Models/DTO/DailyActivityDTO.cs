﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Models.DTO
{
    public class DailyActivityDTO
    {
        public int UserId { get; set; }

        public double FeedConsumedPerKg { get; set; }

        public int Mortality { get; set; }

        public int NoOfMedUsed { get; set; }

        public int FlockId { get; set; }

        public int StoreAllocationFeedId { get; set; }

        public int StoreAllocationMedId { get; set; }
    }
}
