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
        [Required(ErrorMessage = "Enter Role!!")]
        public string Name { get; set; }
    }


    public class UpdateRoleVM : AddRoleVM
    {
    }
}
