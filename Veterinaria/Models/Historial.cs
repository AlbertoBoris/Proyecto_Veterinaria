using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Veterinaria.Models
{
    public class Historial
    {
        [DisplayName("Codigo")]
        public string ID_HIST { get; set; }

        [DisplayName("Mascota")]
        public string ID_MASC { get; set; }

        [DisplayName("Fecha Atencion")]
        public DateTime FEC_ATT { get; set; }

        [DisplayName("Asunto")]
        public string ASUNTO { get; set; }

        [DisplayName("Descripcion")]
        public string DESCRIPCION { get; set; }

        [DisplayName("Tratamiento")]
        public string TRATAMIENTO { get; set; }
    }
}