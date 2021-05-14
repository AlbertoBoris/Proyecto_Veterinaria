using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Veterinaria.Models
{
    public class MascotaOriginal
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Código")]
        [DisplayName("Codigo")]
        public string ID_MASC { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Nombre")]
        [DisplayName("Nombre")]
        public string NOMBRE { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Animal")]
        [DisplayName("Animal")]
        public string ANIMAL { get; set; }

        [Required(AllowEmptyStrings = false,ErrorMessage ="Ingrese Raza")]
        [DisplayName("Raza")]

        public String RAZA { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Edad")]
        [DisplayName("Edad")]
        public string EDAD { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Fecha de Nac.")]
        [DisplayName("Fecha de Nac.")]
        public DateTime FECHA_NACI { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Usuario")]
        [DisplayName("Usuario")]
        public string ID_USU { get; set; }
    }
}