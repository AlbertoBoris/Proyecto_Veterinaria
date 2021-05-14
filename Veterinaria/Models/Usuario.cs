using System;
using System.ComponentModel;

namespace Veterinaria.Models
{
    public class Usuario
    {
        [DisplayName("CODIGO")]
        public string ID_USU { get; set; }

        [DisplayName("NOMBRES")]
        public string NOMBRES { get; set; }

        [DisplayName("APELLIDOS")]
        public string APELLIDOS { get; set; }

        [DisplayName("DIRECCION")]
        public string DIRECCION { get; set; }

        [DisplayName("DNI")]
        public string DNI { get; set; }

        [DisplayName("NICKNAME")]
        public string NOMB_USU { get; set; }

        [DisplayName("CONTRASEÑA")]
        public string PASS_USU { get; set; }

        [DisplayName("CORREO")]
        public string CORREO_USU { get; set; }

        [DisplayName("FECHA NACIMIENTO")]
        public DateTime FECHA_NACI { get; set; }

        [DisplayName("TELEFONO")]
        public string TELEFONO { get; set; }

        [DisplayName("SEXO")]
        public string SEXO_USU { get; set; }

        [DisplayName("DISTRITO")]
        public string ID_DIST { get; set; }
    }
}