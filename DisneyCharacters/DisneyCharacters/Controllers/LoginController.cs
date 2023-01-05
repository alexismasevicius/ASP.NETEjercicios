using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DisneyCharacters.Models;
using Microsoft.EntityFrameworkCore;
using DisneyCharacters.Helper;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace DisneyCharacters.Controllers
{
    /// <summary>
    /// Controlador de login
    /// </summary>
    [ApiController]
    [Route("auth/login")]
    [Authorize]
    public class LoginController : Controller
    {
        private readonly RepositoryContext ctx;
        private readonly IConfiguration config;

        public LoginController(RepositoryContext ctx, IConfiguration _config)
        {
            this.ctx = ctx;
            this.config = _config;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post(LoginVista login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Usuario usuario = await ctx.Usuarios.Where(x => x.Nombre == login.Nombre).FirstOrDefaultAsync();
            if(usuario == null)
            {
                return NotFound("usuario no encontrado");
            }
            if (HashHelper.CheckHash(login.Clave, usuario.Clave, usuario.Sal))
            {

                var secretKey = config.GetValue<string>("SecretKey"); //Lee la clave secreta
                var key = Encoding.ASCII.GetBytes(secretKey); //

                var claims = new ClaimsIdentity(); //Crea un nuevo claim para poder iniciar sesion
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, login.Nombre)); //Crea Claim del tipo nameIdentifier para poder identificar el usuario

                var tokenDescriptor = new SecurityTokenDescriptor //Descriptor de token donde agregamos los claims
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddHours(4), //Token Caduca despues de 4 horas
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    //Datos de inicio de sesion encriptados
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var createdToken = tokenHandler.CreateToken(tokenDescriptor);            //TOKEN CREADO
    
                string bearer_token = tokenHandler.WriteToken(createdToken);
                return Ok(bearer_token);
            }
            else
            {
                return Forbid();
            }


        }

        [HttpGet]
        public IActionResult Get()
        {
            var r = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier);
            return Ok(r == null ? "" : r.Value);
        }

    }
}
