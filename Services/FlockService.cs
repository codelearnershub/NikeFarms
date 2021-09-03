using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
using NikeFarms.v2._0.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Services
{
    public class FlockService : IFlockService
    {
        private readonly IFlockRepository _flockRepository;
        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;
        private readonly IMortalityService _mortalityService;
        private readonly IRoleService _roleService;
        private readonly IDailyActivityService _dailyActivityService;
        private readonly IFlockTypeService _flockTypeService;
        private readonly INotificationRepository _notificationRepository;
        private readonly IWeeklyReportService _weeklyReportService;
        private readonly IStockService _stockService;

        public FlockService(IFlockRepository flockRepository, IUserService userService, INotificationRepository notificationRepository, IUserRoleService userRoleService, IRoleService roleService, IFlockTypeService flockTypeService, IDailyActivityService dailyActivityService, IMortalityService mortalityService, IWeeklyReportService weeklyReportService, IStockService stockService)
        {
            _flockRepository = flockRepository;
            _userService = userService;
            _notificationRepository = notificationRepository;
            _userRoleService = userRoleService;
            _roleService = roleService;
            _flockTypeService = flockTypeService;
            _dailyActivityService = dailyActivityService;
            _mortalityService = mortalityService;
            _weeklyReportService = weeklyReportService;
            _stockService = stockService;
        }

        public Flock Add(FlockDTO flockDTO)
        {
            var flock = new Flock
            {
                CreatedBy = _userService.FindById(flockDTO.UserId).Email,
                CreatedAt = DateTime.Now,
                FlockTypeId = flockDTO.FlockTypeId,
                BatchNo = Guid.NewGuid().ToString().Substring(0, 6).ToUpper(),
                TotalNo = flockDTO.TotalNo,
                Age = flockDTO.Age,
                AverageWeight = flockDTO.AverageWeight,
                AmountPurchased = flockDTO.AmountPurchased,
                AvailableBirds = flockDTO.AvailableBird,
                IsApproved = flockDTO.IsApproved,
            };
            _flockRepository.Add(flock);

            var user = _userService.FindById(flockDTO.UserId);
            var role = _roleService.FindByName("Admin");
            Notification notify = new Notification
            {
                CreatedBy = _userService.FindById(flockDTO.UserId).Email,
                CreatedAt = DateTime.Now,
                Type = "Flock",
                ApproveId = _flockRepository.FindByBatchNo(flock.BatchNo).Id,
                Content = $"Manager {user.LastName} {user.FirstName} added Flock: {_flockTypeService.FindById(flockDTO.FlockTypeId).Name}, " +
                $"Total No: {flockDTO.TotalNo} Bird(s), Age: {flockDTO.Age} (days old) , Price Purchased: N{flockDTO.AmountPurchased}",
                RecieverId = _userRoleService.FindUserWithParticularRole(role.Id).UserId,

            };
            _notificationRepository.Add(notify);
            return flock;
        }

        public IEnumerable<Flock> GetAllFlocks()
        {
            return _flockRepository.GetAllFlocks();
        }

        public Flock FindById(int id)
        {
            return _flockRepository.FindById(id);
        }

        public Flock Update(FlockDTO flockDTO)
        {
            var flock = _flockRepository.FindById(flockDTO.Id);
            if (flock == null)
            {
                return null;
            }

            flock.FlockTypeId = flockDTO.FlockTypeId;
            flock.TotalNo = flockDTO.TotalNo;
            flock.AvailableBirds = flockDTO.AvailableBird;
            flock.Age = flockDTO.Age;
            flock.AverageWeight = flockDTO.AverageWeight;
            flock.AmountPurchased = flockDTO.AmountPurchased;
            flock.IsApproved = flockDTO.IsApproved;
            flock.UpdatedAt = DateTime.Now;

            var notify = _notificationRepository.FindByApproveId(flock.Id);
            if (notify != null)
            {
                var user = _userService.FindById(flockDTO.UserId);
                var role = _roleService.FindByName("Admin");
                notify.Content = $"Manager {user.LastName} {user.FirstName} added Flock: {_flockTypeService.FindById(flockDTO.FlockTypeId).Name}, " +
                    $"Total No: {flockDTO.TotalNo} Bird(s), Age: {flockDTO.Age} (days old) , Price Purchased: N{flockDTO.AmountPurchased}";

                _notificationRepository.Update(notify);
            }


            return _flockRepository.Update(flock);
        }

        public Flock UpdateFlockOnly(FlockDTO flockDTO)
        {
            var flock = _flockRepository.FindById(flockDTO.Id);
            if (flock == null)
            {
                return null;
            }

            flock.FlockTypeId = flockDTO.FlockTypeId;
            flock.TotalNo = flockDTO.TotalNo;
            flock.AvailableBirds = flockDTO.AvailableBird;
            flock.Age = flockDTO.Age;
            flock.AverageWeight = flockDTO.AverageWeight;
            flock.AmountPurchased = flockDTO.AmountPurchased;
            flock.IsApproved = flockDTO.IsApproved;
            flock.UpdatedAt = DateTime.Now;

            return _flockRepository.Update(flock);
        }

        public void Delete(int id)
        {
            _flockRepository.Delete(id);
            _notificationRepository.Delete(id);


        }

        public Flock FindByBatchNo(string batchNo)
        {
            return _flockRepository.FindByBatchNo(batchNo);
        }

        public List<Flock> GetApprovedFlocks()
        {
            return _flockRepository.GetApprovedFlocks();
        }

        public List<Flock> OperationDaily()
        {
            List<Flock> FlockId = new List<Flock>();
            var flocks = _flockRepository.GetApprovedFlocks();
            foreach (var flock in flocks)
            {
                var daily = _dailyActivityService.GetDailyActivitiesFlockId(flock.Id);
                if (daily == null)
                {
                    FlockId.Add(flock);
                }

            }

            return FlockId;
        }

        public List<Flock> OperationFlock()
        {
            List<Flock> FlockId = new List<Flock>();
            var flocks = _flockRepository.GetApprovedFlocks();
            foreach (var flock in flocks)
            {
                var floc = _mortalityService.GetMortalityFlockId(flock.Id);
                if (floc == null)
                {
                    FlockId.Add(flock);
                }

            }

            return FlockId;
        }

        public List<Stock> OperationStock()
        {
            List<Stock> StockId = new List<Stock>();
            var stocks = _stockService.GetAllStocks();
            foreach (var stock in stocks)
            {
                var stoc = _mortalityService.GetMortalityStockId(stock.Id);
                if (stoc == null)
                {
                    StockId.Add(stock);
                }

            }

            return StockId;
        }


        public int Mortality(int flockId)
        {
            var mortality = _mortalityService.GetMortalityPerFlock(flockId);
            int deaths = 0;
            if(mortality != null)
            {
                foreach (var m in mortality)
                {
                    deaths += m.NoOfDeaths;
                }
            }
            return deaths;
        }

        public double GetCurrentAverageWeight(int flockId)
        {
            var weekly = _weeklyReportService.GetWeeklyReportByFlockId(flockId);
            double CAW = _flockRepository.FindById(flockId).AverageWeight;
            int count = 1;
            if (weekly != null)
            {
                foreach (var week in weekly)
                {
                    CAW += week.AverageWeight;
                    count++;
                }
            };
            double result = CAW / count;
            return Math.Round(result, 2);
        }

        public decimal EstimatedPriceOfFlockPerKg(int flockId)
        {
            var flock = _flockRepository.FindById(flockId);
            double currentWeight = GetCurrentAverageWeight(flockId);
            decimal pricePerKg = 0;
            decimal estimatedPrice = 0;
            if (flock != null)
            {
                pricePerKg = flock.AmountPurchased/flock.TotalNo ;
                estimatedPrice =  (decimal)currentWeight*pricePerKg;
            };
            return Math.Round(estimatedPrice, 2);
        }


        public double TotalNoOfStockedBird(int flockId)
        {
            var stockList = _stockService.GetStocksByFlockId(flockId);
            var flock = _flockRepository.FindById(flockId);
            double noOfStock = 0;
            if (stockList != null)
            {
                foreach (var stock in stockList)
                {
                    noOfStock += stock.AvailableItem;
                };
            };
            return noOfStock;
        }

        public void CheckFlockFinish(int flockId)
        {
            var flock = _flockRepository.FindById(flockId);
            if(flock.AvailableBirds == 0)
            {
                
                var role = _roleService.FindByName("Admin");
                Notification notify = new Notification
                {
                    CreatedAt = DateTime.Now,
                    Type = "FlockFinish",
                    ApproveId = flock.Id,
                    Content = $"Flock: {_flockTypeService.FindById(flock.FlockTypeId).Name} with Batch No: {flock.BatchNo} " +
                    $"finished at {DateTime.Now.ToShortTimeString()}, {DateTime.Now.ToLongDateString()}",
                    RecieverId = _userRoleService.FindUserWithParticularRole(role.Id).UserId,

                };
                _notificationRepository.Add(notify);
            }
        }
    }
}
