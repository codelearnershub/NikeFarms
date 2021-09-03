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
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter Item Sold!")]
        public string Item { get; set; }


        [Required(ErrorMessage = "Input No. of Item Sold!")]
        public int NoOfItem { get; set; }


        [Required(ErrorMessage = "Select Stock!")]
        public IEnumerable<SelectListItem> StockList { get; set; }


        [Required(ErrorMessage = "Select Stock!")]
        public int StockId { get; set; }


        public int SalesId { get; set; }


    }

    public class UpdateSalesItemVM : AddSalesItemVM
    {
    }

    public class ListSalesItemVM
    {
        public int Id { get; set; }

        public string Item { get; set; }

        public int NoOfItem { get; set; }

        public string ItemType { get; set; }

        public double CurrentWeight { get; set; }
        public decimal PricePerItem { get; set; }

        public decimal TotalPrice { get; set; }

        public string CreatedBy { get; set; }

    }
}
