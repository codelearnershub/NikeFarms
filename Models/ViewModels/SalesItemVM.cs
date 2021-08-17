using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Models.ViewModels
{
    public class SalesItemVM
    {
    }

    public class AddSalesItemVM
    {
        [Required(ErrorMessage = "Enter Item Sold!")]
        public string Item { get; set; }


        [Required(ErrorMessage = "Input No. of Item Sold!")]
        public int NoOfItem { get; set; }


        [Required(ErrorMessage = "Select Stock!")]
        public IEnumerable<SelectListItem> Stock { get; set; }


        [Required(ErrorMessage = "Select Stock!")]
        public int StockId { get; set; }


        [Required(ErrorMessage = "Choose Sales!")]
        public IEnumerable<SelectListItem> Sales { get; set; }


        [Required(ErrorMessage = "Choose Sales!")]
        public int SalesId { get; set; }


        [Required(ErrorMessage = "Fill out this Field!")]
        public decimal PricePerItem { get; set; }
    }

    public class UpdateSalesItemVM : AddSalesItemVM
    {
    }
}
