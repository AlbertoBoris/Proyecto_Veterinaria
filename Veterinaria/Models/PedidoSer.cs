using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Veterinaria.Models
{
    public class PedidoSer
    {
        [DisplayName("Id")]
        public string ID_PEDI { get; set; }

        [DisplayName("Fecha")]
        public DateTime FECHA_PEDI { get; set; }

        [DisplayName("Usuario")]
        public string ID_USU { get; set; }

        [DisplayName("Producto")]
        public string ID_SERV { get; set; }

        [DisplayName("Edad")]
        public string ID_ESTA { get; set; }

        [DisplayName("Horario")]
        public string ID_HORAR { get; set; }

        [DisplayName("Hora")]
        public string ID_HORA { get; set; }

        [DisplayName("Importe")]
        public double IMPORTE { get; set; }
    }
}