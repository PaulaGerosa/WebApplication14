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
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ApiController
    {
        [HttpGet(Name = "TraerUsuario")]
        public Usuario TraerUsuario_conNombreUsuario(string nombreUsuario)
        {
            return UsuarioHandler.TraerUsuario_conNombreUsuario(nombreUsuario);
        }

        [HttpPut(Name = "ModificarUsuario")]
        public bool ModificarUsuario([FromBody] PutUsuario usuario)
        {
            try
            {
                Usuario usuarioExistente = new Usuario();
                usuarioExistente = UsuarioHandler.TraerUsuario_conId(usuario.Id);
                if (usuarioExistente.Id <= 0)
                {
                    return false; // El Id de Usuario no existe en BD, no hay nada para modificar, debe crearse primero.
                }
                else
                {
                    return UsuarioHandler.ModificarUsuario(
                    new Usuario
                    {
                        Id = usuario.Id,
                        Nombre = usuario.Nombre,
                        Apellido = usuario.Apellido,
                        NombreUsuario = usuario.NombreUsuario,
                        Contraseña = usuario.Contraseña,
                        Mail = usuario.Mail
                    }
                    );
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
