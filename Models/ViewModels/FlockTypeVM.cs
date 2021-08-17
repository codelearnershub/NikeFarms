using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Models.ViewModels
{
    public class FlockTypeVM
    {
    }

    public class AddFlockTypeVM
    {
        [Required(ErrorMessage = "Enter Name for Flock Type!")]
        public string Name { get; set; }

     
        public string Description { get; set; }

    }

    public class UpdateFlockTypeVM : AddFlockTypeVM
    {
    }
}
