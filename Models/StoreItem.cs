﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Models
{
    public class StoreItem : BaseEntity
    {
        public string Name { get; set; }

        public string ItemType { get; set; }

        public double NoOfItem { get; set; }

        public double ItemPerKg { get; set; }
    }
}
