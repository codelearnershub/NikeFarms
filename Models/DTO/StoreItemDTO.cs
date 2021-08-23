using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Models.DTO
{
    public class StoreItemDTO
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ItemType { get; set; }

        public double ItemRemaining { get; set; }

        public double NoOfItem { get; set; }

        public double? ItemPerKg { get; set; }

        public decimal TotalPricePurchased { get; set; }

        public bool IsApproved { get; set; }

    }
}
