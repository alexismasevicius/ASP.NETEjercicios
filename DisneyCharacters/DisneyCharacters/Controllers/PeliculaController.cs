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
    [ApiController]
    [Route("movies")]
    public class PeliculaController : ControllerBase
    {
        private readonly RepositoryContext ctx;

        public PeliculaController(RepositoryContext ctx)
        {
            this.ctx = ctx;
        }

        [HttpGet]
        public async Task<IEnumerable<Pelicula>> Get()
        {
            return await ctx.Peliculas.Include(s => s.Nombre).Include(s => s.Fecha).Include(s => s.Imagen).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var pelicula = await ctx.Peliculas.FindAsync(id);

            var peliculaConPersonajes =
                ctx.Peliculas.Where(x => x.Id == id);


            if (pelicula == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(pelicula);
            }

        }

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


        // DELETE: api/Peliculas/5
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

        private bool PeliculaExists(int id)
        {
            return ctx.Peliculas.Any(e => e.Id == id);
        }


    }
}
