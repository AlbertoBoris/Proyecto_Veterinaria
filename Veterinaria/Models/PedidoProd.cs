using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Veterinaria.Models
{
    public class PedidoProd
    {
        [DisplayName("Id")]
        public string ID_PEDI { get; set; }

        [DisplayName("Fecha")]
        public DateTime FECHA_PEDI { get; set; }

        [DisplayName("Usuario")]
        public string ID_USU { get; set; }

        [DisplayName("Producto")]
        public string ID_PROD { get; set; }

        [DisplayName("Estado")]
        public string ID_ESTA { get; set; }

        [DisplayName("Contador")]
        public  int CONTADOR { get; set; }

        [DisplayName("Importe")]
        public double IMPORTE { get; set; }
    }
}