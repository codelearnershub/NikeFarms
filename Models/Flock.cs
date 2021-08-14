﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Models
{
    public class Flock : BaseEntity
    {

        public FlockType FlockType { get; set; }

        public int FlockTypeId { get; set; }

        public string BatchNo { get; set; }

        public int TotalNo { get; set; }

        public int Age { get; set; }

        public double AverageWeight { get; set; }
    }
}
