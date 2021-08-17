using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Models.ViewModels
{
    public class ExpensesVM
    {
    }

    public class AddExpensesVM
    {

        [Required(ErrorMessage = "Description is Required!")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Input Amout Spent!")]
        public int Price { get; set; }

    }

    public class UpdateExpensesVM : AddExpensesVM
    {
       
    }
}
