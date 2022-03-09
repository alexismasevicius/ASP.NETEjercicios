using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DisneyCharacters.Models
{
    public class Personaje
    {
        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public int Edad { get; set; }
        public float Peso { get; set; }
        public string Historia { get; set; }
        public virtual List<PersonajePelicula> PersonajePeliculas { get; set; }






    }
}
