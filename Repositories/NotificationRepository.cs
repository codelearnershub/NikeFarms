using NikeFarms.Context;
using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly NikeDbContext _dbContext;

        public NotificationRepository(NikeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Notification Add(Notification notification)
        {
            _dbContext.Notifications.Add(notification);
            _dbContext.SaveChanges();
            return notification;
        }

        public void Delete(int notificationId)
        {
            var notification = FindByApproveId(notificationId);

            if (notification != null)
            {
                _dbContext.Notifications.Remove(notification);
                _dbContext.SaveChanges();
            }
        }

        public Notification FindById(int notificationId)
        {
            return _dbContext.Notifications.FirstOrDefault(u => u.Id.Equals(notificationId));
        }

        public Notification FindByApproveId(int approveId)
        {
            return _dbContext.Notifications.FirstOrDefault(u => u.ApproveId.Equals(approveId));
        }

        public List<Notification> GetNotifications(int recieverId)
        {
            return _dbContext.Notifications.Where(m => m.RecieverId == recieverId).OrderByDescending(r => r.CreatedAt).ToList();
        }


        public Notification Update(Notification notification)
        {
            _dbContext.Notifications.Update(notification);
            _dbContext.SaveChanges();
            return notification;
        }
    }
}
