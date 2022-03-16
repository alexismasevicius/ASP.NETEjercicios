using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DisneyCharacters.Models
{
    /// <summary>
    /// Relacion muchos a muchos ENtre personajes y pelicula
    /// </summary>
    public class PersonajePelicula
    {

        //Esta clase tiene una clave compuesta creada con fluent api (x.PeliculaId, x.PersonajeId)

        /// <summary>
        /// Id del personaje
        /// </summary>
        public int PersonajeId { get; set; }
        /// <summary>
        /// Id de la pelicula
        /// </summary>
        public int PeliculaId { get; set; }
        /// <summary>
        /// objeto Pelicula
        /// </summary>
        public Pelicula Pelicula { get; set; }

        /// <summary>
        /// objeto personaje
        /// </summary>
        public Personaje Personaje { get; set; }



    }
}
