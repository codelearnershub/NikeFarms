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
        public double NoOfMedUsed { get; set; }

        [Required(ErrorMessage = "You must fill out this field!!")]
        public double NoOfFeedUsed { get; set; }

        [Required(ErrorMessage = "Select Flock!")]
        public int FlockId { get; set; }


        [Required(ErrorMessage = "Select Flock!")]
        public IEnumerable<SelectListItem> FlockList { get; set; }


        [Required(ErrorMessage = "Select Feed Allocated!!")]
        public IEnumerable<SelectListItem> FeedAllocationList { get; set; }


        [Required(ErrorMessage = "Select Feed Allocated!!")]
        public int StoreAllocationFeedId { get; set; }


       // [Required(ErrorMessage = "Select Medication Allocated!!")]
        public IEnumerable<SelectListItem> MedAllocationList { get; set; }


        //[Required(ErrorMessage = "Select Medication Allocated!!")]
        public int? StoreAllocationMedId { get; set; }
    }

    public class UpdateDailyActivityVM : AddDailyActivityVM
    {
    }

    public class ListDailyActivityVM
    {
        public int Id { get; set; }

        public int Mortality { get; set; }

        public string MedUsed { get; set; }

        public double NoOfMedUsed { get; set; }

        public string FeedUsed { get; set; }

        public double NoOfFeedUsed { get; set; }

        public string FlockDescription { get; set; }

        public string CreatedBy { get; set; }

        public string CreatedAt { get; set; }
    }
}
