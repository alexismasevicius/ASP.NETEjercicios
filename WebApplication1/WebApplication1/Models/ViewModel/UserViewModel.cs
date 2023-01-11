using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.ViewModel
{
    public class UserViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Correo Electronico")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name = "Cofirma conntraseña")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Edad { get; set; }



    }
}