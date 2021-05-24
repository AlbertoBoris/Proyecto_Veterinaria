using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Veterinaria.Models
{
    public class PedidoSerOriginal
    {
        [DisplayName("Id")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese ID")]
        public string ID_PEDI { get; set; }

        [DisplayName("Fecha")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Fecha")]
        public DateTime FECHA_PEDI { get; set; }

        [DisplayName("Usuario")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Usuario")]
        public string ID_USU { get; set; }

        [DisplayName("Producto")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Producto")]
        public string ID_SERV { get; set; }

        [DisplayName("Edad")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Edad")]
        public string ID_ESTA { get; set; }

        [DisplayName("Horario")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Horario")]
        public string ID_HORAR { get; set; }

        [DisplayName("Hora")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Hora")]
        public string ID_HORA { get; set; }

        [DisplayName("Importe")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Importe")]
        public double IMPORTE { get; set; }
    }
}