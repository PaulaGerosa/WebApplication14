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
    public class VentaController : ApiController
    {

        [HttpGet(Name = "TraerVentas")]
        public List<ProductoVendido> TraerVentas()
        {
            return VentaHandler.TraerVentas();
        }


        [HttpPost(Name = "CargarVenta")]    
        public bool CargarVenta([FromBody] List<PostVenta> listaDeProductosVendidos)
        {
            
            Producto producto = new Producto();
            Usuario usuario = new Usuario();
            foreach (PostVenta item in listaDeProductosVendidos)
            {
                producto = ProductoHandler.TraerProducto_conId(item.Id);
                if (producto.Id <= 0) 
                {
                    return false;
                }

                if (item.Stock <= 0) 
                {
                    return false;
                }

                if (producto.Stock < item.Stock) 
                {
                    return false;
                }

                usuario = UsuarioHandler.TraerUsuario_conId(item.IdUsuario);
                if (usuario.Id <= 0) 
                {
                    return false;
                }
            }

            
            Venta venta = new Venta();
            long idVenta = VentaHandler.CargarVenta(venta);
            
            if (idVenta >= 0)
            {
                
                List<ProductoVendido> productosVendidos = new List<ProductoVendido>();
                foreach (PostVenta item in listaDeProductosVendidos)
                {
                    ProductoVendido productoVendido = new ProductoVendido();
                    productoVendido.IdProducto = item.Id;
                    productoVendido.Stock = item.Stock;
                    productoVendido.IdVenta = idVenta;
                    productosVendidos.Add(productoVendido);
                }
                
                if (ProductoVendidoHandler.CargarProductosVendidos(productosVendidos))
                {
                 
                    bool resultado = false;

                    
                    foreach (ProductoVendido item in productosVendidos)
                    {
                        producto.Id = item.IdProducto;
                        producto = ProductoHandler.ConsultarStock(producto);
                        producto.Stock = producto.Stock - item.Stock;
                        resultado = ProductoHandler.ActualizarStock(producto);
                        if (resultado == false) 
                        {
                            break;
                        }
                    }
                    return resultado;
                }
                else
                {
                    return false; 
                }
            }
            else
            {
                return false; 
            }
        }

    }
}
