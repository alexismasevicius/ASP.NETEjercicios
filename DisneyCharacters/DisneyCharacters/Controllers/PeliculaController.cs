using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DisneyCharacters.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace DisneyCharacters.Controllers
{
    /// <summary>
    /// Controlador de Pelicula
    /// </summary>
    [ApiController]
    [Route("movies")]
    [Authorize]
    public class PeliculaController : ControllerBase
    {
        private readonly RepositoryContext ctx;

        /// <summary>
        /// ctor 
        /// </summary>
        /// <param name="ctx">contex</param>
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
            var peliculaConPersonajes = await ctx.Peliculas.Where(x => x.Id == id)
                .Include(p => p.PersonajePeliculas)
                .ThenInclude(p => p.Personaje)
                .ToListAsync();


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
        /// Realiza busquedas de peliculas en la BD de acuerdo a los parametros
        /// </summary>
        /// <param name="name">nombre a buscar</param>
        /// <param name="idGenero">ID del genero de pelicula a buscar</param>
        /// <param name="order">Orden segun FECHA de creacion. ASC para ascendiente, DESC para descendiente </param>
        /// <returns>OK y resultado si fue exitosa. Not found o Bad Request si no lo fue</returns>
        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> GetPeliculasBusqueda(string name, int? idGenero, string order)
        {
            List<Pelicula> query = new List<Pelicula>();
            

            if (!string.IsNullOrEmpty(name))
            {
                query = await ctx.Peliculas.Where(p => p.Nombre.Contains(name))
                    .Include(p => p.PersonajePeliculas)
                    .ThenInclude(p => p.Personaje)
                    .ToListAsync();
            }
            if (idGenero != null)
            {
                query = await ctx.Peliculas
                    .Include(p => p.Genero)
                    .ToListAsync();
            }
            if (order!=null)
            {
                order = order.ToUpper();
                if (order == "DESC")
                {
                    query = await ctx.Peliculas.OrderByDescending(query => query.Fecha).ToListAsync();
                }
                else if (order == "ASC")
                {
                    query = await ctx.Peliculas.OrderBy(query => query.Fecha).ToListAsync();

                }
                else
                {
                    return BadRequest();
                }
            }
            if (query.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(query);
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

        /// <summary>
        /// Verifica si la pelicula existe
        /// </summary>
        /// <param name="id">id de la pelicla</param>
        /// <returns>true/false</returns>
        private bool PeliculaExists(int id)
        {
            return ctx.Peliculas.Any(e => e.Id == id);
        }


    }
}
