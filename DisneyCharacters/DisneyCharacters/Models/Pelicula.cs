using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DisneyCharacters.Models
{
    /// <summary>
    /// Pelicula de disney
    /// </summary>
    public class Pelicula
    {
        /// <summary>
        /// Id de la pelicula
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Nombre de la pelicula
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Imagen de la pelicula
        /// </summary>
        public string Imagen { get; set; }
        /// <summary>
        /// Fecha de estreno de la pelicula
        /// </summary>
        public DateTime Fecha { get; set; }
        /// <summary>
        /// Calificacion de la pelicula
        /// </summary>
        public int Calificacion { get; set; }

        /// <summary>
        /// Genero de la pelicula
        /// </summary>
        public virtual Genero Genero { get; set; }


        /// <summary>
        /// Lista compuesta de personajes y peliculas relacionadas
        /// </summary>
        public virtual List<PersonajePelicula> PersonajePeliculas { get; set; }



    }
}
