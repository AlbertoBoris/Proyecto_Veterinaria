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
        [DisplayName("NOMBRE")]
        public string NOMB_SERV { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Precio")]
        [DisplayName("PRECIO")]
        public double PRECIO_SERV { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Descripción")]
        [DisplayName("DESCRIPCION")]
        public string DESC_SERV { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Horario")]
        [DisplayName("HORARIO")]
        public string ID_HORAR { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Fecha de Servicio")]
        [DisplayName("FECHASERVICIO")]
        public DateTime FECH_SERV { get; set; }





    }
}