using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Veterinaria.Models

{
    public class ProductoOriginal
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Código")]
        [DisplayName("CODIGO")]
        public string ID_PROD { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Nombre")]
        [DisplayName("NOMBRE")]
        public string NOMB_PROD { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Precio")]
        [DisplayName("PRECIO")]
        public double PREC_PROD { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Stock")]
        [DisplayName("STOCK")]
        public int STOCK { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Serie")]
        [DisplayName("SERIE")]
        public string SERIE { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Marca")]
        [DisplayName("MARCA")]
        public string MARCA { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Proveedor")]
        [DisplayName("PROVEEDOR")]
        public string PROV_PROD { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Descripcion")]
        [DisplayName("DESCRIPCION")]
        public string DESC_PROD { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Detalle")]
        [DisplayName("DETALLE")]
        public string DESC_HTML { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Foto")]
        [DisplayName("FOTO")]
        public string FOTO { get; set; }
    }
}
