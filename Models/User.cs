using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Models
{
    public class User: BaseEntity
    {
        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string HashSalt { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string PhoneNo { get; set; }

        public string Address { get; set; }

        public string Gender { get; set; }

        List<UserRole> UserRoles { get; set; }

        List<Message> Messages { get; set; }
    }
}
