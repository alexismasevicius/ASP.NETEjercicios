﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DisneyCharacters.Models
{
    /// <summary>
    /// Genero de las peliculas
    /// </summary>
    public class Genero
    {
        /// <summary>
        /// Id del genero
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// NOmbre del genero
        /// </summary>
        public string Nombre { get; set; }
        /// <summary>
        /// Imagen del genero
        /// </summary>
        public string Imagen { get; set; }

        /// <summary>
        /// Peliculas relacionadas al genero
        /// </summary>
        public virtual ICollection<Pelicula> Peliculas { get; set; }


    }
}
