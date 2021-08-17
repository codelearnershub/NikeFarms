using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Models.ViewModels
{
    public class FlockVM
    {
    }

    public class AddFlockVM
    {
        [Required(ErrorMessage = "Choose Flock Type!")]
        public IEnumerable<SelectListItem> FlockType { get; set; }


        [Required(ErrorMessage = "Choose Flock Type!")]
        public int FlockTypeId { get; set; }

        [Required(ErrorMessage = "Enter Total Number of Flock!")]
        public int TotalNo { get; set; }


        [Required(ErrorMessage = "Enter Flock Age!")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Enter Average Weight of Flock!")]
        public double AverageWeight { get; set; }
    }

    public class UpdateFlockVM : AddFlockVM
    {

    }
}
