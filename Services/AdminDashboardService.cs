using NikeFarms.v2._0.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 
namespace NikeFarms.v2._0.Services
{
    
    public class AdminDashboardService : IAdminDashboardService
    {
        private readonly ISalesService _salesService;
        private readonly IFlockService _flockService;
        private readonly IStoreItemService _storeItemService;
        private readonly IExpensesService _expensesService;
        private readonly IMortalityService _mortalityService;
        private readonly IDailyActivityService _dailyService;

        public AdminDashboardService(ISalesService salesService, IFlockService flockService, IStoreItemService storeItemService, IExpensesService expensesService, IMortalityService mortalityService, IDailyActivityService dailyService)
        {
            _salesService = salesService;
            _flockService = flockService;
            _storeItemService = storeItemService;
            _expensesService = expensesService;
            _mortalityService = mortalityService;
            _dailyService = dailyService;
        }

        public decimal Revenue()
        {
            var sales = _salesService.GetSoldSales();
            decimal revenue = 0;
            foreach (var sale in sales)
            {
                revenue += (decimal)sale.TotalPrice;
            }

            return Math.Round(revenue, 2);
        }

        public decimal Expenses()
        {
            var flocks = _flockService.GetApprovedFlocks();
            var storeItems = _storeItemService.GetApprovedStoreItems();
            var expenses = _expensesService.GetApprovedExpenses();
            decimal totalFlockPrice = 0;
            decimal totalStoreItemPrice = 0;
            decimal totalExpensesPrice = 0;
            decimal totalExpenses = 0;

            foreach(var flock in flocks)
            {
                totalFlockPrice += flock.AmountPurchased;
            }

            foreach(var s in storeItems)
            {
                totalStoreItemPrice += s.TotalPricePurchased;
            }

            foreach(var expense in expenses)
            {
                totalExpensesPrice += expense.Price;
            }

            totalExpenses = totalExpensesPrice + totalFlockPrice + totalStoreItemPrice;

            return Math.Round(totalExpenses, 2);

        }

        public decimal Profit()
        {
            var profit = Revenue() - Expenses();
            return Math.Round(profit, 2);
        }

        public int SalesCount()
        {
            var sales = _salesService.GetSoldSales();

            return sales.Count();
        }

        public int ActiveMortality()
        {
            var mortalities = _mortalityService.GetAllMortality();
            int mortality = 0;
            foreach (var m in mortalities)
            {
                var flock = _flockService.FindById((int)m.FlockId);
                if(flock.AvailableBirds > 0)
                {
                    mortality += m.NoOfDeaths;
                }
            }
            return mortality;
        }

        public int AllMortality()
        {
            var mortalities = _mortalityService.GetAllMortality();
            int mortality = 0;
            foreach(var m in mortalities)
            {
                mortality += m.NoOfDeaths;
            }

            return mortality; 
        }

        public double ActiveFeed()
        {
            var daily = _dailyService.GetAllDailyActivities();
            double totalFeed = 0;

            foreach (var d in daily)
            {
                var flock = _flockService.FindById((int)d.FlockId);
                if (flock.AvailableBirds > 0)
                {
                    totalFeed += d.NoOfFeedUsed;
                }
            }
            return totalFeed;
        }

        public double AllFeed()
        {
            var daily = _dailyService.GetAllDailyActivities();
            double totalFeed = 0;
            foreach(var d in daily)
            {
                totalFeed += d.NoOfFeedUsed;
            }

            return totalFeed;
        }

        public double ActiveMed()
        {
            var daily = _dailyService.GetAllDailyActivities();
            double totalMed = 0;

            foreach (var d in daily)
            {
                var flock = _flockService.FindById((int)d.FlockId);
                if (flock.AvailableBirds > 0)
                {
                    totalMed += d.NoOfMedUsed;
                }
            }
            return totalMed;
        }

        public double AllMed()
        {
            var daily = _dailyService.GetAllDailyActivities();
            double totalMed = 0;
            foreach (var d in daily)
            {
                totalMed += d.NoOfMedUsed;
            }

            return totalMed;
        }
    }
}
