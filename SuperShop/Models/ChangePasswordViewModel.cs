﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Models
{
    public class ChangePasswordViewModel
    {
        [Required]
        [Display(Name="Current passord")]
        public string OldPassword { get; set; }

        [Required]
        [Display(Name = "New passord")]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword")]
        public string Confirm { get; set; }
    }
}