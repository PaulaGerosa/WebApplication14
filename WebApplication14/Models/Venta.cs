using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication14.Models
{
    public class Venta
    {
        //Modelo
        public int Id { get; set; }
        public string Comentarios { get; set; }


        //Constructor
        public Venta()
        {
            Id = 0;
            Comentarios = string.Empty;
        }
    }
}