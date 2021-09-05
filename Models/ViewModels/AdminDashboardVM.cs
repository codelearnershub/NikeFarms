using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Models.ViewModels
{
    public class AdminDashboardVM
    {
        public decimal Revenue { get; set; }

        public decimal Expenses { get; set; }

        public decimal Profit { get; set; }

        public int SalesCount { get; set; }

        public int ActiveMortality { get; set; }

        public int AllMortality { get; set; }

        public double ActiveFeed { get; set; }

        public double AllFeed { get; set; }

        public double ActiveMed { get; set; }

        public double AllMed { get; set; }
    }
}
