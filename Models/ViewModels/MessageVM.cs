using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Models.ViewModels
{
    public class MessageVM
    {
    }

    public class AddMessageVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Message Title!")]
        public string Title { get; set; }


        [Required(ErrorMessage = "Message Content!")]
        public string Content { get; set; }


        [Required(ErrorMessage = "Choose Recipient!")]
        public IEnumerable<SelectListItem> RecieverList { get; set; }


        [Required(ErrorMessage = "Choose Recipient!")]
        public int RecieverId { get; set; }

        public string Role { get; set; }
    }

    public class UpdateMessageVM : AddMessageVM
    {
    }

    public class ListMessageVM
    {
        public int Id { get; set; }
    
        public string Title { get; set; }

        public string Content { get; set; }

        public string CreatedBy { get; set; }

        public string RecievedBy { get; set; }

        public DateTime CreatedAt { get; set; }
    }

    public class SeeMore
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string CreatedBy { get; set; }

        public string RecievedBy { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
