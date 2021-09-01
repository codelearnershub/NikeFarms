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
        private readonly IRoleService _roleService;
        private readonly IDailyActivityService _dailyActivityService;
        private readonly IFlockTypeService _flockTypeService;
        private readonly INotificationRepository _notificationRepository;

        public FlockService(IFlockRepository flockRepository, IUserService userService, INotificationRepository notificationRepository, IUserRoleService userRoleService, IRoleService roleService, IFlockTypeService flockTypeService, IDailyActivityService dailyActivityService)
        {
            _flockRepository = flockRepository;
            _userService = userService;
            _notificationRepository = notificationRepository;
            _userRoleService = userRoleService;
            _roleService = roleService;
            _flockTypeService = flockTypeService;
            _dailyActivityService = dailyActivityService;
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
            if(notify != null )
            {
                var user = _userService.FindById(flockDTO.UserId);
                var role = _roleService.FindByName("Admin");
                notify.Content = $"Manager {user.LastName} {user.FirstName} added Flock: {_flockTypeService.FindById(flockDTO.FlockTypeId).Name}, " +
                    $"Total No: {flockDTO.TotalNo} Bird(s), Age: {flockDTO.Age} (days old) , Price Purchased: N{flockDTO.AmountPurchased}";

                _notificationRepository.Update(notify);
            }
            

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

        public List<Flock>  OperationDaily()
        {
            List<Flock> FlockId = new List<Flock>();
            var flocks = _flockRepository.GetApprovedFlocks();
            foreach(var flock in flocks)
            {
                var daily = _dailyActivityService.GetDailyActivitiesFlockId(flock.Id);
                if(daily == null )
                {
                    FlockId.Add(flock);
                }
                   
            }

            return FlockId;
        }


       

        public int Mortality(int flockId)
        {
            var daily = _dailyActivityService.GetDailyActivitiesPerFlockId(flockId);
            var mortality = 0;
            if(daily != null)
            {
                foreach (var d in daily)
                {
                    mortality += d.Mortality;
                }
            }
            return mortality;
        }
    }
}
