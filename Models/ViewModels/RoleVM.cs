using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Models.ViewModels
{
    public class RoleVM
    {
    }

    public class AddRoleVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter Role!!")]
        public string Name { get; set; }
    }

    public class ListRoleVM
    {
        public int Id { get; set; }

        public string CreatedBy { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; }
    }

    public class UpdateRoleVM : AddRoleVM
    {
    }
}
