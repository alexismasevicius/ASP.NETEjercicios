using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisneyCharacters.Models
{
    public class PersonajePelicula
    {
        public int PersonajeId { get; set; }
        public int PeliculaId { get; set; }
        public Pelicula Pelicula { get; set; }
        public Personaje Personaje { get; set; }



    }
}
