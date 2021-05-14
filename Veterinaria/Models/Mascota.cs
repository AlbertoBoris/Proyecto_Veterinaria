using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Veterinaria.Models
{
    public class Mascota
    {
        [DisplayName("Codigo")]
        public string ID_MASC { get; set; }

        [DisplayName("Nombre")]
        public string NOMBRE { get; set; }

        [DisplayName("Animal")]
        public string ANIMAL { get; set; }

        [DisplayName("Raza")]
        public string RAZA { get; set; }

        [DisplayName("Edad")]
        public string EDAD { get; set; }

        [DisplayName("Fecha de Nac.")]
        public DateTime FECHA_NACI { get; set; }

        [DisplayName("Dueño")]
        public string ID_USU { get; set; }
    }
}