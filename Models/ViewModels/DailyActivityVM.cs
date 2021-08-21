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
        public int Id { get; set; }


        [Required(ErrorMessage = "This Field is Required!!")]
        public double FeedConsumedPerKg { get; set; }


        [Required(ErrorMessage = "Input Mortality!!")]
        public int Mortality { get; set; }


        [Required(ErrorMessage = "You must fill out this field!!")]
        public int NoOfMedUsed { get; set; }


        [Required(ErrorMessage = "Select Flock!!")]
        public IEnumerable<SelectListItem> FlockList { get; set; }


        [Required(ErrorMessage = "Select Flock!!")]
        public int FlockId { get; set; }


        [Required(ErrorMessage = "Select Feed Allocated!!")]
        public IEnumerable<SelectListItem> FeedAllocationList { get; set; }


        [Required(ErrorMessage = "Select Feed Allocated!!")]
        public int StoreAllocationFeedId { get; set; }


        [Required(ErrorMessage = "Select Medication Allocated!!")]
        public IEnumerable<SelectListItem> MedAllocationList { get; set; }


        [Required(ErrorMessage = "Select Medication Allocated!!")]
        public int StoreAllocationMedId { get; set; }
    }

    public class UpdateDailyActivityVM : AddDailyActivityVM
    {
    }

    public class ListDailyActivityVM
    {
        public int Id { get; set; }
    }
}
