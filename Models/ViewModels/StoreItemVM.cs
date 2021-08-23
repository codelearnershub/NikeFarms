using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Models.ViewModels
{
    public class StoreItemVM
    {
    }

    public class AddStoreItemVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You must Fill out this Field!")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "This Field is Required!")]
        public string ItemType { get; set; }


        [Required(ErrorMessage = "Input No. Of Item!")]
        public double NoOfItem { get; set; }

        public double? ItemPerKg { get; set; }

        [Required(ErrorMessage = "Input Total Price of Items Purchased!")]
        public decimal TotalPricePurchased { get; set; }
    }

    public class UpdateStoreItemVM : AddStoreItemVM
    {
    }

    public class ListStoreItemVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ItemType { get; set; }

        public double NoOfItem { get; set; }

        public double? ItemPerKg { get; set; }

        public double ItemRemaining { get; set; }

        public decimal TotalPricePurchased { get; set; }

        public string CreatedBy { get; set; }
    }
}
