using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Models.DTO
{
    public class NotificationDTO
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int ApproveId { get; set; }

        public string Content { get; set; }

        public string Type { get; set; }

        public User Reciever { get; set; }

        public int RecieverId { get; set; }
    }
}
