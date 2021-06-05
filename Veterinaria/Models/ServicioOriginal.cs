using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Veterinaria.Models

{
    public class ServicioOriginal
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Código")]
        [DisplayName("CODIGO")]
        public string ID_SERV { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Nombre")]
        [RegularExpression("^[a-zA-Z ]{1,50}$", ErrorMessage = "Solo letras en el Campo Nombre")]

        [DisplayName("NOMBRE")]
        public string NOMB_SERV { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Precio")]
        [Range(1.00, 10000.00, ErrorMessage = "El precio no puede ser 0")]

        [DisplayName("PRECIO")]
        public double PRECIO_SERV { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Descripción")]
        [RegularExpression("^[a-zA-Z ]{1,100}$", ErrorMessage = "Solo letras en el Campo descripcion")]

        [DisplayName("DESCRIPCION")]
        public string DESC_SERV { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Horario")]

        [DisplayName("HORARIO")]
        public string ID_HORAR { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Fecha de Servicio (yyyy-MM-dd)")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("FECHASERVICIO")]
        public DateTime FECH_SERV { get; set; }


    }
}