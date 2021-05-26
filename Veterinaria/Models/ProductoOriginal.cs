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
        [RegularExpression("^[a-zA-Z]{1,40}$", ErrorMessage = "Solo letras en el Campo Nombre")]

        [DisplayName("NOMBRE")]
        public string NOMB_PROD { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Precio")]
        [Range(1.00, 10000.00, ErrorMessage = "El precio no puede ser 0")]

        [DisplayName("PRECIO")]
        public double PREC_PROD { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Stock")]
        [Range(1, 10000, ErrorMessage = "Solo numeros en el Campo Stock")]

        [DisplayName("STOCK")]
        public int STOCK { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Serie")]
        [RegularExpression("^[0-9A-Z]{8}$", ErrorMessage = "La serie del Producto debe ser de 8 caracteres entre numeros y Letras en Mayuscula")]
        [StringLength(8, ErrorMessage = "La serie del Producto debe ser de 8 caracteres")]
        [DisplayName("SERIE")]
        public string SERIE { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Marca")]
        [RegularExpression("^[a-zA-Z]{1,40}$", ErrorMessage = "Solo letras en el Campo Marca")]

        [DisplayName("MARCA")]
        public string MARCA { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Proveedor")]
        [RegularExpression("^[a-zA-Z]{1,40}$", ErrorMessage = "Solo letras en el Campo Proveedor")]

        [DisplayName("PROVEEDOR")]
        public string PROV_PROD { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Descripcion")]
        [RegularExpression("^[a-zA-Z]{1,1000}$", ErrorMessage = "Solo letras en el Campo Descripcion")]

        [DisplayName("DESCRIPCION")]
        public string DESC_PROD { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Detalle")]
        [RegularExpression("^[a-zA-Z]{1,1000}$", ErrorMessage = "Solo letras en el Campo Detalle")]

        [DisplayName("DETALLE")]
        public string DESC_HTML { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Foto")]
        [DisplayName("FOTO")]
        public string FOTO { get; set; }
    }
}
