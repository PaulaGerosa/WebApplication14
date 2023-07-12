using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Linq;
using WebApplication14.Models;
using WebApplication14.Repository;

namespace WebApplication14.Controllers
{
    public class UsuarioController : ApiController
    {
        [HttpPut]

        public void Modificar([FromBody] Usuario usuario)
        {
            UsuarioHandler.ModificarUsuario(usuario);
        }
    }
}
