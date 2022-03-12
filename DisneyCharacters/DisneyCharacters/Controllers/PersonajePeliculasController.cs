using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DisneyCharacters.Models;

namespace DisneyCharacters.Controllers
{
    //ID es clave compuesta : PeliculaId + PersonajeId

    [Route("api/[controller]")]
    [ApiController]
    public class PersonajePeliculasController : ControllerBase
    {
        private readonly RepositoryContext _context;

        public PersonajePeliculasController(RepositoryContext context)
        {
            _context = context;
        }

        // GET: api/PersonajePeliculas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonajePelicula>>> GetPersonajesPeliculas()
        {
            return await _context.PersonajesPeliculas.ToListAsync();
        }

        // GET: api/PersonajePeliculas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonajePelicula>> GetPersonajePelicula(int id)
        {
            var personajePelicula = await _context.PersonajesPeliculas.FindAsync(id);

            if (personajePelicula == null)
            {
                return NotFound();
            }

            return personajePelicula;
        }

        // PUT: api/PersonajePeliculas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonajePelicula(int id, PersonajePelicula personajePelicula)
        {
            if (id != personajePelicula.PeliculaId)
            {
                return BadRequest();
            }

            _context.Entry(personajePelicula).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonajePeliculaExists(id))
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

        // POST: api/PersonajePeliculas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PersonajePelicula>> PostPersonajePelicula(PersonajePelicula personajePelicula)
        {
            _context.PersonajesPeliculas.Add(personajePelicula);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PersonajePeliculaExists(personajePelicula.PeliculaId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPersonajePelicula", new { id = personajePelicula.PeliculaId }, personajePelicula);
        }

        // DELETE: api/PersonajePeliculas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PersonajePelicula>> DeletePersonajePelicula(int id)
        {
            var personajePelicula = await _context.PersonajesPeliculas.FindAsync(id);
            if (personajePelicula == null)
            {
                return NotFound();
            }

            _context.PersonajesPeliculas.Remove(personajePelicula);
            await _context.SaveChangesAsync();

            return personajePelicula;
        }

        private bool PersonajePeliculaExists(int id)
        {
            return _context.PersonajesPeliculas.Any(e => e.PeliculaId == id);
        }
    }
}
