using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Models.ViewModels
{
    public class MortalityVM
    {
        [Required(ErrorMessage = "Input No. of Deaths")]
        public int NoOfDeaths { get; set; }

        public int? stockId { get; set; }

        public int? FlockId { get; set; }
    }
}
