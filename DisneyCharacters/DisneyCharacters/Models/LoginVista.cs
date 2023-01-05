using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DisneyCharacters.Models
{
    /// <summary>
    /// Vista de Login
    /// </summary>
    public class LoginVista: Controller
    {

        /// <summary>
        /// Nombre del usuario
        /// </summary>
        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        public string Nombre { get; set; }

        /// <summary>
        /// Contraseña del usuario
        /// </summary>
        [Required(ErrorMessage = "La clave de usuario es obligatoria")]
        public string Clave { get; set; }


    }
}
