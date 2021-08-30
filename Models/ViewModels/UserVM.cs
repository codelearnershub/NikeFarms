 using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Models.ViewModels
{
    public class UserVM
    {
    }

    public class LoginVM
    {

        [Required(ErrorMessage = "Enter your Email!!")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Invalid E-Mail!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter your password!!")]
        [MinLength(4, ErrorMessage = "Password must be a minimum of 4 Characters")]
        [MaxLength(15, ErrorMessage = "Password must not be more than 15 Characters")]
        public string Password { get; set; }


    }

    public class RegisterVM : LoginVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Last Name is Required")]
        [MaxLength(20, ErrorMessage = "Last Name must not be more than 20 Characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "First Name is Required")]
        [MaxLength(20, ErrorMessage = "First Name must not be more than 20 Characters")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Confirm Password!!")]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage = "Enter Phone Number!!")]
        [MaxLength(11, ErrorMessage = "Invalid Phone Number!")]
        [MinLength(11, ErrorMessage = "Invalid Phone Number!")]
        public string PhoneNo { get; set; }

        [Required(ErrorMessage = "Enter Resident Address!!")]
        public string Address { get; set; }


        [Required(ErrorMessage = "Choose Role!")]
        public int RoleId { get; set; }


        [Required(ErrorMessage = "Choose Role!")]
        public IEnumerable<SelectListItem> RoleList { get; set; }


        [Required(ErrorMessage = "Select Gender!")]
        public string Gender { get; set; }


    }

    public class UpdateUserVM : RegisterVM
    {
      
    }

    public class ListUserVM
    {
        public int UserId { get; set; }
        
        public string FullName { get; set; }

        public string Email { get; set; }

        public string PhoneNo { get; set; }

        public string RoleName { get; set; }

        public string CreatedBy { get; set; }

        public string Gender { get; set; }

    }
}
