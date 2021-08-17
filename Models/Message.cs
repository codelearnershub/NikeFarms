using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Models
{
    public class Message : BaseEntity
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public User Reciever { get; set; }

        public int RecieverId { get; set; }
    }
}
