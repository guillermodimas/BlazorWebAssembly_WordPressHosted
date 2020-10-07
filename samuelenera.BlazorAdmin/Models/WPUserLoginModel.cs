using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace samuelenera.BlazorAdmin.Models
{
    public class WPUserLoginModel
    {
        [Required(ErrorMessage = "Username or E-mail")]
        public string username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
