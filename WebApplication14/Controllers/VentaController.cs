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
        //Traer ventas
        [HttpGet]
        public List<ProductoVendido> TraerVentas()
        {
            return VentaHandler.TraerVentas();
        }


    }
}