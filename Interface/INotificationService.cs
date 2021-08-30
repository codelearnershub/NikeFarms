using NikeFarms.v2._0.Models;
using NikeFarms.v2._0.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Interface
{
    public interface INotificationService
    {
        public Notification Add(NotificationDTO notificationDTO);

        public Notification FindById(int id);

        public Notification FindByApproveId(int approveId);

        public Notification Update(NotificationDTO notificationDTO);

        public void Delete(int id);

        public IEnumerable<Notification> GetNotifications(int recieverId);
    }
}
