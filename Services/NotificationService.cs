 using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
using NikeFarms.v2._0.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IUserService _userService;

        public NotificationService(INotificationRepository notificationRepository, IUserService userService)
        {
            _notificationRepository = notificationRepository;
            _userService = userService;
        }

        public Notification Add(NotificationDTO notificationDTO)
        {
            var notification = new Notification
            {
                CreatedBy = _userService.FindById(notificationDTO.UserId).Email,
                CreatedAt = DateTime.Now,
                Content = notificationDTO.Content,
                RecieverId = notificationDTO.RecieverId,
            };

            return _notificationRepository.Add(notification);
        }

        public Notification FindById(int id)
        {
            return _notificationRepository.FindById(id);
        }

        public Notification Update(NotificationDTO notificationDTO)
        {
            var notification = _notificationRepository.FindByApproveId(notificationDTO.ApproveId);
            if (notification == null)
            {
                return null;
            }

            notification.Content = notificationDTO.Content;
            notification.RecieverId = notificationDTO.RecieverId;
            notification.UpdatedAt = DateTime.Now;

            return _notificationRepository.Update(notification);
        }

        public void Delete(int id)
        {
            _notificationRepository.Delete(id);
        }

        public IEnumerable<Notification> GetNotifications(int recieverId)
        {
            return _notificationRepository.GetNotifications(recieverId);
        }

        public Notification FindByApproveId(int approveId)
        {
            return _notificationRepository.FindByApproveId(approveId);
        }
    }
}
