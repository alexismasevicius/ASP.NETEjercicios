﻿using System;
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
    [Route("characters")]
    [ApiController]
    public class PersonajesController : ControllerBase
    {
        private readonly RepositoryContext ctx;

        public PersonajesController(RepositoryContext context)
        {
            ctx = context;
        }


        /// <summary>
        /// obtiene la lista de los personajes desde la base de datos en una forma simplificada
        /// </summary>
        /// <returns>Una lista simplificada de los personajes</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonajeVistaSimple>>> GetPersonajes()
        {
            List<Personaje> miLista = await ctx.Personajes.ToListAsync();
            List<PersonajeVistaSimple> listaPersonajeSimple = new List<PersonajeVistaSimple>();
            foreach (Personaje item in miLista)
            {
                PersonajeVistaSimple miPersonajeSimple = new PersonajeVistaSimple();
                miPersonajeSimple.Imagen = item.Imagen;
                miPersonajeSimple.Nombre = item.Nombre;
                if(miPersonajeSimple != null)
                {
                    listaPersonajeSimple.Add(miPersonajeSimple);
                }
            }
            return listaPersonajeSimple;
        }

        /// <summary>
        /// obtiene el detalle de un personaje junto con sus peliculas asociadas
        /// </summary>
        /// <param name="id">id del personaje</param>
        /// <returns>El detalle del personaje</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonaje(int id)
        {

            var query = await ctx.Personajes.Where(p => p.Id == id).Include(p => p.PersonajePeliculas).ThenInclude(p => p.Pelicula).ToListAsync();

            ////CREAR OTRA CLASE PARA RESUMIR LOS DATOS DEVUELTOS ( IGNORAR CLASE PersonajePelicula)

            if (query == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(query);
            }
        }
        
        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> GetPersonajesNombre(string name, int? age, int? movies)
        {

            if (!string.IsNullOrEmpty(name))
            {
                var query = await ctx.Personajes.Where(p => p.Nombre.Contains(name)).Include(p => p.PersonajePeliculas).ThenInclude(p => p.Pelicula).ToListAsync();
                if (query == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(query);
                }
            }
            if(age!=null)
            {
                var query = await ctx.Personajes.Where(p => p.Edad == age).Include(p => p.PersonajePeliculas).ThenInclude(p => p.Pelicula).ToListAsync();
                if (query == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(query);
                }
            }
            if(movies!=null)
            {
                List<PersonajePelicula> miLista = await ctx.PersonajesPeliculas.Where(x => x.PeliculaId == movies).ToListAsync();
                var queryAux = await ctx.Personajes.ToListAsync();

                List<Personaje> query = new List<Personaje>();

                foreach (var item in miLista)
                {
                    query.Add(queryAux.Find(x => x.Id == item.PersonajeId));
                }

                return Ok(query);
            }

            return NotFound();

        }



        /// <summary>
        /// Modifica un personaje en la BD
        /// </summary>
        /// <param name="id">id del personaje a modificar</param>
        /// <param name="personaje">el personaje modificado</param>
        /// <returns>400 si el id a modificar no coincide con el personaje, 404 si el id no se encuentra en la BD</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonaje(int id, Personaje personaje)
        {
            if (id != personaje.Id)
            {
                return BadRequest();
            }

            ctx.Entry(personaje).State = EntityState.Modified;

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

        /// <summary>
        /// Crea un personaje en la base de datos
        /// </summary>
        /// <param name="personaje">el objeto personaje a crear</param>
        /// <returns>200 si se creo, 400 si el modelo enviado no es valido</returns>
        [HttpPost]
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

        /// <summary>
        /// Elimina un personaje de la base de datos
        /// </summary>
        /// <param name="id">id del personaje</param>
        /// <returns>el personaje eliminado</returns>
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


        /// <summary>
        /// Verifica la existencia del personaje
        /// </summary>
        /// <param name="id">id del personaje</param>
        /// <returns>true o false</returns>
        private bool PersonajeExists(int id)
        {
            return ctx.Personajes.Any(e => e.Id == id);
        }
    }
}
