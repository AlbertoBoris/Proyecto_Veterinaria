using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Veterinaria.Models

{
    public class Servicio
    {
        [DisplayName("CODIGO")]
        public string ID_SERV { get; set; }

        [DisplayName("NOMBRE")]
        public string NOMB_SERV { get; set; }

        [DisplayName("PRECIO")]
        public double PRECIO_SERV { get; set; }

        [DisplayName("DESCRIPCION")]
        public string DESC_SERV { get; set; }

        [DisplayName("HORARIO")]
        public string ID_HORAR { get; set; }

        [DisplayName("FECHASERVICIO")]
        public DateTime FECH_SERV { get; set; }

    }
}