using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Models.ViewModels
{
    public class NotificationVM
    {
        
    }

    public class ListNotificationVM
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int ApproveId { get; set; }

        public DateTime CreatedAt { get; set; }
    }

}
