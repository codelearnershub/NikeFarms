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
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter Name for Flock Type!")]
        public string Name { get; set; }

     
        public string Description { get; set; }

    }

    public class UpdateFlockTypeVM : AddFlockTypeVM
    {
    }

    public class ListFlockTypeVM
    {
        public int Id { get; set; }

        public string CreatedBy { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

    }
}
