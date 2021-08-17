using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Models.ViewModels
{
    public class StoreAllocationVM
    {
    }

    public class AddStoreAllocationVM
    {
        [Required(ErrorMessage = "Choose Store Item!")]
        public IEnumerable<SelectListItem> StoreItem { get; set; }


        [Required(ErrorMessage = "Choose Store Item!")]
        public int StoreItemId { get; set; }


        [Required(ErrorMessage = "No. of Item Allocated!")]
        public double NoOfItem { get; set; }

        public double? ItemPerKg { get; set; }

        [Required(ErrorMessage = "Select Manager Allocated To!")]
        public User Manager { get; set; }


        [Required(ErrorMessage = "Select Manager Allocated To!")]
        public int ManagerId { get; set; }
    }

    public class UpdateStoreAllocationVM : AddStoreAllocationVM
    {
    }
}
