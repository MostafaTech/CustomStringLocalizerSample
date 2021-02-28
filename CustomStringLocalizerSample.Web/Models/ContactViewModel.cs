using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomStringLocalizerSample.Web.Models
{
    public class ContactViewModel
    {

        [Display(Name = "Name")]
        [Required(ErrorMessage = "error-required")]
        public string Name { get; set; }

        [Display(Name = "Message")]
        [Required(ErrorMessage = "error-required")]
        public string Message { get; set; }

    }
}
