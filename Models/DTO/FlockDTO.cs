using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Models.DTO
{
    public class FlockDTO
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int FlockTypeId { get; set; }

        public string BatchNo { get; set; }

        public int TotalNo { get; set; }

        public int Age { get; set; }

        public double AverageWeight { get; set; }
    }
}
