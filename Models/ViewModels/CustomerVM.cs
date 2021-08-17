using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Models.ViewModels
{
    public class CustomerVM
    {
    }

    public class AddCustomerVM
    {
        [Required(ErrorMessage = "Customer Last Name is Required")]
        [MaxLength(20, ErrorMessage = "Last Name must not be more than 20 Characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Customer First Name is Required")]
        [MaxLength(20, ErrorMessage = "First Name must not be more than 20 Characters")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Enter your Email!!")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Invalid E-Mail!")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Enter Customer Phone Number!!")]
        [MaxLength(11, ErrorMessage = "Invalid Phone Number!")]
        [MinLength(11, ErrorMessage = "Invalid Phone Number!")]
        public string PhoneNo { get; set; }

        [Required(ErrorMessage = "Enter Customer Address!!")]
        public string Address { get; set; }

    }

    public class UpdateCustomerVM : AddCustomerVM
    {

    }
}
