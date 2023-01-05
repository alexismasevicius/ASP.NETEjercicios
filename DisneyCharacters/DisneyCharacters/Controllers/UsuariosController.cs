using DisneyCharacters.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DisneyCharacters.Helper;

namespace DisneyCharacters.Controllers
{
    /// <summary>
    /// Controlador de usuarios
    /// </summary>
    [Route("auth/register")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        RepositoryContext ctx;
        /// <summary>
        /// Ctor 
        /// </summary>
        /// <param name="ctx">contexto</param>
        public UsuariosController(RepositoryContext ctx)
        {
            this.ctx = ctx;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<UsuarioVista> Usuarios = await ctx.Usuarios.Select(x => new UsuarioVista()
            {
                IdUsuario = x.Id,
                Nombre = x.Nombre
            }).ToListAsync();
            return Ok(Usuarios);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            UsuarioVista Usuarios = await ctx.Usuarios.Where(x => x.Id == id).Select(x => new UsuarioVista()
            {
                IdUsuario = x.Id,
                Nombre = x.Nombre
            }).SingleOrDefaultAsync();
            return Ok(Usuarios);
        }

        /// <summary>
        /// Registrar un usuario. Envie solo Nombre de usuario y contraseña.
        /// </summary>
        /// <param name="usuario">el usuario a registrar</param>
        /// <returns>Ok y el nombre del usuario</returns>
        [HttpPost]
        public async Task<IActionResult> Post (Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //Si el usuario ya existe
            if(await ctx.Usuarios.Where(x => x.Nombre == usuario.Nombre).AnyAsync())
            {
                return BadRequest("El usuario ya existe");
            }

            HashedPassword password = HashHelper.Hash(usuario.Clave);
            usuario.Clave = password.Password;
            usuario.Sal = password.Salt;
            ctx.Usuarios.Add(usuario);
            await ctx.SaveChangesAsync();

            return Ok(usuario.Nombre);
        }



    }
}
