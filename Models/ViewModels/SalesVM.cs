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
        [Required(ErrorMessage = "This field is Required!")]
        public string Item { get; set; }


        [Required(ErrorMessage = "Enter Customer Email Address!")]
        public string CustomerEmail { get; set; }

        
    }

    public class UpdateSalesVM : AddSalesVM
    {
    }
}
