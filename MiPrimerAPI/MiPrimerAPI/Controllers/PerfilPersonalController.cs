using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimerAPI.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class PerfilPersonalController : ControllerBase
    {
        [HttpGet("LeerPerfil/{id}")]
        public string Get(int id)
        {
            //codigo para leer de la base de datos
            return id switch
            {
                1 => "Alexis",
                2 => "Curso",
                _ => throw new NotSupportedException("El id no es valido")
            };
        }

       /* public string Post(string nombre, string apellido, string email)
        {

        }*/

        public string Post(PerfilPersonalDto perfilPersonal)
        {
            //guardar perfil en base de datos

            return perfilPersonal.Nombre;
        }

    }

    public class PerfilPersonalDto
    {
        public string Nombre { get; set; };
        public string Apellido { get; set; };
        public string EMail { get; set; };


    }



}
