using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisneyCharacters.Models
{
    /// <summary>
    /// Vista Simple del personaje
    /// </summary>
    public class PersonajeVistaSimple
    {
        /// <summary>
        /// Id del personaje
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del personaje
        /// </summary>
        public string Nombre {get; set;}
        /// <summary>
        /// Imagen del personaje
        /// </summary>
        public string Imagen { get; set; }

    }
}
