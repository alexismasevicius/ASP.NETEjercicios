using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DisneyCharacters.Models
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonajePelicula>()
                .HasKey(x => new { x.PeliculaId, x.PersonajeId });
            modelBuilder.Entity<PersonajePelicula>()
                .HasOne(x => x.Personaje)
                .WithMany(x => x.PersonajePeliculas)
                .HasForeignKey(x => x.PersonajeId);
            modelBuilder.Entity<PersonajePelicula>()
                .HasOne(x => x.Pelicula)
                .WithMany(x => x.PersonajePeliculas)
                .HasForeignKey(x => x.PeliculaId);
            base.OnModelCreating(modelBuilder);


        }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<Personaje> Personajes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<Pelicula> Peliculas { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<Genero> Generos { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<PersonajePelicula> PersonajesPeliculas { get; set; }



    }
}
