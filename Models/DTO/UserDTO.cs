using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Models.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }

        public string Email { get; set; }
        
        public string Password { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string PhoneNo { get; set; }

        public string Address { get; set; }

        public int RoleId { get; set; }
    }
}
