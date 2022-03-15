using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisneyCharacters.Models
{
    /// <summary>
    /// Vista Simple de Pelicula
    /// </summary>
    public class PeliculaVistaSimple
    {
        /// <summary>
        /// Id de la pelicula
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Titulo de la pelicula
        /// </summary>
        public string Titulo { get; set; }
        /// <summary>
        /// Imagen de la pelicula
        /// </summary>
        public string Imagen { get; set; }

        /// <summary>
        /// Fecha de la pelicula
        /// </summary>
        public DateTime Fecha { get; set; }

    }
}
