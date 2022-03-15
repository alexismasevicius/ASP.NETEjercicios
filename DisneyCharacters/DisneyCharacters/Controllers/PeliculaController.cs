using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DisneyCharacters.Models;
using Microsoft.EntityFrameworkCore;

namespace DisneyCharacters.Controllers
{
    /// <summary>
    /// Controlador de Pelicula
    /// </summary>
    [ApiController]
    [Route("movies")]
    public class PeliculaController : ControllerBase
    {
        private readonly RepositoryContext ctx;


        public PeliculaController(RepositoryContext ctx)
        {
            this.ctx = ctx;
        }

        /// <summary>
        /// obtiene la lista de los peliculas desde la base de datos en una forma simplificada
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<PeliculaVistaSimple>> Get()
        {
            List<Pelicula> miLista = await ctx.Peliculas.ToListAsync();
            List<PeliculaVistaSimple> listaSimple = SimplificarLista(miLista);
            return listaSimple;

        }

        /// <summary>
        /// obtiene una pelicula con su detalle
        /// </summary>
        /// <param name="id">id de la pelicula</param>
        /// <returns>El detalle del personajke</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var peliculaConPersonajes = await ctx.Peliculas.Where(x => x.Id == id).Include(p => p.PersonajePeliculas).ThenInclude(p => p.Personaje).ToListAsync();


            if (peliculaConPersonajes == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(peliculaConPersonajes);
            }

        }

        /// <summary>
        /// Crea una nueva pelicula en la BD
        /// </summary>
        /// <param name="pelicula">pelicula a agregar</param>
        /// <returns>La pelicula si se creo correctamente</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Pelicula pelicula) //FromBody=Los datos vienen desde el cuerpo de la peticion
        {
            if (!ModelState.IsValid) //valida los Data Annotation del Modelo
            {
                return BadRequest(); //400
            }
            else
            {
                pelicula.Id = 0;
                ctx.Peliculas.Add(pelicula);
                await ctx.SaveChangesAsync(); //Guarda todos los cambios en la DB
                return Created($"api/peliculas/{pelicula.Id}", pelicula);
            }
            

        }

        /// <summary>
        /// Modifica una pelicula
        /// </summary>
        /// <param name="id">id de la pelicula a modificar</param>
        /// <param name="pelicula">Pelicula nueva</param>
        /// <returns>No content si OK</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPelicula(int id, Pelicula pelicula)
        {
            if (id != pelicula.Id)
            {
                return BadRequest();
            }

            ctx.Entry(pelicula).State = EntityState.Modified;

            try
            {
                await ctx.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PeliculaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Elimina una pelicula
        /// </summary>
        /// <param name="id">id de la pelicula</param>
        /// <returns>Si se elimino correctamente, la pelicula</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Pelicula>> DeletePelicula(int id)
        {
            var pelicula = await ctx.Peliculas.FindAsync(id);
            if (pelicula == null)
            {
                return NotFound();
            }

            ctx.Peliculas.Remove(pelicula);
            await ctx.SaveChangesAsync();

            return pelicula;
        }

        
        /// <summary>
        /// Simplifica la vista de una lista de peliculas
        /// </summary>
        /// <param name="miLista">Lista de peliculas</param>
        /// <returns>La lista simplificada</returns>
        private List<PeliculaVistaSimple> SimplificarLista(List<Pelicula> miLista)
        {
            List<PeliculaVistaSimple> listaSimple = new List<PeliculaVistaSimple>();
            foreach (Pelicula item in miLista)
            {
                PeliculaVistaSimple miPersonajeSimple = new PeliculaVistaSimple();
                miPersonajeSimple.Id = item.Id;
                miPersonajeSimple.Imagen = item.Imagen;
                miPersonajeSimple.Titulo = item.Nombre;
                if (miPersonajeSimple != null)
                {
                    listaSimple.Add(miPersonajeSimple);
                }
            }
            return listaSimple;

        }

        private bool PeliculaExists(int id)
        {
            return ctx.Peliculas.Any(e => e.Id == id);
        }


    }
}
