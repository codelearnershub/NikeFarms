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
    public class AddWeeklyReportVM

    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Input Average Weight of Birds!")]
        public double AverageWeight { get; set; }


        [Required(ErrorMessage = "Select Flock!")]
        public IEnumerable<SelectListItem> FlockList { get; set; }


        [Required(ErrorMessage = "Select Flock!")]
        public int FlockId { get; set; }
    }

    public class UpdateWeeklyReportVM : AddWeeklyReportVM
    {
        public string SelectedFlock { get; set; }
    }

    public class ListWeeklyReportVM
    {
        public int Id { get; set; }

        public string CreatedBy { get; set; }

       public double AverageWeight { get; set; }

        public string Flock { get; set; }

        public string CreatedAt { get; set; }
    }

}
