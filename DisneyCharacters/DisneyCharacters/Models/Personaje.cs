using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DisneyCharacters.Models
{
    /// <summary>
    /// Personaje de Disney
    /// </summary>
    public class Personaje
    {
        /// <summary>
        /// Id del personaje
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Nombre del personaje
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Imagen del personaje
        /// </summary>
        public string Imagen { get; set; }

        /// <summary>
        /// Edad del personaje 
        /// </summary>
        public int Edad { get; set; }

        /// <summary>
        /// Peso del personaje
        /// </summary>
        public float Peso { get; set; }

        /// <summary>
        /// Historia del personaje 
        /// </summary>
        public string Historia { get; set; }

        /// <summary>
        /// Listado de personajes y peliculas relacionadas
        /// </summary>
        public virtual List<PersonajePelicula> PersonajePeliculas { get; set; }






    }
}
