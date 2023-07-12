using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication14.Models
{
        public class ProductoVendido
    {
        //Modelo
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public int Stock { get; set; }
        public int IdVenta { get; set; }


        //Constructor
        public ProductoVendido()
        {
            Id = 0;
            IdProducto = 0;
            Stock = 0;
            IdVenta = 0;
        }
    }
}