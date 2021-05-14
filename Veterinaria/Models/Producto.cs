using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Veterinaria.Models

{
    public class Producto
    {
        [DisplayName("CODIGO")]
        public string ID_PROD { get; set; }

        [DisplayName("NOMBRE")]
        public string NOMB_PROD { get; set; }

        [DisplayName("PRECIO")]
        public double PREC_PROD { get; set; }

        [DisplayName("STOCK")]
        public int STOCK { get; set; }

        [DisplayName("SERIE")]
        public string SERIE { get; set; }

        [DisplayName("MARCA")]
        public string MARCA { get; set; }

        [DisplayName("PROVEEDOR")]
        public string PROV_PROD { get; set; }

        [DisplayName("DESCRIPCION")]
        public string DESC_PROD { get; set; }

        [DisplayName("DETALLE")]
        public string DESC_HTML { get; set; }

        [DisplayName("FOTO")]
        public string FOTO { get; set; }
    }
}