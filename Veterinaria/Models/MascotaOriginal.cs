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
        [RegularExpression("^[a-zA-Z ]{1,30}$", ErrorMessage = "Solo letras en el Campo Nombre")]
        [DisplayName("Nombre")]
        public string NOMBRE { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Animal")]
        [RegularExpression("^[a-zA-Z ]{1,30}$", ErrorMessage = "Solo letras en el Campo Animal")]
        [DisplayName("Animal")]
        public string ANIMAL { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Raza")]
        [RegularExpression("^[a-zA-Z ]{1,40}$", ErrorMessage = "Solo letras en el Campo Raza")]
        [DisplayName("Raza")]
        public String RAZA { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Edad")]
        [Range(1, 20, ErrorMessage = "La edad debe ser de 1 a 20 años")]
        [DisplayName("Edad")]
        public string EDAD { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Fecha de Nacimiento (yyyy-MM-dd)")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FECHA_NACI { get; set; }

        [DisplayName("Usuario")]
        public string ID_USU { get; set; }
    }
}