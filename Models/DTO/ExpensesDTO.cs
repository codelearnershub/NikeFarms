using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Models.DTO
{
    public class ExpensesDTO
    {
        public int UserId { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
