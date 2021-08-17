using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Models.ViewModels
{
    public class StockVM
    {
    }

    public class AddStockVM
    {
        [Required(ErrorMessage = "Input Stock Item!")]
        public string Item { get; set; }


        [Required(ErrorMessage = "Enter No. of Item Stocked!")]
        public double NoOfItem { get; set; }
       

        public decimal? PricePerCrate { get; set; }

        public decimal? PricePerKg { get; set; }


        [Required(ErrorMessage = "Choose Flock!")]
        public IEnumerable<SelectListItem> Flock { get; set; }


        [Required(ErrorMessage = "Choose Flock!")]
        public int FlockId { get; set; }
    }

    public class UpdateStockVM : AddStockVM
    {
    }
}
