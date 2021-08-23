using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Models.ViewModels
{
    public class SalesVM
    {
    }

    public class AddSalesVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is Required!")]
        public string Item { get; set; }


        [Required(ErrorMessage = "Choose Customer!")]
        public IEnumerable<SelectListItem> CustomerList { get; set; }

        [Required(ErrorMessage = "Choose Customer!")]
        public int CustomerId { get; set; }
    }

    public class UpdateSalesVM : AddSalesVM
    {
    }

    public class ListSalesVM
    {
        public int Id { get; set; }

        public string Item { get; set; }

        public decimal? TotalPrice { get; set; }

        public string CustomerFullName { get; set; }

        public string CreatedBy { get; set; }

    }
}
