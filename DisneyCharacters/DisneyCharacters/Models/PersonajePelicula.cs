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

        public int PersonajeId { get; set; }

        public int PeliculaId { get; set; }
        public Pelicula Pelicula { get; set; }
        public Personaje Personaje { get; set; }



    }
}
