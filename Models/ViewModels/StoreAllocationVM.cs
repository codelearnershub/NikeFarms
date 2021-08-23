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
        public int Id { get; set; }

        [Required(ErrorMessage = "Choose Store Item!")]
        public IEnumerable<SelectListItem> StoreItemList { get; set; }


        [Required(ErrorMessage = "Choose Store Item!")]
        public int StoreItemId { get; set; }

        public string ItemType { get; set; }


        [Required(ErrorMessage = "No. of Item Allocated!")]
        public double NoOfItem { get; set; }

        public double? ItemPerKg { get; set; }

        [Required(ErrorMessage = "Select Manager Allocated To!")]
        public IEnumerable<SelectListItem> ManagerList { get; set; }


        [Required(ErrorMessage = "Select Manager Allocated To!")]
        public int ManagerId { get; set; }
    }

    public class UpdateStoreAllocationVM : AddStoreAllocationVM
    {
    }

    public class ListStoreAllocationVM
    {
        public int Id { get; set; }

        public string StoreItemName { get; set; }

        public double NoOfItem { get; set; }

        public double? ItemPerKg { get; set; }

        public double ItemRemaining { get; set; }

        public string AllocatedTo { get; set; }

        public string ItemType { get; set; }

        public string CreatedBy { get; set; }
    }
}
