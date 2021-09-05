using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
using NikeFarms.v2._0.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Services
{
    public class ExpensesService : IExpensesService
    {
        private readonly IExpensesRepository _expensesRepository;
        private readonly IUserService _userService;
        private readonly INotificationRepository _notificationRepository;
        private readonly IUserRoleService _userRoleService;
        private readonly IRoleService _roleService;

        public ExpensesService(IExpensesRepository expensesRepository, IUserService userService, INotificationRepository notificationRepository, IUserRoleService userRoleService, IRoleService roleService)
        {
            _expensesRepository = expensesRepository;
            _userService = userService;
            _notificationRepository = notificationRepository;
            _userRoleService = userRoleService;
            _roleService = roleService;
        }

        public Expenses Add(ExpensesDTO expensesDTO)
        {
            var expenses = new Expenses
            {
                CreatedBy = _userService.FindById(expensesDTO.UserId).Email,
                CreatedAt = DateTime.Now,
                Description = expensesDTO.Description,
                BatchNo = Guid.NewGuid().ToString().Substring(0, 6).ToUpper(),
                Price = expensesDTO.Price,
                IsApproved = expensesDTO.IsApproved,
            };

            _expensesRepository.Add(expenses);

            var user = _userService.FindById(expensesDTO.UserId);
            var role = _roleService.FindByName("Admin");
            Notification notify = new Notification
            {
                CreatedBy = _userService.FindById(expensesDTO.UserId).Email,
                CreatedAt = DateTime.Now,
                Type = "Expenses",
                ApproveId = _expensesRepository.FindByBatchNo(expenses.BatchNo).Id,
                Content = $"Manager {user.LastName} {user.FirstName} Spent: N{expensesDTO.Price}, On: {expensesDTO.Description}",
                RecieverId = _userRoleService.FindUserWithParticularRole(role.Id).UserId,
            };
            _notificationRepository.Add(notify);

            return expenses;
        }

        public Expenses FindById(int id)
        {
            return _expensesRepository.FindById(id);
        }

        public Expenses Update(ExpensesDTO expensesDTO)
        {
            var expenses = _expensesRepository.FindById(expensesDTO.Id);
            if (expenses == null)
            {
                return null;
            }

            expenses.Description = expensesDTO.Description;
            expenses.Price = expensesDTO.Price;
            expenses.UpdatedAt = DateTime.Now;
            expenses.IsApproved = expensesDTO.IsApproved;

            var notify = _notificationRepository.FindByApproveId(expenses.Id);
            if (notify != null)
            {
                var user = _userService.FindById(expensesDTO.UserId);
                var role = _roleService.FindByName("Admin");
                notify.Content = $"Manager {user.LastName} {user.FirstName} Spent: N{expensesDTO.Price}, On: {expensesDTO.Description}";

                _notificationRepository.Update(notify);
            }

            return _expensesRepository.Update(expenses);
        }

        public void Delete(int id)
        {
            _expensesRepository.Delete(id);
            _notificationRepository.Delete(id);
        }

        public IEnumerable<Expenses> GetAllExpenses()
        {
            return _expensesRepository.GetAllExpenses();
        }

        public Expenses FindByBatchNo(string batchNo)
        {
            return _expensesRepository.FindByBatchNo(batchNo);
        }

        public IEnumerable<Expenses> GetApprovedExpenses()
        {
            return _expensesRepository.GetApprovedExpenses();
        }
    }
}
