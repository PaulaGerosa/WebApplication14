using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Linq;
using WebApplication14.Models;

namespace WebApplication14.Controllers
{
    public class ProductoController : ApiController
    {
        [HttpPost(Name = "CrearProducto")]
        public bool CrearProducto([FromBody] PostProducto producto)
        {
            try
            {
                return ProductoHandler.CrearProducto(
                    new Producto
                    {
                        Descripciones = producto.Descripciones,
                        Costo = producto.Costo,
                        PrecioVenta = producto.PrecioVenta,
                        Stock = producto.Stock,
                        IdUsuario = producto.IdUsuario
                    }
                );
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPut(Name = "ModificarProducto")]

        public bool ModificarProducto([FromBody] PutProducto producto)
        {
            try
            {
                return ProductoHandler.ModificarProducto(
                    new Producto
                    {
                        Id = producto.Id,
                        Descripciones = producto.Descripciones,
                        Costo = producto.Costo,
                        PrecioVenta = producto.PrecioVenta,
                        Stock = producto.Stock,
                        IdUsuario = producto.IdUsuario
                    }
                );
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool EliminarProducto([FromBody] long idProducto)
        {
            try
            {
                ProductoVendidoHandler.EliminarProductoVendido(idProducto); // Puede que se quiera eliminar un producto que no fue vendido aún, por ello no se válida.
                return ProductoHandler.EliminarProducto(idProducto);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}