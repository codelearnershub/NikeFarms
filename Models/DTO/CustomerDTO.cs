using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Models.DTO
{
    public class CustomerDTO
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Gender { get; set; }

        public string Email { get; set; }

        public string PhoneNo { get; set; }

        public string Address { get; set; }
    }
}
