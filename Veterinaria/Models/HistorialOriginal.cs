using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Veterinaria.Models
{
    public class HistorialOriginal
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Código")]
        [DisplayName("Codigo")]
        public string ID_HIST { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Código")]
        [DisplayName("Mascota")]
        public string ID_MASC { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Fecha de Nacimiento (yyyy-MM-dd)")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Fecha Atencion")]
        public DateTime FEC_ATT { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese ASunto")]
        [RegularExpression("^[a-zA-Z ]{1,100}$", ErrorMessage = "Solo letras en el Campo Asunto")]
        [DisplayName("Asunto")]
        public string ASUNTO { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Descripcion")]
        [RegularExpression("^[a-zA-Z ]{1,100}$", ErrorMessage = "Solo letras en el Campo Descripcion")]
        [DisplayName("Descripcion")]
        public string DESCRIPCION { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Tratamiento")]
        [RegularExpression("^[a-zA-Z ]{1,100}$", ErrorMessage = "Solo letras en el Campo Tratamiento")]
        [DisplayName("Tratamiento")]
        public string TRATAMIENTO { get; set; }
    }
}