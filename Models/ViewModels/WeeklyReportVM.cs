using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Models.ViewModels
{
    public class WeeklyReportVM
    {
    }

    public class AddWeeklyReport
    {
        [Required(ErrorMessage = "Input Average Weight of Birds!")]
        public double AverageWeight { get; set; }


        [Required(ErrorMessage = "Select Flock!")]
        public IEnumerable<SelectListItem> StoreItem { get; set; }


        [Required(ErrorMessage = "Select Flock!")]
        public int FlockId { get; set; }
    }

    public class UpdateWeeklyReport : AddWeeklyReport
    {
    }
}
