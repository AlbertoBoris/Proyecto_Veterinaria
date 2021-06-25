using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;


namespace Veterinaria.Models
{
    public class Cita
    {
        [DisplayName("Id")]
        public string ID_CITA { get; set; }

        [DisplayName("Fecha")]
        public DateTime FECHA_REG { get; set; }

        [DisplayName("Usuario")]
        public string ID_USU { get; set; }

        [DisplayName("Area")]
        public string ID_AREA { get; set; }

        [DisplayName("Mascota")]
        public string ID_MASC { get; set; }

        [DisplayName("Horario")]
        public string ID_HORAR { get; set; }

        [DisplayName("Hora")]
        public string ID_HORA { get; set; }

        [DisplayName("Estado")]
        public string ID_ESTA { get; set; }

        [DisplayName("Importe")]
        public double IMPORTE { get; set; }
    }
}