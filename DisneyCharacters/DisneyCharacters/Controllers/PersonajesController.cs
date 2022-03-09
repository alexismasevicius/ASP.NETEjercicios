using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DisneyCharacters.Models;
using System.Text.Json;

namespace DisneyCharacters.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonajesController : ControllerBase
    {
        private readonly RepositoryContext ctx;

        public PersonajesController(RepositoryContext context)
        {
            ctx = context;
        }

        // GET: api/Personajes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Personaje>>> GetPersonajes()
        {
            return await ctx.Personajes.Include(p => p.PersonajePeliculas).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonaje(int id)
        {
            var personaje = await ctx.Personajes.FindAsync(id); //Hallo la pelicula correspondiente al Id

            if (personaje == null)
            {
                return NotFound();
            }
            else
            {
                List<Pelicula> miLista = ListarPeliculasDePersonaje(id);
                return Ok(personaje);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Pelicula> ListarPeliculasDePersonaje(int id)
        {
            List<Pelicula> miLista = new List<Pelicula>();
            foreach (var item in ctx.PersonajesPeliculas)
            {
                if(item.PersonajeId == id)
                {
                    var pelicula = ctx.Peliculas.Find(item.PeliculaId);
                    if(pelicula != null)
                    {
                        miLista.Add(pelicula);
                    }
                }
            }
            return miLista;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonaje(int id, Pelicula pelicula)
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
                if (!PersonajeExists(id))
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



        public async Task<IActionResult> Post([FromBody] Personaje personaje) //FromBody=Los datos vienen desde el cuerpo de la peticion
        {
            if (!ModelState.IsValid) //valida los Data Annotation del Modelo
            {
                return BadRequest(); //400
            }
            else
            {
                personaje.Id = 0;
                ctx.Personajes.Add(personaje);
                await ctx.SaveChangesAsync(); //Guarda todos los cambios en la DB
                return Created($"api/peliculas/{personaje.Id}", personaje);
            }


        }

        // DELETE: api/Personajes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Personaje>> DeletePersonaje(int id)
        {
            var personaje = await ctx.Personajes.FindAsync(id);
            if (personaje == null)
            {
                return NotFound();
            }

            ctx.Personajes.Remove(personaje);
            await ctx.SaveChangesAsync();

            return personaje;
        }

        private bool PersonajeExists(int id)
        {
            return ctx.Personajes.Any(e => e.Id == id);
        }
    }
}
