using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Veterinaria.Models
{
    public class PedidoProdOriginal
    {
        [DisplayName("N° Pedido")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese ID")]
        public string ID_PEDI { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Fecha de Registro")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Fecha")]
        public DateTime FECHA_PEDI { get; set; }

        [DisplayName("Usuario")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Usuario")]
        public string ID_USU { get; set; }

        [DisplayName("Producto")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Producto")]
        public string ID_PROD { get; set; }

        [DisplayName("Estado")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Estado")]
        public string ID_ESTA { get; set; }

        [DisplayName("Cantidad")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Contador")]
        public int CONTADOR { get; set; }

        [DisplayName("Total a Pagar")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Importe")]
        public double IMPORTE { get; set; }
    }
}