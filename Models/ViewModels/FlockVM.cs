﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Models.ViewModels
{
    public class FlockVM
    {
    }

    public class AddFlockVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Choose Flock Type!")]
        public IEnumerable<SelectListItem> FlockTypeList { get; set; }


        [Required(ErrorMessage = "Choose Flock Type!")]
        public int FlockTypeId { get; set; }

        [Required(ErrorMessage = "Enter Total Number of Flock!")]
        public int TotalNo { get; set; }


        [Required(ErrorMessage = "Enter Flock Age!")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Enter Average Weight of Flock!")]
        public double AverageWeight { get; set; }
    }

    public class UpdateFlockVM : AddFlockVM
    {

    }

    public class ListFlockVM
    {
        public int Id { get; set; }

        public string CreatedBy { get; set; }

        public string BatchNo { get; set; }

        public int TotalAvailable { get; set; }

        public string FlockType { get; set; }

        public int currentAge { get; set; }

        public double CurrentAverageWeight { get; set; }

    }
}
