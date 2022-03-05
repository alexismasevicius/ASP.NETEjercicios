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
            return await ctx.Peliculas.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var pelicula = await ctx.Peliculas.FindAsync(id);
            if (pelicula == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(pelicula);
            }

        }

        public async Task<IActionResult> Post(Pelicula pelicula)
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



    }
}
