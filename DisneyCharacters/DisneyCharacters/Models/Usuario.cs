using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DisneyCharacters.Models
{
    /// <summary>
    /// Usuario de la api
    /// </summary>
    public partial class Usuario
    {
        /// <summary>
        /// Id del usuario
        /// </summary>
        [Key]
        public int Id { get; set; }

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
        /// <summary>
        /// 
        /// </summary>
        public string Sal { get; set; }

    }
}
