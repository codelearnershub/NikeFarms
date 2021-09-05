using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Interface
{
    public interface IAdminDashboardService
    {
        public decimal Revenue();

        public decimal Expenses();

        public decimal Profit();

        public int SalesCount();

        public int ActiveMortality();

        public int AllMortality();

        public double ActiveFeed();

        public double AllFeed();

        public double ActiveMed();

        public double AllMed();
    }
}
