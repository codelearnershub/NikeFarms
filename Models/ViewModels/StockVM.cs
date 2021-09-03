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
        public int Id { get; set; }

        

        [Required(ErrorMessage = "Enter No. of Item Stocked!")]
        public double NoOfItem { get; set; }


        [Required(ErrorMessage = "Select Stock Item Type!")]
        public string ItemType { get; set; }


        public decimal? PricePerCrate { get; set; }


        public decimal? PricePerKg { get; set; }


        [Required(ErrorMessage = "Choose Flock!")]
        public IEnumerable<SelectListItem> FlockList { get; set; }


        [Required(ErrorMessage = "Choose Flock!")]
        public int FlockId { get; set; }
    }

    public class UpdateStockVM : AddStockVM
    {
    }

    public class ListStockVM
    {
        public int Id { get; set; }

        public string Item { get; set; }

        public double NoOfItem { get; set; }

        public double AvailableItem { get; set; }

        public decimal? PricePerCrate { get; set; }

        public decimal? PricePerKg { get; set; }

        public decimal EstimatedPricePerKg { get; set; }

        public string FlockBatchNo { get; set; }

        public string CreatedBy { get; set; }

        public string CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
