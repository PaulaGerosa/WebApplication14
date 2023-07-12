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
    public class ProductoVendidoController : ApiController
    {
        //Obtener Productos Vendidos
        [HttpGet]
        public List<ProductoVendido> Consultar()
        {
            return new List<ProductoVendido>();
        }
        
    }
}
