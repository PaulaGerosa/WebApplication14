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
    public class ProductoController : ApiController
    {
        //Crear Producto
        [HttpPost]
        public void Crear ([FromBody] Producto producto)
        {
            ProductoHandler.CrearProducto(producto);
        }

        //Modificar Producto
        [HttpPut]

        public void Modificar ([FromBody] Producto producto)
        {
            ProductoHandler.ModificarProducto(producto);
        }

        //Eliminar Producto
        [HttpDelete]
        public void Eliminar ([FromBody] int IdProducto)
        {
            ProductoHandler.EliminarProducto(IdProducto);
        }
    }
}