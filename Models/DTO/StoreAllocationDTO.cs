using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Models.DTO
{
    public class StoreAllocationDTO
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int StoreItemId { get; set; }

        public double NoOfItem { get; set; }

        public double? ItemPerKg { get; set; }

        public double ItemRemaining { get; set; }

        public string ItemType { get; set; }

        public int ManagerId { get; set; }

        public bool IsApproved { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
