using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Interface
{
    public interface INotificationRepository
    {
        public Notification Add(Notification notification);

        public void Delete(int notificationId);

        public Notification FindById(int notificationId);

        public Notification FindByApproveId(int approveId);

        public List<Notification> GetNotifications(int recieverId);


        public Notification Update(Notification notification);
    }
}
