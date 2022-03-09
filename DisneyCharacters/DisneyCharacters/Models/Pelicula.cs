using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DisneyCharacters.Models
{
    public class Pelicula
    {
        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public DateTime Fecha { get; set; }
        public int Calificacion { get; set; }
        public virtual Genero Genero { get; set; }

        public virtual List<PersonajePelicula> PersonajePeliculas { get; set; }



    }
}
