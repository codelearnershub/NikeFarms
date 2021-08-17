using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Models.ViewModels
{
    public class DailyActivityVM
    {
    }

    public class AddDailyActivityVM
    {
        [Required(ErrorMessage = "This Field is Required!!")]
        public double FeedConsumedPerKg { get; set; }


        [Required(ErrorMessage = "Input Mortality!!")]
        public int Mortality { get; set; }


        [Required(ErrorMessage = "You must fill out this field!!")]
        public int NoOfMedUsed { get; set; }


        [Required(ErrorMessage = "Select Flock!!")]
        public IEnumerable<SelectListItem> Flock { get; set; }


        [Required(ErrorMessage = "Select Flock!!")]
        public int FlockId { get; set; }


        [Required(ErrorMessage = "Select Feed Allocated!!")]
        public IEnumerable<SelectListItem> FeedAllocation { get; set; }


        [Required(ErrorMessage = "Select Feed Allocated!!")]
        public int StoreAllocationFeedId { get; set; }


        [Required(ErrorMessage = "Select Medication Allocated!!")]
        public IEnumerable<SelectListItem> MedAllocation { get; set; }


        [Required(ErrorMessage = "Select Medication Allocated!!")]
        public int StoreAllocationMedId { get; set; }
    }

    public class UpdateDailyActivityVM : AddDailyActivityVM
    {
    }
}
