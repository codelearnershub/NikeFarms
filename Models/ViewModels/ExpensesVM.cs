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
        public int Id { get; set; }

        [Required(ErrorMessage = "Description is Required!")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Input Amout Spent!")]
        public decimal Price { get; set; }

    }

    public class UpdateExpensesVM : AddExpensesVM
    {
       
    }

    public class ListExpensesVM
    {
        public int Id { get; set; }

        public string CreatedBy { get; set; }

        
        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
