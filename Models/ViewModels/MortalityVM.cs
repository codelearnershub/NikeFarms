using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Models.ViewModels
{
    public class MortalityVM
    {
        
    }

    public class AddMortalityVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Input No. of Deaths")]
        public int NoOfDeaths { get; set; }

        public IEnumerable<SelectListItem> StockList { get; set; }

        public int? StockId { get; set; }

        public IEnumerable<SelectListItem> FlockList { get; set; }

        public int? FlockId { get; set; }
    }

    public class UpdateMortalityVM : AddMortalityVM
    {

    }

    public class ListMortalityVM
    {
        public int Id { get; set; }

        public int NoOfDeaths { get; set; }

        public string StockDescription { get; set; }

        public string FlockDescription { get; set; }

        public string CreatedBy { get; set; }

        public string CreatedAt { get; set; }
    }
}
