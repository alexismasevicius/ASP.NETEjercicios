using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DisneyCharacters.Models;


namespace DisneyCharacters.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController
    {
        private readonly RepositoryContext ctx;

        public LoginController(RepositoryContext ctx)
        {
            this.ctx = ctx;
        }
    }
}
